namespace Economy.Data
{
    public interface IConverter
    {
        void ConvertAndSave(string filePath, string outFilePath);
    }
}