//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Economy.Models;

//namespace Economy.Common.History
//{
//    public class Parser
//    {
//        public List<PriceHistory> TryParse(string filePath)
//        {
//            try
//            {
//                return Parse(filePath);
//            }
//            catch (Exception)
//            {
//            }
//            return null;
//        }

//        public List<PriceHistory> Parse(string filePath)
//        {
//            string file;
//            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
//            {
//                file = sr.ReadToEnd();
//            }
//        }
//    }
//}
