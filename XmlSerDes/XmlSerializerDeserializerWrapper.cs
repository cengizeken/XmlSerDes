using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Linq.Expressions;

namespace XmlSerDes
{
    public class XmlSerializerDeserializerWrapper
    {
        //private static XmlSerializer _bin = new XmlSerializer();
        /// <summary>
        /// Serialize any class marked as serializable
        /// </summary>
        /// <typeparam name="T">Serializable class</typeparam>
        /// <param name="pathOrFileName">path or file name to which T is serialized/param>
        /// <param name="objectToSerialise">Object to be serialized</param>
        public static void Serialize<T>(string pathOrFileName, T objectToSerialise) where T : class
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(typeof(T).FullName + " is not serializable");
            }

            if(EqualityComparer<T>.Default.Equals(objectToSerialise, default(T)))
            {
                throw new ArgumentNullException(typeof(T).FullName + " should not be null");
            }
            //using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream stream = File.Open(pathOrFileName, FileMode.Create))
                {
                    try
                    {
                        xmlSerializer.Serialize(stream, objectToSerialise);
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                        throw;
                    }
                }
            }

        }

        /// <summary>
        /// Deserialize any class
        /// </summary>
        /// <typeparam name="T">any class marked as Serializable</typeparam>
        /// <param name="pathOrFileName">path or file name from which T is deserialized</param>
        /// <returns>Deserialized class object</returns>
        public static T Deserialize<T>(string pathOrFileName) where T: class
        {
            T deserializedObject;
            //using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream stream = File.Open(pathOrFileName, FileMode.Open))
                {
                    try
                    {
                        deserializedObject = (T)xmlSerializer.Deserialize(stream);
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                        throw;
                    }
                }
            }
            return deserializedObject;
        }
        public static void Write<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) throw new NotSupportedException();
            Console.WriteLine(memberExpression.Member.Name);
        }
    }
}