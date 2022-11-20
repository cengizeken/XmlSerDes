using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace XmlSerDes
{
    public partial class AnaForm : Form
    {
        MySerializableClass mySerializableClass;
        public AnaForm()
        {
            InitializeComponent();
            
        }

        private void btnSerializeDeserialize_Click(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MySerializableClass));
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + @"\serializedXml.xml");
            xmlSerializer.Serialize(streamWriter, new MySerializableClass() { CommanId = 3, MyInteger = 5, MyList = new List<ushort>() { 0xF002, 0xBBFE} });
            streamWriter.Close();

            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + @"\serializedXml.xml");
            MySerializableClass ms = (MySerializableClass)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            //MySerializableClass deneme = new MySerializableClass() { CommanId = 3, MyInteger = 5, MyList = new List<ushort>() { 0xF002, 0xBBFE } };
            //XmlSerializerDeserializerWrapper.Write(() => deneme);
            //new MySerializableClass() { CommanId = 3, MyInteger = 5, MyList = new List<ushort>() { 0xF002, 0xBBFE } }
            XmlSerializerDeserializerWrapper.Serialize(Directory.GetCurrentDirectory() + @"\serializedXmlWrapper.xml", new MySerializableClass() { CommanId = 3, MyInteger = 5, MyList = new List<ushort>() { 0xF002, 0xBBFE } });
            //XmlSerializerDeserializerWrapper.Serialize(Directory.GetCurrentDirectory() + @"\serializedXmlWrapper.xml", deneme);
            MySerializableClass myserializableClass =  XmlSerializerDeserializerWrapper.Deserialize<MySerializableClass>(Directory.GetCurrentDirectory() + @"\serializedXmlWrapper.xml");
        }
    }
}
