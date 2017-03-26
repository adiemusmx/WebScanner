using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WebPictureScanner
{
	class GoodInfo
	{
		public string m_title = "";
		public string m_picture = "";
		public bool m_jdGood = false;
		public float m_price = 0;
	}

	class WebAnalysisException : Exception
	{
		public WebAnalysisException(string errInfo)
		{
			m_errInfo = errInfo;
		}

		// 返回错误信息
		public string toString()
		{
			return m_errInfo;
		}

		private string m_errInfo = "";
	}

    class WebAnalysis
	{
		// 将会抛出WebException，WebAnalysisException
		public void analysisUrl(string url)
		{ 
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

			// 如果不指定这个数据，网站返回的数据将会不正常
			request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
//			request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
//			request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1) Gecko/20070803 Firefox/1.5.0.12";
//			request.UserAgent = "Opera/9.27 (Windows NT 5.2; U; zh-cn)";

			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

			// 从指定的网站下载数据
			m_pageContent = reader.ReadToEnd();

			// 只保留搜索结果部分
			m_goodsList = trimGoodsList(m_pageContent);

			getGoodInfoList();

			reader.Close();
			response.Close();
		}

		// 将内容中无关的数据抛弃，保留搜索结果部分
		private string trimGoodsList(string content)
		{
			int startIndex = content.IndexOf("<div id=\"J_goodsList\"");
			int endIndex = content.IndexOf("<div id=\"J_scroll_loading\"");
			if (startIndex == -1 || endIndex == -1)
				throw new WebAnalysisException("网页数据缺少商品列表关键元素，可能是网页设计发生变更，需要更新软件。");
			return content.Substring(startIndex, endIndex - startIndex); ;
		}

		private List<string> getGoodInfoRawContent(string content)
		{
			List<string> result = new List<string>();
			string tmpContent = content;

			while(true)
			{
				int startIndex = tmpContent.IndexOf("<div class=\"gl-i-wrap\">");
				int length = tmpContent.Substring(startIndex + 23).IndexOf("<div class=\"gl-i-wrap\">");
				//int endIndex = tmpContent.IndexOf("<div class=\"gl-i-wrap\">");
				if (startIndex == -1)
					break;
				if (length == -1)
					length = tmpContent.Substring(startIndex + 23).Length;
				string data = tmpContent.Substring(startIndex, length);
				tmpContent = tmpContent.Substring(startIndex + length);
				result.Add(data);
			}
			
			return result;
		}

		private static string JD_SELF = "//img14.360buyimg.com/uba/jfs/t3139/175/3796130719/1386/3a9cc545/57f8ac4fN87e531d5.png";

		private GoodInfo convertGoodInfo(string content)
		{
			GoodInfo result = new GoodInfo();

			string tmpContent = content;

			// Title
			int titleStartIndex = tmpContent.IndexOf("<a target=\"_blank\" title=\"") + 26;
			int titleLength = tmpContent.Substring(titleStartIndex).IndexOf("\"");
			result.m_title = tmpContent.Substring(titleStartIndex, titleLength);

			// Picture，部分商品存在src和data-lazy-img，取出现较早的一个
			int picStartIndex;
			int picSrcStartIndex = tmpContent.IndexOf("src=");
			int picLazyStartIndex = tmpContent.IndexOf("data-lazy-img=");
			if (picSrcStartIndex == -1 || (picLazyStartIndex != -1 && picSrcStartIndex > picLazyStartIndex))
				picStartIndex = picLazyStartIndex + 15;
			else
				picStartIndex = picSrcStartIndex + 5;
			int picLength = tmpContent.Substring(picStartIndex).IndexOf("\"");
			result.m_picture = tmpContent.Substring(picStartIndex, picLength);

			// JD Good
			result.m_jdGood = tmpContent.Contains(JD_SELF);

			// Price
			if (tmpContent.Contains("data-price"))
			{
				int priceStartIndex = tmpContent.IndexOf("data-price=") + 12;
				int priceLength = tmpContent.Substring(priceStartIndex).IndexOf("\">");
				result.m_price = float.Parse(tmpContent.Substring(priceStartIndex, priceLength));
			}
			else
			{
				result.m_price = 0.0f;
			}

			return result;
		}

		// 获得图片的配对信息
		public List<GoodInfo> getGoodInfoList()
		{
			List<GoodInfo> result = new List<GoodInfo>();

			List<string> rawContent = getGoodInfoRawContent(m_goodsList);
			foreach (string content in rawContent)
			{
				GoodInfo info = convertGoodInfo(content);
				result.Add(info);
			}

			return result;
		}

		public void savePicture(string savePath, string fileName)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http:" + fileName);

			req.ServicePoint.Expect100Continue = false;
			req.Method = "GET";
			req.KeepAlive = true;

			req.ContentType = "image/png";
			HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

			System.IO.Stream stream = null;
			savePath = Directory.GetCurrentDirectory() + '/' + savePath;
			DirectoryInfo di = new DirectoryInfo(savePath);
			if (!di.Exists)
				di.Create();

			try
			{
				// 以字符流的方式读取HTTP响应
				stream = rsp.GetResponseStream();
				Image.FromStream(stream).Save(savePath + fileName.Substring(fileName.LastIndexOf('/') + 1));
			}
			finally
			{
				// 释放资源
				if (stream != null) stream.Close();
				if (rsp != null) rsp.Close();
			}
		}

		public string m_pageContent = "";
		public string m_goodsList = "";
		public GoodInfo m_pictureInfoList = null;
	}
}
