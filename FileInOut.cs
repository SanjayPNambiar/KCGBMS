using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace SG
{
    public class FileInOut
    {

        public string CalculateMD5Hash(string literal)
        {
            // step 1, calculate MD5 hash from literal given as input   
            MD5 md5 = MD5.Create();
            byte[] byteBuffer = System.Text.Encoding.ASCII.GetBytes(literal);
            byte[] hash = md5.ComputeHash(byteBuffer);
            // step 2, convert byte array to hexadecimal string   
            string hashHex = ByteArrayToHexString(hash);
            return hashHex;
        }

        public string ByteArrayToHexString(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public void write(List<BMSUser> lu)
        {
            try
            {
                // Get serializer.
                IFormatter serializer = new BinaryFormatter();
                // Serialize
                FileStream saveFile = new FileStream("BU.bin", FileMode.Create, FileAccess.Write);
                serializer.Serialize(saveFile, lu);
                saveFile.Close();
            }
            catch (SerializationException ee)
            {
                MessageBox.Show(ee.Message, "A serialization exception has been thrown!");
            }
            catch (IOException ee)
            {
                MessageBox.Show(ee.ToString(), "An IO exception has been thrown!");
            }

        }

        public List<BMSUser> read()
        {
            List<BMSUser> lu = new List<BMSUser>();
            try
            {
                // Get serializer.
                IFormatter serializer = new BinaryFormatter();

                if (File.Exists("BU.bin"))
                {
                    // Deserialize
                    FileStream loadFile = new FileStream("BU.bin", FileMode.Open, FileAccess.Read);
                    lu = serializer.Deserialize(loadFile) as List<BMSUser>;
                    loadFile.Close();
                }


                
            }
            catch (SerializationException ee)
            {
                MessageBox.Show(ee.Message, "A serialization exception has been thrown!");
            }
            catch (IOException ee)
            {
                MessageBox.Show(ee.ToString(), "An IO exception has been thrown!");
            }

            return lu;



        }
    }
}
