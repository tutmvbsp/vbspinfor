using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Globalization;
using System.IO;
using System.Data;
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Net.Sockets;
using PQScan.PDFToText;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using DAL;

namespace BLL
{
    public class ToolBll
    {
        // kiem tra da ton tai du lieu hay chua
        public bool ExitsData(string strsql)
        {
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            DataTable dt = new DataTable();
            string str = strsql;
            dt = cls.LoadDataText(str);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KtrNgay(string ng)
        {
            if (ng.Trim() == "")
                return false;
            else
                return true;
        }

        public bool KtrCbo(string strcbo)
        {
            if (strcbo == "")
                return false;
            else
                return true;
        }

        //xay dung cac ham
        public string Left(string str, int n)
        {
            string result = str.Substring(0, n);
            return result;
        }

        public string Right(string str, int n)
        {
            string result = str.Substring(str.Length - n, n);
            return result;
        }


        public string XoaKyTuTrang(string str)
        {
            //ham xoa ky tu trang trong chuoi
            Regex r = new Regex(@"\s+");
            return r.Replace(str, @" ");
        }

        public string KyTuHoaDau(string str)
        {
            //ham chuyen ky tu dau tien thanh chu hoa
            Regex r = new Regex(@"(?<=\w)\w");
            return Regex.Replace(str, @"\b\w", (Match match) => match.ToString().ToUpper());
        }

        public bool KyTuDacBiet(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < 48 || (str[i] > 57 && str[i] < 65) || (str[i] > 90 && str[i] < 97) || str[i] > 122)
                {
                    return true;
                }
            }
            return false;
        }

        public string XoaHetKyTuTrang(string str)
        {
            str = str.Trim();
            while (str.Contains(" "))
                str = str.Replace(" ", "");
            return str;
        }

        public string ChuThuong(string str)
        {
            //ham chuyen ky tu dau tien thanh chu hoa
            Regex r = new Regex(@"(?<=\w)\w");
            return r.Replace(str, new MatchEvaluator(m => m.Value.ToLowerInvariant()));
            //Regex.Replace(str, @"\b\w", (Match match) => match.ToString().ToUpper());
        }

        public bool CheckUserPass(string str)
        {
            if (str == "")
            {
                return false;
            }
            char[] varChar = str.ToCharArray();
            int i = 0;
            while (i < varChar.Length &&
                   ((Convert.ToInt32(varChar[i]) >= 97 && Convert.ToInt32(varChar[i]) <= 122)
                    || (Convert.ToInt32(varChar[i]) >= 48 && Convert.ToInt32(varChar[i]) <= 57)))
            {
                i++;
            }
            if (i < varChar.Length)
            {
                return false;
            }
            return true;
        }

        public string Convert_AV(string str)
        {
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = str.Normalize(NormalizationForm.FormD);
            return v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public string ConvertUpperLower(string str)
            //Hàm chuyển ký tự hoa sang thương và thường sang hoa ví dụ Tu thành tU
        {
            string result = "";
            foreach (char c in str)
            {
                string s = c.ToString(CultureInfo.InvariantCulture);
                if (s == s.ToLower())
                    result += s.ToUpper();
                else
                    result += s.ToLower();
            }
            return result;
        }

        public int KiemTraKyTuTv(string str)
        {
            int dem = 0;
            const string strTv =
                "áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                char kytu = str[i];
                if (strTv.IndexOf(kytu) >= 0)
                    dem = dem + 1;

            }
            return dem;
        }

        //--------------------------------------------
        public string Encrypt(string value)
        {

            if (string.IsNullOrEmpty(value))

                return string.Empty;

            var md5 = new MD5CryptoServiceProvider();

            byte[] valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = md5.ComputeHash(valueArray);

            var sb = new StringBuilder();

            for (int i = 0; i < valueArray.Length; i++)
                sb.Append(valueArray[i].ToString(
                    "x2", CultureInfo.CurrentCulture)
                                       .ToLower(
                                           CultureInfo.CurrentCulture));

            return sb.ToString();
        }

