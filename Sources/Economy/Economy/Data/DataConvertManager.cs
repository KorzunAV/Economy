using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Economy.Common.Belinvest;
using Economy.Common.FileSystem;
using Economy.Models;

namespace Economy.Data
{
    public class ConvertManager
    {
        private readonly Parser _parser;

        public ConvertManager()
        {
            _parser = new Parser();
        }

        public Task Convert(string dirPath, string dirPathOut, Action<string, int, int> callback = null)
        {
            return Task.Run((() => TryConvertTest(dirPath, dirPathOut, _parser.Parse, callback)));
        }

        public Task TryConvert(string dirPath, string dirPathOut, Action<string, int, int> callback = null)
        {
            return Task.Run(() => TryConvertTest(dirPath, dirPathOut, _parser.TryParse, callback));
        }

        private void TryConvertTest(string dirPath, string dirPathOut, Func<string, MontlyReport> parseDelegate, Action<string, int, int> callback)
        {
            if (!Directory.Exists(dirPath)) throw new DirectoryNotFoundException(dirPath);
            if (!Directory.Exists(dirPathOut)) throw new DirectoryNotFoundException(dirPathOut);


            var paths = Directory.GetFiles(dirPath);
            var convertedPaths = Directory.GetFiles(dirPathOut);
            var index = 0;

            foreach (var filePath in paths)
            {
                index++;
                var fpwe = Path.GetFileNameWithoutExtension(filePath) ?? string.Empty;
                if (!convertedPaths.Any(f => fpwe.Equals(Path.GetFileNameWithoutExtension(f), StringComparison.CurrentCultureIgnoreCase)))
                {
                    var data = parseDelegate(filePath);
                    if (data != null)
                    {
                        var outpath = Path.Combine(dirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        Serialization.Save(data, outpath, FileMode.Create);
                    }
                }
                if (callback != null)
                    callback(fpwe, paths.Length, index);
            }
        }
    }
}
