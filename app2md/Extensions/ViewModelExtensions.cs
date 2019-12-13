using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace app2md.Extensions
{
    public static class ViewModelExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}