        //---------------------------------------------------
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("tutm"));
            }
            else keyArray = Encoding.UTF8.GetBytes("tutm");
            var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string toDecrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("tutm"));
            }
            else keyArray = Encoding.UTF8.GetBytes("tutm");
            var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        //--------------------
        public void exportDataTableToExcel(DataTable dt, string filePath)
        {

            // Excel file Path

            string myFile = filePath;

            //System.Data.DataRow dr = default(System.Data.DataRow);

            int colIndex = 0;
            int rowIndex = 0;

            // Open the file and write the headers
            StreamWriter fs = new StreamWriter(myFile, false);

            fs.WriteLine("<? xml version=\"1.0\"?>");
            fs.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            fs.WriteLine("<ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");

            // Create the styles for the worksheet
            fs.WriteLine(" <ss:Styles>");
            // Style for the column headers
            fs.WriteLine(" <ss:Style ss:ID=\"1\">");
            fs.WriteLine(" <ss:Font ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
            fs.WriteLine(" <ss:Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" " + "ss:WrapText=\"1\"/>");
            fs.WriteLine(" <ss:Interior ss:Color=\"#254117\" ss:Pattern=\"Solid\"/>");
            fs.WriteLine(" </ss:Style>");
            // Style for the column information
            fs.WriteLine(" <ss:Style ss:ID=\"2\">");
            fs.WriteLine(" <ss:Alignment ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            fs.WriteLine(" </ss:Style>");
            // Style for the column headers
            fs.WriteLine(" <ss:Style ss:ID=\"3\">");
            fs.WriteLine(" <ss:Font ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
            fs.WriteLine(" <ss:Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" " + "ss:WrapText=\"1\"/>");
            fs.WriteLine(" <ss:Interior ss:Color=\"#736AFF\" ss:Pattern=\"Solid\"/>");
            fs.WriteLine(" </ss:Style>");
            fs.WriteLine(" </ss:Styles>");


            // Write the worksheet contents
            fs.WriteLine("<ss:Worksheet ss:Name=\"Sheet1\">");
            fs.WriteLine(" <ss:Table>");

            fs.WriteLine(" <ss:Row>");
            foreach (DataColumn dc in dt.Columns)
            {

                fs.WriteLine(
                    string.Format(
                        " <ss:Cell ss:StyleID=\"1\">" + "<ss:Data ss:Type=\"String\">{0}</ss:Data></ss:Cell>",
                        dc.ColumnName));
            }

            fs.WriteLine(" </ss:Row>");


            object cellText = null;

            // Write contents for each cell
            foreach (DataRow dr in dt.Rows)
            {
                rowIndex = rowIndex + 1;
                colIndex = 0;
                fs.WriteLine(" <ss:Row>");
                foreach (DataColumn dc in dt.Columns)
                {
                    cellText = dr[dc];
                    // Check for null cell and change it to empty to avoid error
                    if (cellText == null) cellText = "";
                    fs.WriteLine(string.Format(" <ss:Cell ss:StyleID=\"2\">" +
                                               "<ss:Data ss:Type=\"String\">{0}</ss:Data></ss:Cell>", cellText));
                    colIndex = colIndex + 1;
                }
                fs.WriteLine(" </ss:Row>");
            }

            fs.WriteLine(" <ss:Row>");
            fs.WriteLine(" </ss:Row>");


            // Close up the document
            fs.WriteLine(" </ss:Table>");
            fs.WriteLine("</ss:Worksheet>");
            fs.WriteLine("</ss:Workbook>");
            fs.Close();

        }

        //--------------------

        //-------------
        public bool WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation,string ReporType)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet) excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;


                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount%2 == 0)
                                {
                                    excelCellrange =
                                        excelSheet.Range[
                                            excelSheet.Cells[rowcount, 1],
                                            excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange =
                    excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel


                excelworkBook.SaveAs(saveAsLocation);
                ;
                excelworkBook.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }

        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="HTMLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="IsFontbool"></param>
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode,
                                         System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        //-------------
        public string XoaHetKyTu(string str, string kytu)
        {
            str = str.Trim();
            while (str.Contains(kytu))
                str = str.Replace(kytu, "");
            return str;
        }

        public void WriteText(DataTable dt, String fileName)
        {
            
            System.Text.Encoding encode = System.Text.Encoding.BigEndianUnicode;
           FileStream _fw = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(_fw, encode);
            //TextWriter sw = new StreamWriter(expFile);
            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn col in dt.Columns)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i + 1 < dt.Columns.Count)
                    {
                        //sw.Write(row[col].ToString() + "#");
                        sw.Write(row[i].ToString() + "#");
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
        }
        public void WriteToText(string str, String fileName)
        {

            System.Text.Encoding encode = System.Text.Encoding.BigEndianUnicode;
            FileStream _fw;
            if (File.Exists(fileName))
            {
                 _fw = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None);
            }
            else
            {
                 _fw = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            }
            StreamWriter sw = new StreamWriter(_fw, encode);
            //TextWriter sw = new StreamWriter(expFile);
   
            sw.Write(str + "#");
