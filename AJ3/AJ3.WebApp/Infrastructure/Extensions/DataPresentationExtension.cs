using System.Globalization;

namespace AJ3.WebApp.Infrastructure.Extensions
{
    public static class DataPresentationExtension
    {
        /// <summary>
        /// Use to display display amount or money in Ph format currency
        /// </summary>
        /// <param name="value">Amount/Money</param>
        /// <returns>{format}</returns>
        public static string ToPhFormatCurrency(this decimal value)
        {
            var nfi = new CultureInfo("en-Ph", false).NumberFormat;
            nfi.CurrencyPositivePattern = 0;
            return string.Format(nfi, "{0:C}", value);
        }
    }
}
