using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UtilitiesLib
{
    public static class Serializer<T>
    {
        /// <summary>
        /// Serialize the object to a binary file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool BinarySerialize(string fileName, object obj)
        {
            bool result;
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, obj);
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Deserialize a binary file to an object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool BinaryDeSerialize(string fileName, ref T obj)
        {
            bool result;
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    //obj.Clear();
                    obj = /*(List<T>)*/(T)bin.Deserialize(stream);
                }
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Serialize the object to an XML file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool XMLSerialize(string fileName, object obj)
        {
            bool result;
            List<Type> types = new List<Type>();
            if (obj is System.Collections.ICollection)
            {
                foreach (Object o in (System.Collections.ICollection)obj)
                {
                    Type type = o.GetType();
                    if (!types.Contains(type))
                    {
                        types.Add(type);
                    }
                }
            }


            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlFormatter = new XmlSerializer(obj.GetType(), types.ToArray());
                    xmlFormatter.Serialize(stream, obj);
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }
    }
}
