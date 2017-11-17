using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace afri_data
{
    public class sauvgarde_loud
    {



        public static bool Write(string doc_path, string chemain)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(doc_path.GetType());
                StreamWriter writer = new StreamWriter(chemain);
                x.Serialize(writer, doc_path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read Settings
        /// </summary>
        /// <returns></returns>
        public static string Read(string doc_path)
        {
            string deri_path;
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(string));
                StreamReader reader = new StreamReader(doc_path);
                deri_path = (string)x.Deserialize(reader);
                return deri_path;
            }
            catch
            {
                return null ;

            }
        }
    }
}
