using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Html
{
    class StringTokenizer
    {
        static StringSplitOptions options;
        static string[] delimeters;
        static string str;

        #region Split
        public static string[] Split(string str, string[] delimeters, StringSplitOptions options = StringSplitOptions.None)
        {
            StringTokenizer.options = options;
            StringTokenizer.delimeters = delimeters;
            StringTokenizer.str = str;
            List<string> list = new List<string>();
            string cur = "";
            for (int i = 0; i <= str.Length; i++)
            {
                int d = getDelimeter(i);
                if (d == -1)
                {
                    if (i < str.Length)
                        cur += str[i];
                    else
                    {
                        AddToList(list, cur);
                        cur = "";
                    }
                }
                else
                {
                    AddToList(list, cur);
                    cur = "";
                    AddToList(list, delimeters[d]);
                    i += delimeters[d].Length - 1;
                }
            }
            return list.ToArray();
        }
        private static int getDelimeter(int index)
        {
            int ret = -1;
            for (int i = 0; i < delimeters.Length; i++)
            {
                if (index + delimeters[i].Length <= str.Length &&
                    str.Substring(index, delimeters[i].Length) == delimeters[i])
                {
                    if (ret == -1 || delimeters[ret].Length < delimeters[i].Length)
                        ret = i;
                }
            }
            return ret;
        }
        private static void AddToList(List<string> list, string str)
        {
            if (options == StringSplitOptions.RemoveEmptyEntries)
                str = str.Trim();
            if (str.Length > 0)
                list.Add(str);
        }
        #endregion
    }
}
