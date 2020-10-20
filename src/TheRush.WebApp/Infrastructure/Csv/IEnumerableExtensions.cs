using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;

namespace TheRush.WebApp.Infrastructure.Csv
{
    public static class EnumerableExtensions
    {
        public static async Task<MemoryStream> ToCsvStream<T>(this IEnumerable<T> enumerable, Func<PropertyInfo, bool> filter)
        {
            await using var ms = new MemoryStream();
            await using var writer = new StreamWriter(ms);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            foreach (var row in enumerable)
            {
                foreach (var field in row.GetType().GetProperties().Where(filter))
                {
                    csv.WriteField(field.GetValue(row));
                }

                await csv.NextRecordAsync();
            }

            return ms;
        }
    }
}