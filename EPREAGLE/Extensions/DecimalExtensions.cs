using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPREAGLE.Extensions
{
    internal static class DecimalExtensions
    {
        public static decimal ToNumber(this object input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "القيمة المدخلة null");

            // لو أصلاً القيمة رقم (int, double, decimal...)
            if (input is IConvertible)
            {
                try
                {
                    return Convert.ToDecimal(input);
                }
                catch
                {
                    throw new ArgumentException($"القيمة '{input}' ليست رقم صحيح.");
                }
            }

            // آخر محاولة: جرب نحولها من string
            if (decimal.TryParse(input.ToString(), out decimal result))
                return result;

            throw new ArgumentException($"القيمة '{input}' ليست رقم صحيح.");
        }
        //جمع
        public static decimal AddValue(this object value1,object value2) 
            => value1.ToNumber() + value2.ToNumber();
        //طرح
        public static decimal SubtractValue(this object a, object b)
            => a.ToNumber() - b.ToNumber();

        // ضرب
        public static decimal MultiplyValue(this object a, object b)
            => a.ToNumber() * b.ToNumber();

        // قسمة
        public static decimal DivideValue(this object a, object b)
        {
            var divisor = b.ToNumber();
            if (divisor == 0)
                throw new DivideByZeroException("لا يمكن القسمة على صفر");
            return a.ToNumber() / divisor;
        }
        // خصم الخصم 
        public static decimal ApplyDiscount(this decimal price, decimal discountPercent)
            => price - (price * discountPercent / 100);
        //اضافة قيمة الضريبة
        public static decimal ApplyTax(this decimal price, decimal taxPercent)
            => price + (price * taxPercent / 100);

    }
}
