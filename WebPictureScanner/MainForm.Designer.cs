namespace WebPictureScanner
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.AuthorInfo = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SearchKeywordTextBox = new System.Windows.Forms.TextBox();
			this.SearchKeywordButton = new System.Windows.Forms.Button();
			this.ResultTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SearchListTextBox = new System.Windows.Forms.TextBox();
			this.SearchListButton = new System.Windows.Forms.Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.PageNumTextBox = new System.Windows.Forms.TextBox();
			this.JDSelfCheckBox = new System.Windows.Forms.CheckBox();
			this.FilterComboBox = new System.Windows.Forms.ComboBox();
			this.CleanComboBox = new System.Windows.Forms.CheckBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.ShowFileSelectDialog = new System.Windows.Forms.Button();
			this.HelpLabel = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// AuthorInfo
			// 
			this.AuthorInfo.AutoSize = true;
			this.AuthorInfo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AuthorInfo.Location = new System.Drawing.Point(282, 290);
			this.AuthorInfo.Name = "AuthorInfo";
			this.AuthorInfo.Size = new System.Drawing.Size(91, 14);
			this.AuthorInfo.TabIndex = 0;
			this.AuthorInfo.TabStop = true;
			this.AuthorInfo.Text = "Author: Leon";
			this.AuthorInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorInfo_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(379, 290);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 14);
			this.label1.TabIndex = 1;
			this.label1.Text = "Version 0.7";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(27, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "搜索关键字";
			// 
			// SearchKeywordTextBox
			// 
			this.SearchKeywordTextBox.Location = new System.Drawing.Point(98, 8);
			this.SearchKeywordTextBox.Name = "SearchKeywordTextBox";
			this.SearchKeywordTextBox.Size = new System.Drawing.Size(259, 21);
			this.SearchKeywordTextBox.TabIndex = 3;
			this.SearchKeywordTextBox.TextChanged += new System.EventHandler(this.SearchKeywordTextBox_TextChanged);
			// 
			// SearchKeywordButton
			// 
			this.SearchKeywordButton.Enabled = false;
			this.SearchKeywordButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SearchKeywordButton.Location = new System.Drawing.Point(363, 7);
			this.SearchKeywordButton.Name = "SearchKeywordButton";
			this.SearchKeywordButton.Size = new System.Drawing.Size(100, 22);
			this.SearchKeywordButton.TabIndex = 4;
			this.SearchKeywordButton.Text = "搜索";
			this.SearchKeywordButton.UseVisualStyleBackColor = true;
			this.SearchKeywordButton.Click += new System.EventHandler(this.SearchKeywordButton_Click);
			// 
			// ResultTextBox
			// 
			this.ResultTextBox.Location = new System.Drawing.Point(13, 97);
			this.ResultTextBox.Multiline = true;
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ResultTextBox.Size = new System.Drawing.Size(452, 181);
			this.ResultTextBox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(39, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "批量搜索";
			// 
			// SearchListTextBox
			// 
			this.SearchListTextBox.Location = new System.Drawing.Point(98, 31);
			this.SearchListTextBox.Name = "SearchListTextBox";
			this.SearchListTextBox.Size = new System.Drawing.Size(218, 21);
			this.SearchListTextBox.TabIndex = 7;
			this.SearchListTextBox.TextChanged += new System.EventHandler(this.SearchListTextBox_TextChanged);
			// 
			// SearchListButton
			// 
			this.SearchListButton.Enabled = false;
			this.SearchListButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SearchListButton.Location = new System.Drawing.Point(363, 30);
			this.SearchListButton.Name = "SearchListButton";
			this.SearchListButton.Size = new System.Drawing.Size(100, 21);
			this.SearchListButton.TabIndex = 8;
			this.SearchListButton.Text = "批量搜索";
			this.SearchListButton.UseVisualStyleBackColor = true;
			this.SearchListButton.Click += new System.EventHandler(this.SearchListButton_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// PageNumTextBox
			// 
			this.PageNumTextBox.Location = new System.Drawing.Point(98, 68);
			this.PageNumTextBox.Name = "PageNumTextBox";
			this.PageNumTextBox.Size = new System.Drawing.Size(218, 21);
			this.PageNumTextBox.TabIndex = 10;
			this.PageNumTextBox.Text = "100";
			this.PageNumTextBox.TextChanged += new System.EventHandler(this.PageNumTextBox_TextChanged);
			// 
			// JDSelfCheckBox
			// 
			this.JDSelfCheckBox.AutoSize = true;
			this.JDSelfCheckBox.Checked = true;
			this.JDSelfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.JDSelfCheckBox.Location = new System.Drawing.Point(322, 70);
			this.JDSelfCheckBox.Name = "JDSelfCheckBox";
			this.JDSelfCheckBox.Size = new System.Drawing.Size(72, 16);
			this.JDSelfCheckBox.TabIndex = 11;
			this.JDSelfCheckBox.Text = "京东自营";
			this.JDSelfCheckBox.UseVisualStyleBackColor = true;
			// 
			// FilterComboBox
			// 
			this.FilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FilterComboBox.FormattingEnabled = true;
			this.FilterComboBox.Items.AddRange(new object[] {
            "结果数量",
            "结果页数"});
			this.FilterComboBox.Location = new System.Drawing.Point(13, 68);
			this.FilterComboBox.Name = "FilterComboBox";
			this.FilterComboBox.Size = new System.Drawing.Size(80, 20);
			this.FilterComboBox.TabIndex = 14;
			this.FilterComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterComboBox_SelectedIndexChanged);
			// 
			// CleanComboBox
			// 
			this.CleanComboBox.AutoSize = true;
			this.CleanComboBox.Checked = true;
			this.CleanComboBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CleanComboBox.Location = new System.Drawing.Point(393, 70);
			this.CleanComboBox.Name = "CleanComboBox";
			this.CleanComboBox.Size = new System.Drawing.Size(72, 16);
			this.CleanComboBox.TabIndex = 15;
			this.CleanComboBox.Text = "清理图片";
			this.CleanComboBox.UseVisualStyleBackColor = true;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "关键字列表|*.txt";
			this.openFileDialog.InitialDirectory = "./";
			// 
			// ShowFileSelectDialog
			// 
			this.ShowFileSelectDialog.Location = new System.Drawing.Point(322, 31);
			this.ShowFileSelectDialog.Name = "ShowFileSelectDialog";
			this.ShowFileSelectDialog.Size = new System.Drawing.Size(35, 21);
			this.ShowFileSelectDialog.TabIndex = 16;
			this.ShowFileSelectDialog.Text = "...";
			this.ShowFileSelectDialog.UseVisualStyleBackColor = true;
			this.ShowFileSelectDialog.Click += new System.EventHandler(this.showDialog_Click);
			// 
			// HelpLabel
			// 
			this.HelpLabel.AutoSize = true;
			this.HelpLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HelpLabel.Location = new System.Drawing.Point(12, 292);
			this.HelpLabel.Name = "HelpLabel";
			this.HelpLabel.Size = new System.Drawing.Size(53, 12);
			this.HelpLabel.TabIndex = 17;
			this.HelpLabel.TabStop = true;
			this.HelpLabel.Text = "使用帮助";
			this.HelpLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLabel_LinkClicked);
			// 
			// MainForm
			// 
			this.AcceptButton = this.SearchKeywordButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(477, 315);
			this.Controls.Add(this.HelpLabel);
			this.Controls.Add(this.ShowFileSelectDialog);
			this.Controls.Add(this.CleanComboBox);
			this.Controls.Add(this.FilterComboBox);
			this.Controls.Add(this.JDSelfCheckBox);
			this.Controls.Add(this.PageNumTextBox);
			this.Controls.Add(this.SearchListButton);
			this.Controls.Add(this.SearchListTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ResultTextBox);
			this.Controls.Add(this.SearchKeywordButton);
			this.Controls.Add(this.SearchKeywordTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.AuthorInfo);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "WebPictureScanner For jd.com";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel AuthorInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SearchKeywordTextBox;
        private System.Windows.Forms.Button SearchKeywordButton;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SearchListTextBox;
        private System.Windows.Forms.Button SearchListButton;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.TextBox PageNumTextBox;
		private System.Windows.Forms.CheckBox JDSelfCheckBox;
		private System.Windows.Forms.ComboBox FilterComboBox;
		private System.Windows.Forms.CheckBox CleanComboBox;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button ShowFileSelectDialog;
		private System.Windows.Forms.LinkLabel HelpLabel;
    }
}

