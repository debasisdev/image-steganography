using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;


namespace Steganography
{
    class LogFile
    {
        public static void WriteLogInformation(string filename, string info1, string info2)
        {
            StringBuilder sbuilder = new StringBuilder();
            using (StringWriter sw = new StringWriter(sbuilder))
            {
                using (XmlTextWriter w = new XmlTextWriter(sw))
                {
                    w.WriteStartElement("LogInfo");
                    w.WriteElementString("Time", DateTime.Now.ToString());
                    w.WriteElementString("InfoOne:", info1);
                    w.WriteElementString("InfoTwo:", info2);
                    w.WriteEndElement();
                }
            }
            using (StreamWriter w = new StreamWriter(filename, true, Encoding.UTF8))
            {
                w.WriteLine(sbuilder.ToString());
            }
        }
    }
}
