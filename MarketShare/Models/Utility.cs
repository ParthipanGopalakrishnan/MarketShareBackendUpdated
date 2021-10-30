namespace MarketShare.Models
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="Utility" />.
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Defines the _authData.
        /// </summary>
        private readonly AuthData.AuthData _authData = new AuthData.AuthData();

        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The FormatNumber.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="number">The number<see cref="T"/>.</param>
        /// <param name="maxDecimals">The maxDecimals<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string FormatNumber<T>(T number, int maxDecimals = 4) => Regex.Replace(String.Format("{0:n" + maxDecimals + "}", number),
                                    @"[" + System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");

        /// <summary>
        /// The WriteTsv.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="data">The data<see cref="IEnumerable{T}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string WriteTsv<T>(IEnumerable<T> data)
        {
            StringBuilder output = new StringBuilder();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in props)
            {
                output.Append(prop.DisplayName); // header
                output.Append("\t");
            }
            output.AppendLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Append(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Append("\t");
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        /// <summary>
        /// The CreateHeader.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="List{T}"/>.</param>
        /// <param name="sw">The sw<see cref="StreamWriter"/>.</param>
        private void CreateHeader<T>(List<T> list, StreamWriter sw)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    sw.Write(properties[i].Name + ",");
                }
                var lastProp = properties[properties.Length - 1].Name;
                sw.Write(sw.NewLine);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CreateHeader" + ex.StackTrace);
            }
        }

        /// <summary>
        /// The CreateRows.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="List{T}"/>.</param>
        /// <param name="sw">The sw<see cref="StreamWriter"/>.</param>
        private void CreateRows<T>(List<T> list, StreamWriter sw)
        {
            try
            {
                foreach (var item in list)
                {
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    var tst = list.Select(z => z.GetType()).ToList();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var prop = properties[i];
                        sw.Write(prop.GetValue(item) + ",");
                    }
                    var lastProp = properties[properties.Length - 1];
                    sw.Write(lastProp.GetValue(item) + sw.NewLine);
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CreateRows" + ex.StackTrace);
            }
        }
    }
}
