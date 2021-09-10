using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;

namespace Asaph.Shared.ExtensionMethods {
    public static class AsaphExtensionMethods {
        public static DateTime RoundToSeconds(this DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

        public static DataTable ToDataTable<T>(this IEnumerable<T> data) => data.ToList().ToDataTable();

        private static DataTable ToDataTable<T>(this IList<T> data) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new();
            foreach (PropertyDescriptor prop in properties) {
                Type propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, propertyType);
            }

            foreach (T item in data) {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties) {
                    object columnValue = prop.GetValue(item) ?? DBNull.Value;
                    row[prop.Name] = columnValue;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
