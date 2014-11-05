using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Economy.Common.FileSystem
{
    public class Serialization
    {
        public static void Save<T>(T obj, string fileName, FileMode fileMode)
            where T : class
        {
            using (Stream fs = new FileStream(fileName, fileMode))
            {
                var fmt = new BinaryFormatter();
                fmt.Serialize(fs, obj);
            }
        }
    }
}
