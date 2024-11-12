using Aspose.Pdf;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BlogManagement.Domain.Constants.CommonConstants;

namespace BlogManagement.Service.Abstract.Helpers
{
	public static class ImportExportHelper<T> where T : class
	{
		public static ExportStream ExportFile(ExportFileVModel exportModel, IEnumerable<T> fileContent)
		{
			string exportType = exportModel.Type;

			return String.Equals(exportType, ExportTypeConstant.EXCEL, StringComparison.OrdinalIgnoreCase)
				? ExportExcel(exportModel.FileName, exportModel.SheetName, fileContent)
				: String.Equals(exportType, ExportTypeConstant.PDF, StringComparison.OrdinalIgnoreCase)
					? ExportPdf(exportModel.FileName, fileContent)
					: String.Equals(exportType, ExportTypeConstant.WORD, StringComparison.OrdinalIgnoreCase)
						? ExportWord(exportModel.FileName, fileContent)
						: null;
		}


		public static ExportStream ExportExcel(string fileName, string sheetName, IEnumerable<T> fileContent, Action<ExcelPackage, string, IEnumerable<T>> delegateAction = null)
		{
			if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(sheetName)) return null;
			try
			{
				ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
				var stream = new MemoryStream();
				using (var package = new ExcelPackage(stream))
				{
					if (delegateAction == null)
					{
						ExportDefault(package, sheetName, fileContent);
					}
					else
					{
						delegateAction(package, sheetName, fileContent);
					}
				}
				stream.Position = 0;

				return new ExportStream()
				{
					FileName = $"{fileName}{CommonConstants.Excel.fileNameExtention}",
					Stream = stream,
					ContentType = Excel.openxmlformats
				};
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static ExportStream ExportWord(string fileName, IEnumerable<T> fileContent)
		{
			var objectType = typeof(T);
			var headers = GetHeaders(objectType);
			var rows = objectType.GetProperties().Select(p => p.Name).ToList();

			using (var document = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
			{
				var mainPart = document.AddMainDocumentPart();
				mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(new Body());

				var body = mainPart.Document.Body;

				var table = new DocumentFormat.OpenXml.Wordprocessing.Table();

				var tableProperties = new TableProperties(
					new TableBorders(
						new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
						new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
						new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
						new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
						new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
						new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
					)
				);

				table.AppendChild(tableProperties);

				var headerRow = new TableRow();
				foreach (var header in headers)
				{
					headerRow.Append(CreateTableCell(header));
				}
				table.Append(headerRow);

				foreach (var item in fileContent)
				{
					var dataRow = new TableRow();
					for (int i = 0; i < rows.Count; i++)
					{
						var rowData = Convert.ToString(objectType.GetProperty(rows[i]).GetValue(item));

						if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
						objectType.GetProperty(rows[i]).GetValue(item).GetType() == typeof(DateTime))
						{
							DateTime date = (DateTime)objectType.GetProperty(rows[i]).GetValue(item);
							rowData = date.ToString("dd-MM-yyyy");
						}
						else if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
							objectType.GetProperty(rows[i]).GetValue(item).GetType() == typeof(decimal))
						{
							decimal number = (decimal)objectType.GetProperty(rows[i]).GetValue(item);
							NumberFormatInfo numberFormatInfo =
								(NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
							numberFormatInfo.NumberDecimalSeparator = ",";
							numberFormatInfo.NumberGroupSeparator = ".";
							rowData = string.Format(numberFormatInfo, "{0:n}", number);
						}
						else if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
						  Regex.IsMatch(objectType.GetProperty(rows[i]).GetValue(item).ToString(), @"<[^>]*>"))
						{
							rowData = Regex.Replace(objectType.GetProperty(rows[i]).GetValue(item).ToString(), @"<[^>]+>|&nbsp;", "");
						}

						dataRow.Append(CreateTableCell(rowData));
					}
					table.Append(dataRow);
				}

				foreach (var cell in table.Descendants<TableCell>())
				{
					if (string.IsNullOrEmpty(cell.InnerText))
					{
						Paragraph newParagraph = new Paragraph(new Run(new Text("NULL")));
						cell.RemoveAllChildren<Paragraph>();
						cell.Append(newParagraph);
					}
					TableCellMargin margin = new TableCellMargin();
					TopMargin topMargin = new TopMargin() { Width = "50" };
					BottomMargin bottomMargin = new BottomMargin() { Width = "50" };
					LeftMargin leftMargin = new LeftMargin() { Width = "80" };
					RightMargin rightMargin = new RightMargin() { Width = "80" };
					TableCellWidth cellWidth = new TableCellWidth() { Width = "1000", Type = TableWidthUnitValues.Dxa };
					margin.Append(topMargin, bottomMargin, leftMargin, rightMargin);

					TableCellProperties cellProperties = new TableCellProperties(margin, cellWidth);
					cell.Append(cellProperties);

					foreach (var paragraph in cell.Descendants<Paragraph>())
					{
						ParagraphProperties paragraphProperties = paragraph.ParagraphProperties ?? new ParagraphProperties();
						paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "0", After = "0" };
						paragraph.ParagraphProperties = paragraphProperties;
						foreach (var run in paragraph.Descendants<Run>())
						{
							run.RunProperties = new RunProperties(new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = "7" });
						}
					}
				}

