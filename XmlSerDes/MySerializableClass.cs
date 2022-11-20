using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XmlSerDes
{
    [Serializable]//Added to force the class to be serializable. If ommited, remove .IsSerializable control from XmlSerializerDeserializerWrapper
    public class MySerializableClass
    {
        public int MyInteger { get; set; }
        public int CommanId { get; set; }
        //[XmlArray("HuzmeParametreleri")]//you can ommit ("HuzmeParametreleri"). In this case MyList will appear in serialized xml file
        [XmlArray]
        //[XmlArrayItem("ListOfHuzmeler")]//you can change the names of the list items in the serialized xml file as ListOfHuzmeler instead unsignedShort
        public List<ushort> MyList {get; set; }
    }
}
