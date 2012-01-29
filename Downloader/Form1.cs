using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

/// <summary>
/// Input file format:
/// [absolute url base]
/// [sha1:size:path]+
/// </summary>

namespace Downloader
{
    public partial class downloaderFrm : Form
    {

        struct Link
        {
            public byte[] hash;
            public long size;
            public string path;
        };
        private string root;
        private string file_data;
        private Link[] links;

        private bool hasError;

        private static string array_to_string<T>(T[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T x in array)
            {
                sb.Append(String.Format("{0:X2}", x));
            }
            return sb.ToString();
        }

        private static bool array_eq<T>(T[] a1, T[] a2)
        {
            if (a1.Length != a2.Length)
                return false;
            for (int i = 0; i < a1.Length; ++i)
                if (!a1[i].Equals(a2[i]))
                    return false;
            return true;
        }

        // mkdir -p
        // first path component of path0 must be valid or
        // (DirectoryNotFoundException : IOException) will be thrown
        private static void mkdirP(string path0) 
        {
            string path = path0.Replace('\\', '/'); // normalize dir sep to unix
            if (Directory.Exists(path))
                return;
            // Loop as long as there exist at least 2 slashes
            while (path.IndexOf('/') != path.LastIndexOf('/'))
            {
                string p0 = path.Substring(0, path.LastIndexOf('/'));
                if (Directory.Exists(p0))
                {
                    Directory.CreateDirectory(path);
                    return;
                }
                path = p0;
            }

            if (path.IndexOf('/') != -1)
            {
                Directory.CreateDirectory(path);
            }
        }
  
        // get sha1 of fname
        private byte[] hashFile(string fname)
        {
            SHA1 sha1 = SHA1.Create();
            try
            {
                return sha1.ComputeHash(File.OpenRead(fname));
            }
            catch (IOException e)
            {
                MessageBox.Show("IOException: " + e.Message);
                hasError = true;
                // can't hash -> return all zeroes
                byte[] b = new byte[sha1.HashSize / 8];
                System.Array.Clear(b, 0, b.Length);
                return b;
            }
        }

        private static bool matchesSize(string fname, long sz)
        {
            long fsz = (new FileInfo(fname)).Length;
            return (fsz == sz);
        }
        private bool matchesHash(string fname, byte[] h)
        {
            byte[] x = hashFile(fname);
            bool z = array_eq(x, h);
            return z;
        }

        // Tokenize sha1:size:link\n to array of sha-link pairs
        private void parseInFile()
        {
            if (file_data == null)
            {
                MessageBox.Show("Error: no active input file");
                return;
            }
            string[] lines = file_data.Split('\n');
            root = lines[0];
            ArrayList al = new ArrayList();
            for (int i = 1; i < lines.Length; ++i)
            {
                if (lines[i].Length == 0 || lines[i][0] == '#')
                    continue;
                string[] comp = lines[i].Split(':');
                if (comp.Length != 3)
                {
                    MessageBox.Show("Tokenizer error: input lines does not split into two tokens");
                    continue;
                }
                byte[] b = new byte[comp[0].Length / 2];
                for (int j = 0; j < comp[0].Length; ++j)
                {
                    if (j % 2 == 0)
                        b[j / 2] = Byte.Parse(comp[0].Substring(j, 2),
                                    System.Globalization.NumberStyles.HexNumber);
                }
                al.Add(new Link
                        {
                            hash = b,
                            size = long.Parse(comp[1]),
                            path = comp[2]
                        });
            }
            links = (Link[])al.ToArray(typeof(Link));
        }


        public downloaderFrm()
        {
            InitializeComponent();
        }

        private void selectSrcBtn_Click(object sender, EventArgs e)
        {
            openFileDlg.ShowDialog();
        }

