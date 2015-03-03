using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Economy.Common;
using Economy.Common.FileSystem;

namespace Economy.Data
{
    public class ConvertManager
    {
        public Task Convert(string dirPath, string dirPathOut, Action<string, int, int> callback = null)
        {
            return Task.Run((() => TryConvertTest(dirPath, dirPathOut, callback)));
        }

        private IParser GetParser(string dirPath)
        {
            if (dirPath.Contains("History"))
            {
                return new Economy.Common.History.Parser();
            }
            return new Economy.Common.Belinvest.Parser();
        }

        private void TryConvertTest(string dirPath, string dirPathOut, Action<string, int, int> callback)
        {
            if (!Directory.Exists(dirPath)) throw new DirectoryNotFoundException(dirPath);
            if (!Directory.Exists(dirPathOut)) throw new DirectoryNotFoundException(dirPathOut);

            var parser = GetParser(dirPath);

            var paths = Directory.GetFiles(dirPath);
            var convertedPaths = Directory.GetFiles(dirPathOut);
            var index = 0;

            foreach (var filePath in paths)
            {
                index++;
                var fpwe = Path.GetFileNameWithoutExtension(filePath) ?? string.Empty;
                if (!convertedPaths.Any(f => fpwe.Equals(Path.GetFileNameWithoutExtension(f), StringComparison.CurrentCultureIgnoreCase)))
                {
                    var data = parser.Parse(filePath);
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