				body.Append(table);
			}

			var stream = new MemoryStream(File.ReadAllBytes(fileName));
			return new ExportStream
			{
				FileName = $"{fileName}{CommonConstants.Word.fileNameExtention}",
				Stream = stream,
				ContentType = Word.format
			};
		}

		private static TableCell CreateTableCell(string text)
		{
			var tableCell = new TableCell(new Paragraph(new Run(new Text(text))));
			tableCell.TableCellProperties = new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center });
			return tableCell;
		}

		public static void ExportDefault(ExcelPackage package, string sheetName, IEnumerable<T> fileContent)
		{
			var objectType = typeof(T);
			var properties = objectType.GetProperties();
			var headers = GetHeaders(objectType);
			var rows = properties.Select(p => p.Name).ToList();

			// name of the sheet
			var workSheet = package.Workbook.Worksheets.Add(sheetName);

			// Header of the Excel sheet
			for (int i = 0; i < headers.Count; i++)
			{
				workSheet.Cells[1, i + 1].Value = headers[i];
			}

			// Inserting the article data into excel
			// sheet by using the for each loop
			// As we have values to the first row
			// we will start with second row
			int recordIndex = 2;
			foreach (var item in fileContent)
			{
				for (int i = 0; i < rows.Count; i++)
				{
					var value = objectType.GetProperty(rows[i]).GetValue(item);
					if (value != null && Regex.IsMatch(value.ToString(), @"<[^>]*>"))
					{
						value = Regex.Replace(value.ToString(), @"<[^>]+>", "");
					}
					workSheet.Cells[recordIndex, i + 1].Value = value;
					if (value != null && value.GetType() == typeof(DateTime))
					{
						workSheet.Cells[recordIndex, i + 1].Style.Numberformat.Format = "dd-MM-yyyy";
					}
				}
				recordIndex++;
			}

			workSheet.Cells.AutoFitColumns();
			package.Save();
		}

		public static ExportStream ExportPdf(string fileName, IEnumerable<T> fileContent)
		{
			var objectType = typeof(T);
			var headers = GetHeaders(objectType);
			var rows = objectType.GetProperties().Select(p => p.Name).ToList();

			var document = new Aspose.Pdf.Document
			{
				PageInfo = new PageInfo
				{
					Margin = new MarginInfo(28, 40, 28, 40)
				}
			};
            Page page = document.Pages.Add();
			Aspose.Pdf.Table table = new()
			{
				ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
				DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
				Border = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.Black),
				DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Aspose.Pdf.Color.Black),
			};
			table.DefaultCellTextState.FontSize = 4;
			var headerRow = table.Rows.Add();
			foreach (string header in headers)
			{
				headerRow.Cells.Add(header);
			}

			foreach (var item in fileContent)
			{
				var dataRow = table.Rows.Add();
				for (int i = 0; i < rows.Count; i++)
				{
					var rowData = Convert.ToString(objectType.GetProperty(rows[i]).GetValue(item));

					if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
						objectType.GetProperty(rows[i]).GetValue(item).GetType() == typeof(DateTime))//format datetime
					{
						DateTime date = (DateTime)objectType.GetProperty(rows[i]).GetValue(item);
						rowData = date.ToString("dd-MM-yyyy");
					}
					else if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
						objectType.GetProperty(rows[i]).GetValue(item).GetType() == typeof(decimal))// format number
					{
						decimal number = (decimal)objectType.GetProperty(rows[i]).GetValue(item);
						NumberFormatInfo numberFormatInfo =
							(NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
						numberFormatInfo.NumberDecimalSeparator = ",";
						numberFormatInfo.NumberGroupSeparator = ".";
						rowData = string.Format(numberFormatInfo, "{0:n}", number);
					}
					else if (objectType.GetProperty(rows[i]).GetValue(item) != null &&
					  Regex.IsMatch(objectType.GetProperty(rows[i]).GetValue(item).ToString(), @"<[^>]*>"))
					{
						rowData = Regex.Replace(objectType.GetProperty(rows[i]).GetValue(item).ToString(), @"<[^>]+>|&nbsp;", "");
					}
					dataRow.Cells.Add(rowData);
				}
			}

			page.Paragraphs.Add(table);
			document.PageInfo.IsLandscape = true;
			var stream = new MemoryStream();
			document.Save(stream);
			stream.Position = 0;

			return new ExportStream
			{
				FileName = $"{fileName}{CommonConstants.Pdf.fileNameExtention}",
				Stream = stream,
				ContentType = Pdf.format
			};
		}


		private static List<string> GetHeaders(Type type)
		{
			var properties = type.GetProperties();
			var headers = new List<string>();
			foreach (var property in properties)
			{
				var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), false);
				foreach (DataMemberAttribute dma in attributes.Cast<DataMemberAttribute>())
				{
					if (!string.IsNullOrEmpty(dma.Name))
					{
						headers.Add(dma.Name);
					}
				}
			}
			return headers;
		}

		public static List<dynamic> Import(string pathFile)
		{
			var resultObject = new List<dynamic>();
			using (ExcelPackage package = new ExcelPackage(new FileInfo(pathFile)))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				var rowCount = worksheet.Dimension.End.Row;
				var colCount = worksheet.Dimension.End.Column;
				for (int row = 2; row <= rowCount; row++)
				{
					var dataRow = new ExpandoObject() as IDictionary<string, object>;
					for (int col = 1; col <= colCount; col++)
					{
						var fieldName = worksheet.Cells[1, col].Value.ToString().Trim();
						var valueCell = worksheet.Cells[row, col].Value;
						dataRow.Add(fieldName, valueCell);
					}
					resultObject.Add((ExpandoObject)dataRow);
				}
			}
			return resultObject;
		}
		//public static IEnumerable<T> ImportWord<T>(string fileName) where T : new()
		//{
		//    var result = new List<T>();

		//    using (var document = WordprocessingDocument.Open(fileName, false))
		//    {
		//        var body = document.MainDocumentPart.Document.Body;
		//        var table = body.Descendants<Table>().FirstOrDefault();

		//        if (table != null)
		//        {
		//            var headers = table.Descendants<TableRow>().First()
		//                               .Descendants<TableCell>()
		//                               .Select(cell => cell.InnerText.Trim())
		//                               .ToList();

		//            foreach (var row in table.Descendants<TableRow>().Skip(1))
		//            {
		//                var item = new T();
		//                var properties = typeof(T).GetProperties();

		//                for (int i = 0; i < headers.Count && i < properties.Length; i++)
		//                {
		//                    var cellValue = row.Descendants<TableCell>().ElementAt(i).InnerText.Trim();
		//                    var property = properties[i];

		//                    // Convert cellValue to the appropriate type if necessary
		//                    if (!string.IsNullOrEmpty(cellValue))
		//                    {
		//                        if (property.PropertyType == typeof(DateTime))
		//                        {
		//                            if (DateTime.TryParseExact(cellValue, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
		//                            {
		//                                property.SetValue(item, date);
		//                            }
		//                        }
		//                        else if (property.PropertyType == typeof(decimal))
		//                        {
		//                            // Remove thousand separators and replace comma decimal separator with dot
		//                            cellValue = Regex.Replace(cellValue, @"\.(?=\d{3})", "").Replace(",", ".");
		//                            if (decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
		//                            {
		//                                property.SetValue(item, number);
		//                            }
		//                        }
		//                        else
		//                        {
		//                            // Default case: assuming string type
		//                            property.SetValue(item, cellValue);
		//                        }
		//                    }
		//                }

		//                result.Add(item);
		//            }
		//        }
		//    }

		//    return result;
		//}
	}
}
