namespace Economy.Parsers
{
    public interface IConverter
    {
        void ConvertAndSave(string filePath, string outFilePath);
    }
}