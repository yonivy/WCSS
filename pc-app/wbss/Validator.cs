using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WBSS
{
    static class Validator
    {
        public static bool isEmailAddress(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            return Regex.IsMatch(str,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$",
                    RegexOptions.IgnoreCase);
        }

        public static bool isURL(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            Uri uri;

            return Uri.TryCreate(str, UriKind.Absolute, out uri);
        }

        public static bool isPort(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            if (Regex.IsMatch(
                str, 
                @"^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$", 
                RegexOptions.IgnoreCase))
            {
                int value = Convert.ToInt32(str);

                return 1023 < value && value < 65536;
            }

            return false;
        }

        public static bool isPassword(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            return true;
        }
    }
}
