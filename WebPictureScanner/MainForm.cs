using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace WebPictureScanner
{	
    public partial class MainForm : Form
    {
		private string m_filterType = "搜索结果";
		private bool m_isBatchSearch = false;
		private bool m_isRunning = false;

		public MainForm()
		{
			InitializeComponent();
			FilterComboBox.SelectedIndex = 0;
			m_filterType = FilterComboBox.SelectedItem.ToString();
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// 更新当前的状态
			ResultTextBox.AppendText(e.UserState.ToString() + "\r\n");
			ResultTextBox.ScrollToCaret();
		}

		private List<GoodInfo> searchResultByKeyword(object sender, DoWorkEventArgs e, string keyword)
		{
			WebAnalysis myWebAnalysis = new WebAnalysis();
			int FilterNum = int.Parse(PageNumTextBox.Text.ToString());
			List<GoodInfo> infoList = new List<GoodInfo>();

			int i = 1;
			while (true)
			{
				string url = "https://search.jd.com/Search?keyword=" + keyword + "&enc=utf-8&wq=" + keyword + "&page=" + (i * 2 - 1);
				backgroundWorker.ReportProgress(0, "载入第" + i + "页...");

				myWebAnalysis.analysisUrl(url);

				List<GoodInfo> result = myWebAnalysis.getGoodInfoList();
				foreach (GoodInfo info in result)
				{
					if (!JDSelfCheckBox.Checked || info.m_jdGood)
					{
						myWebAnalysis.savePicture(keyword + '/', info.m_picture);
						infoList.Add(info);
					}
						
				}

				if (m_filterType.Equals("结果数量") && infoList.Count > FilterNum)
				{
					infoList = infoList.GetRange(0, FilterNum);
					backgroundWorker.ReportProgress(0, "→  发现" + infoList.Count + "件商品！");
					break;
				}
				else if (m_filterType.Equals("结果页数") && i >= FilterNum)
				{
					backgroundWorker.ReportProgress(0, "→  发现" + infoList.Count + "件商品！");
					break;
				}
				else
				{
					backgroundWorker.ReportProgress(0, "→  发现" + infoList.Count + "件商品！");
				}

				// 取消
				if (((BackgroundWorker)sender).CancellationPending)
				{
					backgroundWorker.ReportProgress(0, "-------------------------- 程序被用户终止！ --------------------------\r\n");
					e.Cancel = true;
					return null;
				}

				++i;
			}
			return infoList;
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			List<string> batchKeyWord = new List<string>();

			HSSFWorkbook workbook = ExcelManager.createExcel();

			if (!m_isBatchSearch)
			{
				batchKeyWord.Add(SearchKeywordTextBox.Text);
			}
			else
			{
				StreamReader sr = new StreamReader(SearchListTextBox.Text, Encoding.Default);

				while (!sr.EndOfStream)
				{
					batchKeyWord.Add(sr.ReadLine());
				}
				sr.Close();
			}

			foreach (string keyword in batchKeyWord)
			{
				backgroundWorker.ReportProgress(0, "搜索关键字[" + keyword + "]...");
				List<GoodInfo> singleResult = searchResultByKeyword(sender, e, keyword);
				if (singleResult == null)
					return;

				HSSFSheet worksheet = ExcelManager.createSheet(workbook, keyword);

				// 调整picture的宽度
				worksheet.SetColumnWidth(0, 15000);
				worksheet.SetColumnWidth(1, 10000);

				for (int j = 0; j < singleResult.Count; ++j)
				{
					ExcelManager.getRow(worksheet, j).HeightInPoints = 200;
					ExcelManager.setCell(worksheet, j, 0, singleResult[j].m_title);
					string localFilePath = Directory.GetCurrentDirectory() + '/' + keyword + singleResult[j].m_picture.Substring(singleResult[j].m_picture.LastIndexOf('/'));
					//localFilePath = SearchKeywordTextBox.Text.ToString() + localFilePath;
					ExcelManager.setPicture(workbook, worksheet, j, 1, localFilePath);
				}
			}

			ExcelManager.closeExcelWithSave(workbook, string.Format("searchResult_{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}.xls", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));

			backgroundWorker.ReportProgress(0, "文件保存成功！");

			if (CleanComboBox.Checked)
			{
				backgroundWorker.ReportProgress(0, "清理图片资源中...");

				foreach (string keyword in batchKeyWord)
				{
					DirectoryInfo di = new DirectoryInfo(keyword + '/');
					if (di.Exists)
						di.Delete(true);
				}
				backgroundWorker.ReportProgress(0, "图片资源清理完毕！");
			}
		}

		private void SearchKeywordButton_Click(object sender, EventArgs e)
		{
			m_isBatchSearch = false;
			if (SearchKeywordButton.Text.Equals("搜索"))
			{
				ResultTextBox.Text = "-------------------------- 程序开始执行！ --------------------------\r\n";
				m_isRunning = true;
				backgroundWorker.RunWorkerAsync();
				CheckButtonStatus();
			}
			else
			{
				m_isRunning = false;
				backgroundWorker.CancelAsync();
				CheckButtonStatus();
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ResultTextBox.AppendText("-------------------------- 程序执行完毕！ --------------------------\r\n");
			ResultTextBox.ScrollToCaret();
			m_isRunning = false;
			CheckButtonStatus();
		}

		private void PageNumTextBox_TextChanged(object sender, EventArgs e)
		{
			int result;
			if (!int.TryParse(PageNumTextBox.Text.ToString(), out result)
				|| PageNumTextBox.Text.ToString().Equals(0)
				|| PageNumTextBox.Text.ToString().Equals(string.Empty))
				SearchKeywordButton.Enabled = false;
			else
				SearchKeywordButton.Enabled = true;
		}

		private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_filterType = FilterComboBox.SelectedItem.ToString();
		}

		private void showDialog_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				SearchListTextBox.Text = openFileDialog.FileName;
			}
		}

		private void CheckButtonStatus()
		{
			if (m_isRunning)
			{
				if (m_isBatchSearch)
				{
					SearchListButton.Text = "停止";
					SearchKeywordButton.Text = "搜索";
					SearchKeywordButton.Enabled = false;
					SearchListButton.Enabled = true;
				}
				else
				{
					SearchListButton.Text = "批量搜索";
					SearchKeywordButton.Text = "停止";
					SearchKeywordButton.Enabled = true;
					SearchListButton.Enabled = false;
				}
					
				FilterComboBox.Enabled = false;
				PageNumTextBox.Enabled = false;

				SearchKeywordTextBox.Enabled = false;
				SearchListTextBox.Enabled = false;

				JDSelfCheckBox.Enabled = false;
				CleanComboBox.Enabled = false;

				ShowFileSelectDialog.Enabled = false;
			}
			else
			{
				SearchKeywordButton.Text = "搜索";
				SearchListButton.Text = "批量搜索";

				int result;
				if (!int.TryParse(PageNumTextBox.Text.ToString(), out result)
				|| PageNumTextBox.Text.ToString().Equals(0)
				|| PageNumTextBox.Text.ToString().Equals(string.Empty))
				{
					SearchKeywordButton.Enabled = false;
					SearchListButton.Enabled = false;
				}
				else
				{
					if (!SearchKeywordTextBox.Text.ToString().Equals(string.Empty))
						SearchKeywordButton.Enabled = true;
					else
						SearchKeywordButton.Enabled = false;

					if (!SearchListTextBox.Text.ToString().Equals(string.Empty))
						SearchListButton.Enabled = true;
					else
						SearchListButton.Enabled = false;
				}
					
				FilterComboBox.Enabled = true;
				PageNumTextBox.Enabled = true;

				SearchKeywordTextBox.Enabled = true;
				SearchListTextBox.Enabled = true;

				JDSelfCheckBox.Enabled = true;
				CleanComboBox.Enabled = true;

				ShowFileSelectDialog.Enabled = true;
			}
		}

		private void SearchListButton_Click(object sender, EventArgs e)
		{
			FileInfo fi = new FileInfo(SearchListTextBox.Text.ToString());
			if (!fi.Exists)
			{
				MessageBox.Show("非法文件路径", "错误", MessageBoxButtons.OK);
				return;
			}

			m_isBatchSearch = true;
			
			if (SearchListButton.Text.Equals("批量搜索"))
			{
				ResultTextBox.Text = "-------------------------- 程序开始执行！ --------------------------\r\n";
				m_isRunning = true;
				backgroundWorker.RunWorkerAsync();
				CheckButtonStatus();
			}
			else
			{
				m_isRunning = false;
				backgroundWorker.CancelAsync();
				CheckButtonStatus();
			}
		}

		private void AuthorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MessageBox.Show("联系方式：adiemusmx@aliyun.com", "联系方式", MessageBoxButtons.OK);
		}

		private void SearchKeywordTextBox_TextChanged(object sender, EventArgs e)
		{
			CheckButtonStatus();
		}

		private void SearchListTextBox_TextChanged(object sender, EventArgs e)
		{
			CheckButtonStatus();
		}

		private void HelpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MessageBox.Show("1.输入单个搜索词\r\n2.设定筛选规则\r\n3.点击搜索\r\n4.批量搜索时需要指定一个txt文件，以换行符区分。", "使用帮助", MessageBoxButtons.OK);
		}
    }
}
