namespace Economy.Common
{
    public interface IParser<T>
    {
        T TryParse(string filePath);

        T Parse(string filePath);
    }
}