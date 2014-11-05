using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Economy.Common.FileSystem
{
    public class Deserialization
    {
        public static T Load<T>(string fileName)
             where T : class
        {
            T result;
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                result = (T)bf.Deserialize(fs);
            }
            return result;
        }
    }
}
