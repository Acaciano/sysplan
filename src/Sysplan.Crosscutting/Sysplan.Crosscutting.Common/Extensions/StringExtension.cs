using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class StringExtension
    {
        public static string RemoveAccents(this string text)
        {

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
