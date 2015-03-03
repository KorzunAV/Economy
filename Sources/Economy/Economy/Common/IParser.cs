

namespace Economy.Common
{
    public interface IParser
    {
        object TryParse(string filePath);

        object Parse(string filePath);
    }
}