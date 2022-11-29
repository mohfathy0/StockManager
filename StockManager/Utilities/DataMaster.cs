using StockManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Utilities
{
    public static class DataMaster
    {
        public static string GetNextNumberInString(this string Number)
        {
            if (string.IsNullOrEmpty(Number))
                return "1";
            string strDigits1 = string.Empty;

            foreach (var c in Number)
                strDigits1 = char.IsDigit(c) ? strDigits1 + c.ToString() : "";
            if (strDigits1 == string.Empty)
                return $"{Number}1";
            // PRD01 => 01 => 101
            string strDigits2 = strDigits1.Insert(0, "1");
            strDigits2 = (Convert.ToInt32(strDigits2) + 1).ToString();
            string strDigits3 = strDigits2[0] == '1' ? strDigits2.Remove(0, 1) : strDigits2.Remove(0, 1).Insert(0, "1");
            int index = Number.LastIndexOf(strDigits1);
            Number = Number.Remove(index);
            Number = Number.Insert(index, strDigits3);
            return Number;
        }
        public static byte[] GetPropertyValue(string propertyName, int profileId)
        {
            using (var db = new DatabaseContext())
            {
                var prop = db.UserSettingsProfileProperties.SingleOrDefault(x => x.ProfileId == profileId && x.PropertyName == propertyName);
                if (prop == null)
                    return null;
                return prop.PropertyValue.ToArray();
            }
        }
        public static byte[] GetGlobalSettingValue(string propertyName)
        {
            using (var db = new DatabaseContext())
            {
                var prop = Session.globalSettings.SingleOrDefault(x => x.SettingName == propertyName);
                if (prop == null || prop.SettingValue == null)
                    return null;

                return prop.SettingValue.ToArray();
            }
        }

        public static int GetGlobalSettingId(string propertyName)
        {
            using (var db = new DatabaseContext())
            {
                var prop = Session.globalSettings.SingleOrDefault(x => x.SettingName == propertyName);
                if (prop == null)
                    return 0;
                return prop.Id;
            }
        }
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }
        public static string ToSentence(this string Input)
        {
            return new string(Input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());
        }
        public static BindingList<T> ToBindingList<T>(this IList<T> source)
        {
            return new BindingList<T>(source);
        }
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> source)
        {
            return new BindingList<T>(source.ToList());
        }
        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(data))
            {
                object obj = (T)formatter.Deserialize(stream);
                return (T)obj;
            }
        }
        public static byte[] ToByteArray<T>(this T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                if (stream.Length == 0)
                    return new byte[1];
                return stream.ToArray();
            }
        }
        public static string GetCallerName([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
        public static decimal ToDecimal(this object s)
        {
            if (s is string)
                return 0;
            return Convert.ToDecimal(s);
        }
        public static double ToDouble(this object s)
        {
            if (s is string)
                return 0;
            return Math.Round(Convert.ToDouble(s), 2);
        }

        public static int ToInt(this object s)
        {
            if (s is string)
                return 0;
            return Convert.ToInt32(s);
        }
        public static byte ToByte(this object s)
        {
            if (s is string)
                return ((byte)s);
            if (s.ToInt() < 0)
                return 0;
            return Convert.ToByte(s);
        }
        // Linq Query extensions  
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return (IQueryable<TSource>)source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return (IQueryable<TSource>)source.Where(predicate);
            else
                return source;
        }
    }
}
