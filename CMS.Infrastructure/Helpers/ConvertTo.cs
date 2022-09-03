namespace CMS.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    ///  Manage to Convert the Data Type to other Data Type
    /// </summary>
    /// <CreatedBy>Dipal Patel</CreatedBy>
    /// <CreatedDate>30-Aug-2017</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewedBy></ReviewedBy>
    /// <ReviewedDate></ReviewedDate>
    public static class ConvertTo
    {
        #region Methods/Functions

        /// <summary>
        /// Convert object value to string
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>if value=string return string else ""</returns>
        public static string String(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return Convert.ToString(readField, CultureInfo.InvariantCulture);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Trim and Convert object value to string
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>if value=string return string else ""</returns>
        public static string ToStringTrim(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return readField.ToString().Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert object value to double
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>if value=double return double else 0.0</returns>
        public static double ToDouble(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0.0;
                    }
                    else
                    {
                        return Convert.ToDouble(readField, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Convert object value to decimal
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <param name="decimalPlace">decimal places</param>
        /// <returns>if value=double return double else 0.0</returns>
        public static decimal ToDecimal(this object readField, int decimalPlace = -1)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        decimal x;
                        if (decimal.TryParse(readField.ToString(), out x))
                        {
                            x = decimal.Round(x, decimalPlace == -1 ? ProjectConfiguration.DecimalPlace : decimalPlace, MidpointRounding.AwayFromZero);
                            return x;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert object value to boolean
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <param name="decimalPoints">decimalPoints</param>
        /// <returns>return true else false</returns>
        public static string ToNullableDecimalString(this decimal? readField, int decimalPoints = 2)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return ToDecimalString(readField.ToDecimal(), decimalPoints);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert object value to boolean
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <param name="decimalPoints">decimalPoints</param>
        /// <returns>return true else false</returns>
        public static string ToDecimalString(this decimal readField, int decimalPoints = 2)
        {
            return Convert.ToString(Math.Round(Convert.ToDecimal(readField), decimalPoints, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Convert decimal value to string
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return true else false</returns>
        public static string ToNullableDecimalStr(this decimal? readField)
        {
            if (readField != null && readField.GetType() != typeof(System.DBNull))
            {
                return string.Format("{0:0.00}", Convert.ToDecimal(readField));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert object value to boolean
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return true else false</returns>
        public static bool ToBoolean(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    string stringReadField = Convert.ToString(readField, CultureInfo.InvariantCulture);

                    if (stringReadField == "1")
                    {
                        return true;
                    }

                    bool x;
                    if (bool.TryParse(stringReadField, out x))
                    {
                        return x;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Convert object value to boolean
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return true else false</returns>
        public static bool ToBooleanCustom(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    string stringReadField = Convert.ToString(readField, CultureInfo.InvariantCulture);

                    if (stringReadField.ToLower() == "on")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// check given value is boolean or null
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return true else false</returns>
        public static bool? ToBoolNull(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    string stringReadField = Convert.ToString(readField, CultureInfo.InvariantCulture);

                    if (stringReadField == "1")
                    {
                        return true;
                    }

                    bool x;
                    if (bool.TryParse(stringReadField, out x))
                    {
                        return x;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert object value to integer
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return integer else 0</returns>
        public static int ToInteger(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        int toReturn;
                        if (int.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert object value to long
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return long else 0</returns>
        public static long ToLong(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        long toReturn;
                        if (long.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert object value to short
        /// </summary>
        /// <param name="readField">object to convert</param>
        /// <returns>return short else 0</returns>
        public static short ToShort(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        short toReturn = 0;
                        if (short.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert object value to nullable date
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static DateTime? ToNullableDate(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Convert object value to date
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static DateTime ToDate(this object readField)
        {
            return DateTime.SpecifyKind(Convert.ToDateTime(readField, CultureInfo.CurrentCulture), DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Convert object value to nullable local date
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static DateTime? ToNullableLocalDate(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToLocalTime();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Convert object value to nullable local date
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static DateTime? FromUploadToNullableLocalDate(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField), out dateReturn))
                    {
                        return Convert.ToDateTime(dateReturn.ToString(ProjectConfiguration.DateFormat));
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Convert object value to local date
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static DateTime ToLocalDate(this object readField)
        {
            return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToLocalTime();
        }

        /// <summary>
        /// Convert object value to date formatted string
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static string ToDateFormat(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.InvariantCulture).GetDateTimeFormats('d', CultureInfo.InvariantCulture)[5];
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Convert object value to date formatted string
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <param name="dateFormat">Date format</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static string ToDate(this object readField, string dateFormat)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (!string.IsNullOrEmpty(dateFormat))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToString(dateFormat, CultureInfo.InvariantCulture);
                    }

                    return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
                }
            }

            return DateTime.MinValue.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <param name="nullvalue">value for null</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime? value, string dateFormat, string nullvalue)
        {
            if (value != null && value.HasValue)
            {
                return value.Value.ToCustomDateFormat(dateFormat);
            }
            else
            {
                return nullvalue;
            }
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime? value, string dateFormat)
        {
            return value.ToCustomDateFormat(dateFormat, string.Empty);
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="timeFormat">time format</param>
        /// <param name="nullvalue">value for null</param>
        /// <returns>return String</returns>
        public static string ToCustomTimeFormat(this TimeSpan? value, string timeFormat, string nullvalue)
        {
            if (value != null && value.HasValue)
            {
                return value.Value.ToString(timeFormat);
            }
            else
            {
                return nullvalue;
            }
        }

        /// <summary>
        /// Return Customize Time Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="timeFormat">time format</param>
        /// <returns>return String</returns>
        public static string ToCustomTimeFormat(this TimeSpan? value, string timeFormat)
        {
            return value.ToCustomTimeFormat(timeFormat, string.Empty);
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime value, string dateFormat)
        {
            try
            {
                return value.ToDate(dateFormat);
            }
            catch
            {
                return value.ToDate(ProjectConfiguration.DateTimeFormat);
            }
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToSystemDate(this DateTime? value)
        {
            string q = string.Empty;
            if (value.HasValue)
            {
                q = value.Value.ToSystemDate();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date time Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDateTime(this DateTime? value)
        {
            string q = string.Empty;
            if (value.HasValue)
            {
                q = value.Value.ToSystemDateTime();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDate(this DateTime value)
        {
            return value.ToDate(ProjectConfiguration.DateFormat);
        }

        /// <summary>
        /// Return Standard Date time Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDateTime(this DateTime value)
        {
            return value.ToDate(ProjectConfiguration.DateTimeFormat);
        }

        /// <summary>
        /// Convert object value to date formatted string
        /// </summary>
        /// <param name="readField">date value to check</param>
        /// <returns>return date if valid format else return nothing</returns>
        public static string ToTime(this DateTime readField)
        {
            return readField.ToString(ProjectConfiguration.DateFormat);
        }

        /// <summary>
        /// for save null value in database
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <returns>return DBNull value</returns>
        public static object ToDBNullValue(this string value)
        {
            if (value == null | string.IsNullOrEmpty(value))
            {
                return System.DBNull.Value;
            }

            return value;
        }

        /// <summary>
        /// To check null value
        /// </summary>
        /// <param name="value">object to check</param>
        /// <returns>if null than returns DBNull.Value else returns object which is passed</returns>
        public static object ToDBNull(this object value)
        {
            if (value != null)
            {
                return value;
            }

            return DBNull.Value;
        }

        /// <summary>
        /// Check is null or not
        /// </summary>
        /// <param name="checkString">checkString value</param>
        /// <returns>return boolean</returns>
        public static bool IsVoid(string checkString)
        {
            if (checkString == null)
            {
                return true;
            }
            else
            {
                return (checkString == string.Empty) || (checkString.Length == 0) || (checkString.Trim() == string.Empty);
            }
        }

        /// <summary>
        /// Get property name as a string
        /// </summary>
        /// <typeparam name="T">T value</typeparam>
        /// <typeparam name="TProp">prop value</typeparam>
        /// <param name="expression">expression value</param>
        /// <returns>return string</returns>
        public static string GetPropertyNameInString<T, TProp>(this Expression<Func<T, TProp>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                return null;
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Convert string to date format
        /// </summary>
        /// <param name="valueDateString">valueDateString value</param>
        /// <param name="dateFromat">date Format value</param>
        /// <param name="dateSeparator">dateSeparator value</param>
        /// <returns>return Date</returns>
        ////public static DateTime ToDateFormatFromString(this object valueDateString, EnumHelper.DATEFORMAT dateFromat, string dateSeparator)
        ////{
        ////    DateTime valueDate = DateTime.MinValue;

        ////    string dateString = valueDateString.String();

        ////    if (!string.IsNullOrEmpty(dateString))
        ////    {
        ////        string dt = string.Empty;
        ////        string mn = string.Empty;
        ////        string yr = string.Empty;

        ////        string[] arrDate = dateString.Split(new string[] { dateSeparator }, StringSplitOptions.None);

        ////        if (dateFromat == EnumHelper.DATEFORMAT.DDMMYYYY)
        ////        {
        ////            dt = arrDate[0];
        ////            mn = arrDate[1];
        ////            yr = arrDate[2];
        ////        }
        ////        else
        ////        {
        ////            mn = arrDate[0];
        ////            dt = arrDate[1];
        ////            yr = arrDate[2];
        ////        }

        ////        valueDate = new DateTime(yr.ToInteger(), mn.ToInteger(), dt.ToInteger());
        ////    }

        ////    return valueDate;
        ////}

        /// <summary>
        /// ImageToBase64
        /// </summary>
        /// <param name="imageURL">imageURL</param>
        /// <returns>returns</returns>
        public static string ImageToBase64(string imageURL)
        {
            if (!string.IsNullOrEmpty(imageURL))
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(imageURL);
                    return "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// ThousandSeperator
        /// </summary>
        /// <param name="dValue">dValue</param>
        /// <returns>returns</returns>
        public static string ThousandSeperator(this decimal? dValue)
        {      
            return dValue.ToDecimal().ToString("N");
        }

        /// <summary>
        /// ThousandSeperator
        /// </summary>
        /// <param name="dValue">dValue</param>
        /// <returns>returns</returns>
        public static string ThousandSeperator(this decimal dValue)
        {
            return dValue.ToDecimal().ToString("N");
        }

        /// <summary>
        /// Set Object Properties Null To Empty
        /// </summary>
        /// <typeparam name="T">T Class</typeparam>
        /// <param name="obj">obj</param>
        public static void SetObjectPropertiesNullToEmpty<T>(T obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    if (propertyInfo.GetValue(obj, null) == null)
                    {
                        propertyInfo.SetValue(obj, string.Empty, null);
                    }
                }
            }
        }

        /// <summary>
        /// ParseXML
        /// </summary>
        /// <typeparam name="T">t</typeparam>
        /// <param name="xml">xml</param>
        /// <returns>class</returns>
        public static T ParseXML<T>(string xml)
            where T : class
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.MaxCharactersFromEntities = 0;
            var reader = XmlReader.Create(new StringReader(xml), settings);
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        /// <summary>
        /// ParseXML
        /// </summary>
        /// <typeparam name="T">t</typeparam>
        /// <param name="items">items</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion
    }
}
