namespace Downloader
{
    partial class downloaderFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.srcPathTxt = new System.Windows.Forms.TextBox();
            this.srcPathLbl = new System.Windows.Forms.Label();
            this.selectSrcBtn = new System.Windows.Forms.Button();
            this.downloadSrcBtn = new System.Windows.Forms.Button();
            this.destPathLbl = new System.Windows.Forms.Label();
            this.destPathTxt = new System.Windows.Forms.TextBox();
            this.selectDestBtn = new System.Windows.Forms.Button();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFolderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.progressLbl = new System.Windows.Forms.Label();
            this.progressTxt = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // srcPathTxt
            // 
            this.srcPathTxt.Location = new System.Drawing.Point(12, 38);
            this.srcPathTxt.Name = "srcPathTxt";
            this.srcPathTxt.Size = new System.Drawing.Size(198, 20);
            this.srcPathTxt.TabIndex = 0;
            // 
            // srcPathLbl
            // 
            this.srcPathLbl.AutoSize = true;
            this.srcPathLbl.Location = new System.Drawing.Point(16, 11);
            this.srcPathLbl.Name = "srcPathLbl";
            this.srcPathLbl.Size = new System.Drawing.Size(66, 13);
            this.srcPathLbl.TabIndex = 1;
            this.srcPathLbl.Text = "Source Path";
            // 
            // selectSrcBtn
            // 
            this.selectSrcBtn.Location = new System.Drawing.Point(216, 36);
            this.selectSrcBtn.Name = "selectSrcBtn";
            this.selectSrcBtn.Size = new System.Drawing.Size(75, 23);
            this.selectSrcBtn.TabIndex = 2;
            this.selectSrcBtn.Text = "Select";
            this.selectSrcBtn.UseVisualStyleBackColor = true;
            this.selectSrcBtn.Click += new System.EventHandler(this.selectSrcBtn_Click);
            // 
            // downloadSrcBtn
            // 
            this.downloadSrcBtn.Location = new System.Drawing.Point(297, 35);
            this.downloadSrcBtn.Name = "downloadSrcBtn";
            this.downloadSrcBtn.Size = new System.Drawing.Size(75, 23);
            this.downloadSrcBtn.TabIndex = 3;
            this.downloadSrcBtn.Text = "Download";
            this.downloadSrcBtn.UseVisualStyleBackColor = true;
            this.downloadSrcBtn.Click += new System.EventHandler(this.downloadSrcBtn_Click);
            // 
            // destPathLbl
            // 
            this.destPathLbl.AutoSize = true;
            this.destPathLbl.Location = new System.Drawing.Point(16, 71);
            this.destPathLbl.Name = "destPathLbl";
            this.destPathLbl.Size = new System.Drawing.Size(84, 13);
            this.destPathLbl.TabIndex = 5;
            this.destPathLbl.Text = "Destination path";
            // 
            // destPathTxt
            // 
            this.destPathTxt.Location = new System.Drawing.Point(12, 87);
            this.destPathTxt.Name = "destPathTxt";
            this.destPathTxt.Size = new System.Drawing.Size(198, 20);
            this.destPathTxt.TabIndex = 6;
            // 
            // selectDestBtn
            // 
            this.selectDestBtn.Location = new System.Drawing.Point(216, 85);
            this.selectDestBtn.Name = "selectDestBtn";
            this.selectDestBtn.Size = new System.Drawing.Size(75, 23);
            this.selectDestBtn.TabIndex = 7;
            this.selectDestBtn.Text = "Select";
            this.selectDestBtn.UseVisualStyleBackColor = true;
            this.selectDestBtn.Click += new System.EventHandler(this.selectDestBtn_Click);
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog1";
            // 
            // progressLbl
            // 
            this.progressLbl.AutoSize = true;
            this.progressLbl.Location = new System.Drawing.Point(16, 120);
            this.progressLbl.Name = "progressLbl";
            this.progressLbl.Size = new System.Drawing.Size(48, 13);
            this.progressLbl.TabIndex = 8;
            this.progressLbl.Text = "Progress";
            // 
            // progressTxt
            // 
            this.progressTxt.Location = new System.Drawing.Point(12, 136);
            this.progressTxt.Multiline = true;
            this.progressTxt.Name = "progressTxt";
            this.progressTxt.Size = new System.Drawing.Size(279, 134);
            this.progressTxt.TabIndex = 9;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(297, 136);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 10;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(298, 247);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(75, 23);
            this.resetBtn.TabIndex = 11;
            this.resetBtn.Text = "Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // downloaderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 282);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.progressTxt);
            this.Controls.Add(this.progressLbl);
            this.Controls.Add(this.selectDestBtn);
            this.Controls.Add(this.destPathTxt);
            this.Controls.Add(this.destPathLbl);
            this.Controls.Add(this.downloadSrcBtn);
            this.Controls.Add(this.selectSrcBtn);
            this.Controls.Add(this.srcPathLbl);
            this.Controls.Add(this.srcPathTxt);
            this.Name = "downloaderFrm";
            this.Text = "Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox srcPathTxt;
        private System.Windows.Forms.Label srcPathLbl;
        private System.Windows.Forms.Button selectSrcBtn;
        private System.Windows.Forms.Button downloadSrcBtn;
        private System.Windows.Forms.Label destPathLbl;
        private System.Windows.Forms.TextBox destPathTxt;
        private System.Windows.Forms.Button selectDestBtn;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.FolderBrowserDialog saveFolderDlg;
        private System.Windows.Forms.Label progressLbl;
        private System.Windows.Forms.TextBox progressTxt;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button resetBtn;
    }
}

