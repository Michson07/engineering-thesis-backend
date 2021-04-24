using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;

namespace Groups.Database
{
    public class StringListToStringValueConverter : ValueConverter<IEnumerable<string>, string>
    {
        public StringListToStringValueConverter() : base(le => ListToString(le), (s => StringToList(s)))
        {

        }

        public static string? ListToString(IEnumerable<string> value)
        {
            if (value == null || value.Count() == 0)
            {
                return null;
            }

            return string.Join(',', value);
        }

        public static IEnumerable<string> StringToList(string value)
        {
            if (value == null || value == string.Empty)
            {
                return null;
            }

            return value.Split(',');

        }
    }
}
