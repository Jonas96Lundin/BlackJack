using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace UtilitiesLib
{
    [Serializable]
    public class ListManager<T> : IListManager<T>
    {
        // Variables
        protected List<T> list;

        public ListManager()
        {
            list = new List<T>();
        }

        #region All method definitions
        /// <summary>
        /// Return the number of items in the collection m_list
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        /// <summary>
        /// Return the list of items
        /// </summary>
        public List<T> List
        {
            get { return list; }
            set { list = value; }
        }

        /// <summary>
        /// Add an object to the collecdtion m_list.
        /// </summary>
        /// <param name="aType">A type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Add(T aType)
        {
            if (aType != null)
            {
                list.Add(aType);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="anIndex">Index to object that is to be removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DeleteAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                list.RemoveAt(anIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Replace an object from the collection at a given index by a new object.
        /// </summary>
        /// <param name="aType">Object to be replaced.</param>
        /// <param name="anIndex">index to element to be replaced by a new object.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                list[anIndex] = aType;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return an object at a given position from the collection m_list.
        /// </summary>
        /// <param name="anIndex">.</param>
        /// <returns></returns>
        public T GetAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                return list[anIndex];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Control that a given index is >= 0 and less than the number of items in 
        /// the collection.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < list.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Deletes all object of the collection and set the collection to null.
        /// </summary>
        public void DeleteAll()
        {
            list.Clear();
            list = null;
        }

        /// <summary>
        /// Prepare a list of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        public List<string> ToStringList()
        {
            List<string> stringList = new List<string>();
            if (list != null)
            {
                foreach (T aType in list)
                {
                    stringList.Add(aType.ToString());
                }
            }
            return stringList;
        }
        /// <summary>
        /// Same as as ToStringList but returning a array of strings.
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            if (list != null)
            {
                string[] stringArray = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    stringArray[i] = list[i].ToString();
                }
                return stringArray;
            }
            else
            {
                return default;
            }
        }
        #endregion

        ///// <summary>
        ///// Serialize the list to a binary file
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public bool BinarySerialize(string fileName)
        //{
        //    bool result;
        //    try
        //    {
        //        using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
        //        {
        //            BinaryFormatter bin = new BinaryFormatter();
        //            bin.Serialize(stream, list);
        //        }
        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Deserialize a binary file to a list
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public bool BinaryDeSerialize(string fileName)
        //{
        //    bool result;
        //    try
        //    {
        //        using(Stream stream = File.Open(fileName, FileMode.Open))
        //        {
        //            BinaryFormatter bin = new BinaryFormatter();
        //            list.Clear();
        //            list = (List<T>)bin.Deserialize(stream);
        //        }
        //        result = true;
        //    }
        //    catch(Exception e)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Serialize the list to an XML file
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public bool XMLSerialize(string fileName)
        //{
        //    bool result;
        //    List<Type> types = new List<Type>();
        //    foreach (Object obj in list)
        //    {
        //        Type type = obj.GetType();
        //        if (!types.Contains(type))
        //        {
        //            types.Add(type);
        //        }
        //    }

        //    try
        //    {
        //        using(Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        //        {
        //            XmlSerializer xmlFormatter = new XmlSerializer(list.GetType(), types.ToArray());
        //            xmlFormatter.Serialize(stream, list);
        //        }
        //        result = true;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e);
        //        result = false;
        //    }
        //    return result;
        //}
    }
}
