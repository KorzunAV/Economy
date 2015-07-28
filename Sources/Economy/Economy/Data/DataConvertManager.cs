using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Economy.Data.Parsers;

namespace Economy.Data
{
    public class ConvertManager
    {
        public Task Convert(string dirPath, string dirPathOut, Action<string, int, int> callback = null)
        {
            return Task.Run((() => TryConvert(dirPath, dirPathOut, callback)));
        }

        private bool IsHistory(string dirPath)
        {
            return dirPath.Contains("History");
        }


        private IConverter GetParser(string dirPath)
        {
            if (IsHistory(dirPath))
            {
                return new CurrencyHistoryConverter();
            }
            return new BelinvestConverter();
        }

        private void TryConvert(string dirPath, string dirPathOut, Action<string, int, int> callback)
        {
            if (!Directory.Exists(dirPath)) 
                throw new DirectoryNotFoundException(dirPath);
            if (!Directory.Exists(dirPathOut))
                throw new DirectoryNotFoundException(dirPathOut);

            var parser = GetParser(dirPath);
            var isHistory = IsHistory(dirPath);

            var paths = Directory.GetFiles(dirPath);
            var convertedPaths = Directory.GetFiles(dirPathOut);
            var index = 0;

            foreach (var filePath in paths)
            {
                index++;
                var fpwe = Path.GetFileNameWithoutExtension(filePath) ?? string.Empty;
                if (isHistory || !convertedPaths.Any(f => fpwe.Equals(Path.GetFileNameWithoutExtension(f), StringComparison.CurrentCultureIgnoreCase)))
                {
                    var outpath = Path.Combine(dirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                    parser.ConvertAndSave(filePath, outpath);
                }
                if (callback != null)
                    callback(fpwe, paths.Length, index);
            }
        }
    }
}
