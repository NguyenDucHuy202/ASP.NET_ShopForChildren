using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QL_BanDoTreEm.Models
{
    public class Helper
    {
        public static string getNewKey(string key)
        {
            Match match = Regex.Match(key, @"\d+");

            if (match.Success)
            {
                int number = int.Parse(match.Value) + 1;

                string newKey = Regex.Replace(key, @"\d+", number.ToString("D6"));

                return newKey;
            }
            return null;
        }
        public static string getNewID_SANPHAM(string key)
        {
            Match match = Regex.Match(key, @"\d+");

            if (match.Success)
            {
                int number = int.Parse(match.Value) + 1;

                string newKey = Regex.Replace(key, @"\d+", number.ToString("D8"));

                return newKey;
            }
            return null;
        }
    }
}