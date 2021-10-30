namespace MarketShare.Models
{
    using log4net;
    using NPOI.SS.UserModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="ExcelExportConncection" />.
    /// </summary>
    public class ExcelExportModel : ExcelExport
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
        /// Initializes a new instance of the <see cref="ExcelExportModel"/> class.
        /// </summary>
        public ExcelExportModel()
        {
            _headers = new List<string>();
            _type = new List<string>();
        }

        /// <summary>
        /// The WriteData.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="exportData">The exportData<see cref="List{T}"/>.</param>
        public override void WriteExcelData<T>(List<T> exportData)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                DataTable table = new DataTable();

                foreach (PropertyDescriptor prop in properties)
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    _type.Add(type.Name);
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    //string name = Regex.Replace(prop.DisplayName, "([A-Z])", " $1").Trim(); //space seperated name by caps for header
                    _headers.Add(prop.DisplayName);
                }

                foreach (T item in exportData)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    IRow sheetRow = _sheet.CreateRow(i + 1);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        ICell Row1 = sheetRow.CreateCell(j);
                        string cellvalue = Convert.ToString(table.Rows[i][j]);

                        if (!string.IsNullOrWhiteSpace(cellvalue))
                        {
                            switch (_type[j].ToLower())
                            {
                                case "string":
                                    Row1.SetCellValue(cellvalue);
                                    break;
                                case "int32":
                                    Row1.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                                    break;
                                case "double":
                                    Row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                                    break;
                                case "decimal":
                                    Row1.SetCellValue(Convert.ToDecimal(table.Rows[i][j]).ToString());
                                    break;
                                case "datetime":
                                    Row1.SetCellValue(Convert.ToDateTime(table.Rows[i][j]).ToString("dd/MM/yyyy hh:mm:ss"));
                                    break;
                                default:
                                    Row1.SetCellValue(string.Empty);
                                    break;
                            }
                        }
                        else
                        {
                            Row1.SetCellValue(string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "ExportData" + ex.StackTrace);
            }
        }
    }
}