namespace MarketShare.Models
{
    using log4net;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// Defines the <see cref="IExcelDataExport" />.
    /// </summary>
    public interface IExportExcelData
    {
        /// <summary>
        /// The Export.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="exportData">The exportData<see cref="List{T}"/>.</param>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <param name="sheetName">The sheetName<see cref="string"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        HttpResponseMessage ExportExcelData<T>(List<T> exportData, string fileName, string sheetName);
    }

    /// <summary>
    /// Defines the <see cref="ExcelExport" />.
    /// </summary>
    public abstract class ExcelExport : IExportExcelData
    {
        /// <summary>
        /// Defines the _authData.
        /// </summary>
        private static readonly AuthData.AuthData _authData = new AuthData.AuthData();

        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Defines the _sheetName.
        /// </summary>
        protected string _sheetName;

        /// <summary>
        /// Defines the _fileName.
        /// </summary>
        protected string _fileName;

        /// <summary>
        /// Defines the _headers.
        /// </summary>
        protected List<string> _headers;

        /// <summary>
        /// Defines the _type.
        /// </summary>
        protected List<string> _type;

        /// <summary>
        /// Defines the _workbook.
        /// </summary>
        protected IWorkbook _workbook;

        /// <summary>
        /// Defines the _sheet.
        /// </summary>
        protected ISheet _sheet;

        /// <summary>
        /// Defines the DefaultSheetName.
        /// </summary>
        private const string DefaultSheetName = "PartNumberSheet1";

        /// <summary>
        /// The Export.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="exportData">The exportData<see cref="List{T}"/>.</param>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <param name="sheetName">The sheetName<see cref="string"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        public HttpResponseMessage ExportExcelData<T>(List<T> exportData, string fileName, string sheetName = DefaultSheetName)
        {
            try
            {
                _fileName = fileName;
                _sheetName = sheetName;

                _workbook = new XSSFWorkbook();
                _sheet = _workbook.CreateSheet(_sheetName);

                var headerStyle = _workbook.CreateCellStyle();
                var headerFont = _workbook.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);

                WriteExcelData(exportData);

                //Header
                var header = _sheet.CreateRow(0);
                for (var i = 0; i < _headers.Count; i++)
                {
                    var cell = header.CreateCell(i);
                    cell.SetCellValue(_headers[i]);
                    cell.CellStyle = headerStyle;
                    _sheet.AutoSizeColumn(i);
                }

                using (var memoryStream = new MemoryStream())
                {
                    _workbook.Write(memoryStream);
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(memoryStream.ToArray())
                    };

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = $"{_fileName}.xlsx"
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "ExportData" + ex.StackTrace);
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// Generic Definition to handle all types of List.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="exportData">.</param>
        public abstract void WriteExcelData<T>(List<T> exportData);
    }
}