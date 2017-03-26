using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace WebPictureScanner
{
	class ExcelManager
	{
		public static HSSFWorkbook createExcel()
		{
			return new HSSFWorkbook();
		}

		public static HSSFSheet createSheet(HSSFWorkbook workbook, string sheetName)
		{
			return (HSSFSheet)workbook.CreateSheet(sheetName);
		}

		public static HSSFSheet getSheet(HSSFWorkbook workbook, string sheetName)
		{
			return (HSSFSheet)workbook.GetSheet(sheetName);
		}

		public static HSSFRow getRow(HSSFSheet worksheet, int row)
		{
			int lastRow = worksheet.PhysicalNumberOfRows;
			for (int i = 0; i <= row; ++i)
			{
				if (i < lastRow)
					continue;
				worksheet.CreateRow(i);
			}
			return (HSSFRow)worksheet.GetRow(row);
		}

		public static HSSFCell getCell(HSSFSheet worksheet, int row, int col)
		{
			HSSFRow targetRow = getRow(worksheet, row);
			int lastCol = targetRow.LastCellNum;
			for (int i = 0; i <= col; ++i)
			{
				if (i < lastCol)
					continue;
				targetRow.CreateCell(i);
			}
			return (HSSFCell)targetRow.GetCell(col);
		}

		public static void setCell(HSSFSheet worksheet, int row, int col, string value)
		{
			getCell(worksheet, row, col).SetCellValue(value);
		}

		public static void setPicture(HSSFWorkbook workbook, HSSFSheet worksheet, int row, int col, string fileName)
		{
			try
			{
				byte[] bytes = File.ReadAllBytes(fileName);

				if (!string.IsNullOrEmpty(fileName))
				{
					int pictureIdx = 0;
					if (fileName.EndsWith(".jpg"))
						pictureIdx = workbook.AddPicture(bytes, PictureType.JPEG);
					else if (fileName.EndsWith(".png"))
						pictureIdx = workbook.AddPicture(bytes, PictureType.PNG);
					HSSFPatriarch patriarch = (HSSFPatriarch)worksheet.CreateDrawingPatriarch();
					HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, col, row, col + 1, row + 1);
					//##处理照片位置，【图片左上角为（col, row）第row+1行col+1列，右下角为（ col +1, row +1）第 col +1+1行row +1+1列，宽为50，高为50

					HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void closeExcelWithSave(HSSFWorkbook workbook, string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
			workbook.Write(fileStream);
			fileStream.Close();
			workbook.Close();
		}
	}
}