        // Open srcPath to input string
        private void openFileDlg_FileOk(object sender, CancelEventArgs e)
        {
            // Update srcPath
            srcPathTxt.Text = openFileDlg.FileName;
            try
            {
                file_data = File.ReadAllText(srcPathTxt.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show("IOException in openFile: " + ex.Message);
                hasError = true;
            }
        }
        // Download srcPath to input string
        private void downloadSrcBtn_Click(object sender, EventArgs e)
        {
            if (srcPathTxt.Text.Length == 0)
            {
                MessageBox.Show("Error: empty src text");
                hasError = true;
                return;
            }
            try
            {
                using (HttpWebResponse wr = ((HttpWebResponse)(
                        ((HttpWebRequest)WebRequest.Create(srcPathTxt.Text))
                        .GetResponse())))
                {
                    StringBuilder sb = new StringBuilder();
                    byte[] buf = new byte[4096];
                    Stream r = wr.GetResponseStream();
                    int count;
                    do
                    {
                        count = r.Read(buf, 0, buf.Length);
                        if (count != 0)
                        {
                            string t = Encoding.ASCII.GetString(buf, 0, count);
                            sb.Append(t);
                        }
                    } while (count > 0);
                    file_data = sb.ToString();
                    MessageBox.Show("Download OK");
                }
            }
            catch (WebException we)
            {
                MessageBox.Show(String.Format("HTTP Error: {0}", we.Message));
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (hasError)
            {
                MessageBox.Show("Pre-existing error; must reset");
                return;
            }
            parseInFile();

            if (links == null)
            {
                MessageBox.Show("Error: no links");
                return;
            }
            // For each <sha, link>:
            //      Get the local name
            //      Log remote, local name
            //      If exist, then
            //          Check the hash
            //          If match, then skip
            //      Else
            //          Download to file
            //      Continue
            foreach (Link link in links)
            {
                string localFile = (new StringBuilder(destPathTxt.Text))
                    .Append('/').Append(link.path).ToString().Replace('\\', '/');
                progressTxt.Text += localFile + ":";
                Console.Write(localFile + ":");
                // Check if file is ok before continuing
                if (File.Exists(localFile)) {
                    // Check size first (and then hash if match) for speed
                    if (matchesSize(localFile, link.size)) {
                        progressTxt.Text += String.Format(" size ok ({0})", link.size);
                        Console.Write(" size ok ({0})", link.size);
                        byte[] fhash = hashFile(localFile);
                        if (array_eq(fhash, link.hash))
                        {
                            progressTxt.Text += String.Format(", hash ok ({0})\r\n", array_to_string(link.hash));
                            Console.Write(", hash ok ({0})\r\n", array_to_string(link.hash));
                            continue;
                        }
                        else
                        {
                            progressTxt.Text += String.Format(", HASH FAIL(got: {0}, expected: {1};", array_to_string(fhash), array_to_string(link.hash));
                            Console.Write(", HASH FAIL(got: {0}, expected: {1};", array_to_string(fhash), array_to_string(link.hash));
                        }
                    }
                    else
                    {
                        progressTxt.Text += String.Format(" SIZE FAIL (got: {0}, expected: {1}; ",
                                    (new FileInfo(localFile)).Length, link.size);
                        Console.Write(" SIZE FAIL (got: {0}, expected: {1}; ",
                                    (new FileInfo(localFile)).Length, link.size);
                    }
                }
                string localFolder = localFile.Substring(0, localFile.LastIndexOf('/'));
                mkdirP(localFolder); // auto-checks if Directory.Exists(...)

                string remotePath = (new StringBuilder(root)).Append('/').Append(link.path).ToString();
                progressTxt.Text += "File not found or fails check: downloading:";
                Console.Write("File not found or fails check: downloading:");
                // File download
                try
                {
                    using (HttpWebResponse wr = ((HttpWebResponse)(
                        ((HttpWebRequest)WebRequest.Create(remotePath))
                        .GetResponse())))
                    {
                        if (wr.StatusCode != HttpStatusCode.OK)
                        {
                            progressTxt.Text += String.Format(" HTTP ERROR: {0}\r\n", wr.StatusCode);
                            Console.Write(" HTTP ERROR: {0}\r\n", wr.StatusCode);
                            continue;
                        }
                        using (BinaryWriter bw =
                                new BinaryWriter(File.Open(localFile, FileMode.OpenOrCreate)))
                        {
                            Stream r = wr.GetResponseStream();
                            byte[] buf = new byte[500000]; // download in 500k chunks
                            int count;
                            do
                            {
                                count = r.Read(buf, 0, buf.Length);
                                if (count != 0)
                                {
                                    bw.Write(buf, 0, count);
                                    if (count == buf.Length)
                                    {
                                        progressTxt.Text += "@";
                                        Console.Write('@');
                                    }
                                    else
                                    {
                                        progressTxt.Text += "$";
                                        Console.Write('$');
                                    }
                                }
                            } while (count > 0);
                        }
                    }
                }
                catch (WebException we)
                {
                    progressTxt.Text += String.Format(" WEB ERROR: {0}\r\n", we.Message);
                    Console.Write(" WEB ERROR: {0}\r\n", we.Message);
                    continue;
                }
                // check if download went ok
                if (matchesSize(localFile, link.size))
                {
                    progressTxt.Text += String.Format(" size ok ({0})", link.size);
                    Console.Write(" size ok ({0})", link.size);
                    byte[] fhash = hashFile(localFile);
                    if (array_eq(fhash, link.hash))
                    {
                        progressTxt.Text += String.Format(", hash ok ({0})\r\n", array_to_string(link.hash));
                        Console.Write(", hash ok ({0})\r\n", array_to_string(link.hash));
                    }
                    else
                    {
                        progressTxt.Text += String.Format(", HASH FAIL(got: {0}, expected: {1}\r\n", array_to_string(fhash), array_to_string(link.hash));
                        Console.Write(", HASH FAIL(got: {0}, expected: {1}\r\n", array_to_string(fhash), array_to_string(link.hash));
                    }
                } else {
                    progressTxt.Text += String.Format(" SIZE FAIL (got: {0}, expected: {1}\r\n",
                                (new FileInfo(localFile)).Length, link.size);
                    Console.Write(" SIZE FAIL (got: {0}, expected: {1}\r\n",
                                (new FileInfo(localFile)).Length, link.size);
                }
            }
            Console.WriteLine("done");
            MessageBox.Show("Downloading complete.");
        }

        private void selectDestBtn_Click(object sender, EventArgs e)
        {
            // Can't do FileOK on folder dialogs so do the dialog handling here
            DialogResult res = saveFolderDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                // Update srcPath
                destPathTxt.Text = saveFolderDlg.SelectedPath;
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            hasError = false;
        }

    }
}