//                sw.WriteLine();
            sw.Close();
        }

        public void TaoThuMuc(String strDir)
        {
            try
            {
                // Bước 1: tạo biến để lưu thư mục cần tạo, tên thư mục cần tạo là "StoredFiles"
                string directoryPath = strDir;
                // Bước 2: kiểm tra nếu thư mục "StoredFiles" chưa tồn tại thì tạo mới
                if (!System.IO.Directory.Exists(directoryPath))
                    System.IO.Directory.CreateDirectory(directoryPath);
                // Bước 4: tạo tập tin "EmployeeList.txt" trong thư mục "StoredFiles"
                //string filePath = directoryPath + @"\EmployeeList.txt";
                //System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create);

                // Kết thúc: thông báo tạo tập tin thành công
                // và chỉ ra đường dẫn tập tin để người dùng dễ dàng kiểm tra tập tin vừa tạo
                //string mesage = "Tạo tập tin \"EmployeeList.txt\" thành công." + Environment.NewLine;
                //mesage += "Đường dẫn là \"" + Application.StartupPath + @"\" + directoryPath + filePath + "\"";
                //MessageBox.Show(mesage, "Thông báo");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }
        public void ExportDTToExcel(DataTable dt, string FileName)
        {

          //  Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;

            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

            // Headers. 

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                ws.Cells[1, i] = dt.Columns[i].ColumnName;
            }

            // Content. 

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 1; j < dt.Columns.Count; j++)
                {

                    ws.Cells[i + 2, j] = dt.Rows[i][j].ToString();

                }

            }

            // Lots of options here. See the documentation. 

            wb.SaveAs(FileName);


            wb.Close();

            app.Quit();

        }
        public void ToCSV(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                List<string> headerValues = new List<string>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerValues.Add(QuoteValue(column.ColumnName));
                }

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }
            string[] items = null;
            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
            writer.Close();
        }
        private static string QuoteValue(string value)
        {
            return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
        }
        public void ExportToExcel(DataTable Tbl, string ExcelFilePath)
        {
            try
            {
                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();
                        //MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        public void OpenExcel(string path)
        {
            Excel.Application xlsxApp = new Excel.Application();
            xlsxApp.Workbooks.Open(path);
            xlsxApp.Visible=true; //Excel will open, I don't want this.
        }
        public void OpenCsvWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);
            ExcelApp.Visible = true;
        }
        // Read PDF
        public string PdfText(string path)
        {
            PdfReader reader = new PdfReader(path);
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();
            return text;
        }
        public void Pdf2Text(string sourceFile, string destFile)
        {
            PDFExtractor extractor = new PDFExtractor();
            // Load a PDF file.
            extractor.LoadPDF(sourceFile);
            // Convert whole PDF text to txt file.
            extractor.ToTextFile(destFile);
            extractor.Dispose();
        }
        public string ExtractTextFromPdf(string path)
        {
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);
                    string[] theLines = thePage.Split('\n');
                    foreach (var theLine in theLines)
                    {
                        text.AppendLine(theLine);
                    }
                }
                return text.ToString();
            }
        }  
        //End
        //Copy file in folder
        public void CopyDir(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            // Get Files & Copy
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = System.IO.Path.GetFileName(file);
                // ADD Unique File Name Check to Below!!!!
                string dest = System.IO.Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            /*
            // Get dirs recursively and copy files
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = System.IO.Path.GetFileName(folder);
                string dest = System.IO.Path.Combine(destFolder, name);
                CopyDir(folder, dest);
            }
             */ 
        }

        //
        //delete all file in forfer
        public void DeleteAllFile(string path)
        {
            //string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            //foreach (string x in directories) Directory.Delete(x, true);
            foreach (string x in files) File.Delete(x);
        }
        //end

        //----------------------------------------------------------------
        public void BackUpDb(string fileName,string filePath)
        {
                string sqlBackup = "BACKUP DATABASE ["+fileName+"] TO DISK='"+filePath+"'";
                ClsConnectLocal cnn = new ClsConnectLocal();
                cnn.ClsConnect();
                cnn.UpdateDataText(sqlBackup);
                cnn.DongKetNoi();
        }
        public void RestoreDb(string fileName, string filePath)
        {
            string sqlRestore = "Use master Restore Database [" + fileName + "] from DISK='" + filePath + "'";
            string drop = "drop database " + fileName;
            ClsConnectLocal cnn = new ClsConnectLocal();
            cnn.ClsConnect();
            cnn.UpdateDataText(drop);
            cnn.UpdateDataText(sqlRestore);
            cnn.DongKetNoi();
        }
        //----------------------------------------------------------------
   
        //------------

        //
    }
}
