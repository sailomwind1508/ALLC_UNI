using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace AllCashUFormsApp
{
    public static class ConvertHelper
    {
        public static DateTime ConvertThaiDate(DateTime date)
        {
            DateTime dt;

            if (CultureInfo.CurrentCulture.Name != "th-TH")
            {
                dt = date.AddYears(543);
            }
            else
            {
                dt = date;
            }

            return dt;

        }

        public static void CopyTo(this object S, object T)
        {
            foreach (var pS in S.GetType().GetProperties())
            {
                foreach (var pT in T.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(T, new object[]
                    { pS.GetGetMethod().Invoke( S, null ) });
                }
            };
        }

        public static string ConvertEmpToTis620(string file)
        {
            string text = "";

            Encoding currentEncoding;
            bool convertFalg = true;
            using (var reader = new StreamReader(file, Encoding.Default, true))
            {
                reader.Peek(); // you need this!
                currentEncoding = reader.CurrentEncoding;

                string param = "Thai (Windows)";
                if (reader.CurrentEncoding.EncodingName.Trim() == param.Trim())
                {
                    convertFalg = false;
                }
            }

            text = File.ReadAllText(file, Encoding.Default);
            if (convertFalg)
            {
                text = EncodeString(text);
            }
            return text;
        }

        public static string EncodeString(string Str)
        {

            System.Text.Encoding latinEnc = System.Text.Encoding.GetEncoding("iso-8859-1");

            System.Text.Encoding thaiEnc = System.Text.Encoding.GetEncoding("TIS-620");

            byte[] bytes = latinEnc.GetBytes(Str);

            string textResult = thaiEnc.GetString(bytes);

            return textResult;

        }

        public static byte[] ToTIS620(string utf8String)
        {
            List<byte> buffer = new List<byte>();
            byte utf8Identifier = 224;
            for (var i = 0; i < utf8String.Length; i++)
            {
                string utf8Char = utf8String.Substring(i, 1);
                byte[] utf8CharBytes = Encoding.UTF8.GetBytes(utf8Char);
                if (utf8CharBytes.Length > 1 && utf8CharBytes[0] == utf8Identifier)
                {
                    var tis620Char = (utf8CharBytes[2] & 0x3F);
                    tis620Char |= ((utf8CharBytes[1] & 0x3F) << 6);
                    tis620Char |= ((utf8CharBytes[0] & 0x0F) << 12);
                    tis620Char -= (0x0E00 + 0xA0);
                    byte tis620Byte = (byte)tis620Char;
                    tis620Byte += 0xA0;
                    tis620Byte += 0xA0;

                    buffer.Add(tis620Byte);
                }
                else
                {
                    buffer.Add(utf8CharBytes[0]);
                }
            }
            return buffer.ToArray();
        }

        public static byte[] ToUTF8(byte[] tis620Bytes)
        {
            List<byte> buffer = new List<byte>();
            byte safeAscii = 126;
            for (var i = 0; i < tis620Bytes.Length; i++)
            {
                if (tis620Bytes[i] > safeAscii)
                {
                    if (((0xa1 <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xda))
                        || ((0xdf <= tis620Bytes[i]) && (tis620Bytes[i] <= 0xfb)))
                    {
                        var utf8Char = 0x0e00 + tis620Bytes[i] - 0xa0;

                        byte utf8Byte1 = (byte)(0xe0 | (utf8Char >> 12));
                        buffer.Add(utf8Byte1);
                        byte utf8Byte2 = (byte)(0x80 | ((utf8Char >> 6) & 0x3f));
                        buffer.Add(utf8Byte2);
                        byte utf8Byte3 = (byte)(0x80 | (utf8Char & 0x3f));
                        buffer.Add(utf8Byte3);
                    }
                }
                else
                {
                    buffer.Add(tis620Bytes[i]);
                }
            }
            return buffer.ToArray();
        }

        public static byte[] ImageToByte(this Image img, int width, int height)
        {
            try
            {
                if (img == null)
                {
                    return null;
                }

                ImageConverter converter = new ImageConverter();
                byte[] bytes = new byte[0];
                bytes = (byte[])converter.ConvertTo(img, typeof(byte[]));
                return bytes.Resize(width, height);
            }
            catch (Exception)
            {
                byte[] bytesArr = new byte[0];
                using (MemoryStream mms = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(mms, ImageFormat.Png);
                    bytesArr = mms.ToArray().Resize(width, height);
                }

                return bytesArr;
            }
        }

        public static byte[] ImageToByte(this Image img)
        {
            try
            {
                if (img == null)
                {
                    return null;
                }

                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
            catch (Exception)
            {
                using (MemoryStream mms = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(mms, ImageFormat.Png);
                    return mms.ToArray();
                }
            }
        }

        public static Image byteArrayToImage(this byte[] bytesArr, int width, int height)
        {
            if (bytesArr == null)
                return null;

            using (MemoryStream memstr = new MemoryStream(bytesArr.Resize(width, height)))
            {
                Image img = Image.FromStream(memstr);
                Bitmap myBitmap = new Bitmap(img);
                return myBitmap;
            }
        }

        public static Image byteArrayToImage(this byte[] bytesArr)
        {
            if (bytesArr == null)
                return null;

            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        public static byte[] Resize(this byte[] data, int width, int height)
        {
            using (var stream = new MemoryStream(data))
            {
                var image = Image.FromStream(stream);

                //var height = (width * image.Height) / image.Width;
                var thumbnail = image.GetThumbnailImage(width, height, null, IntPtr.Zero);

                using (var thumbnailStream = new MemoryStream())
                {
                    thumbnail.Save(thumbnailStream, ImageFormat.Jpeg);
                    return thumbnailStream.ToArray();
                }
            }
        }

        public static void ToNumberOnly(this KeyPressEventArgs e, object sender)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data.ToList())
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTable(this List<Dictionary<string, int>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            var columnNames = list.SelectMany(dict => dict.Keys).Distinct();
            result.Columns.AddRange(columnNames.Select(c => new DataColumn(c)).ToArray());
            foreach (Dictionary<string, int> item in list)
            {
                var row = result.NewRow();
                foreach (var key in item.Keys)
                {
                    row[key] = item[key];
                }

                result.Rows.Add(row);
            }

            return result;
        }

        public static DataTable ToDataTable_2<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name], null);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static List<dynamic> GetListFromDT(Type className, DataTable dataTable)
        {
            List<dynamic> list = new List<dynamic>();
            foreach (DataRow row in dataTable.Rows)
            {
                object objClass = Activator.CreateInstance(className);
                Type type = objClass.GetType();
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = type.GetProperty(column.ColumnName);
                    //prop.SetValue(objClass, row[column.ColumnName], null);
                    object safeValue = (row[column.ColumnName] == null || row[column.ColumnName] == DBNull.Value) ? null : row[column.ColumnName];

                    if (prop != null)
                        prop.SetValue(objClass, safeValue, null);
                }
                list.Add(objClass);
            }
            return list;
        }

        public static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            Type t = Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType;
                            object safeValue = (dr[column.ColumnName] == null || dr[column.ColumnName] == DBNull.Value) ? null : Convert.ChangeType(dr[column.ColumnName], t);

                            pro.SetValue(obj, safeValue, null);
                        }
                        else
                            continue;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static string ToNumberFormat(this int number)
        {
            return string.Format("{0:#,0}", number);
        }

        public static string ToMoneyFormat(this int number)
        {
            return string.Format("{0:#,0.00}", number);
        }

        public static decimal ToDecimalN0(this decimal number)
        {
            string _number = string.Format("{0:#,0}", number);
            return Convert.ToDecimal(_number);
        }

        public static decimal ToDecimalN2(this decimal number)
        {
            string _number = string.Format("{0:#,0.00}", number);
            return Convert.ToDecimal(_number);
        }

        public static decimal ToDecimalN3(this decimal number)
        {
            string _number = string.Format("{0:#,0.000}", number);
            return Convert.ToDecimal(_number);
        }

        public static decimal ToDecimalN5(this decimal number)
        {
            string _number = string.Format("{0:#,0.00000}", number);
            return Convert.ToDecimal(_number);
        }

        public static string ToStringN2(this decimal number)
        {
            return string.Format("{0:#,0.00}", number);
        }

        public static string ToStringN0(this decimal number)
        {
            return string.Format("{0:#,0}", number);
        }

        public static string ToStringN5(this decimal number)
        {
            return string.Format("{0:#,0.00000}", number);
        }

        public static DateTime ToDateTimeFormat(this DateTime dt)
        {
            //string _tempDt = dt.ToString("dd/MM/yyyy");
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        public static string ToDateTimeFormatString(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        public static List<KeyValuePair<string, int>> ConvertRowsToList(DataTable input)
        {
            var ret = new List<KeyValuePair<string, int>>();

            foreach (DataRow dr in input.Rows)
                ret.Add(new KeyValuePair<string, int>((string)dr["Name"], (int)dr["Marks"]));

            return ret;
        }

        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable<T>(this DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        /// <summary>
        /// selectValue => Predicate<Program> test = delegate (Program p) { return p.age > 3; }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ddl"></param>
        /// <param name="obj"></param>
        /// <param name="selectValue">;</param>
        public static void SetDateTimePickerFormat(this DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
        }
    }
}
