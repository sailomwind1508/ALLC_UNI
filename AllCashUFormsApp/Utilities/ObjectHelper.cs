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


namespace AllCashUFormsApp
{
    public static class ObjectHelper
    {
        public static T ConvertTo<T>(object value)
        {
            return ConvertTo(value, default(T));
        }


        public static T ConvertTo<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value)
            {
                return defaultValue;
            }
            return (T)ChangeType(value, typeof(T));
        }


        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }

            // if it's not a nullable type, just pass through the parameters to Convert.ChangeType
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                // null input returns null output regardless of base type
                if (value == null)
                {
                    return null;
                }

                // it's a nullable type, and not null, which means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                conversionType = Nullable.GetUnderlyingType(conversionType);
            }
            else if (conversionType.IsEnum)
            {
                // strings require Parse method
                if (value is string)
                {
                    return Enum.Parse(conversionType, (string)value);
                }
                // primitive types can be instantiated using ToObject
                else if (value is int || value is uint || value is short || value is ushort ||
                     value is byte || value is sbyte || value is long || value is ulong)
                {
                    return Enum.ToObject(conversionType, value);
                }
                else
                {
                    throw new ArgumentException(String.Format("Value cannot be converted to {0} - current type is " +
                                          "not supported for enum conversions.", conversionType.FullName));
                }
            }

            return Convert.ChangeType(value, conversionType);
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
