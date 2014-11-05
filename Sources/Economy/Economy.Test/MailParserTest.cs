using System.IO;
using System.Text;
using Economy.Common;
using NUnit.Framework;

namespace Economy.Test
{
    [TestFixture]
    public class MailParserTest
    {
        [Test]
        public void CleanTest()
        {
            const string filePath = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Mails\54472031.htm";
            string file;
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                file = sr.ReadToEnd();
            }
            var result = MailParser.Clean(file);
            
            using (var sw = new StreamWriter("result.xml"))
            {
                sw.Write(result);
            }
        }
    }
}
