using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SF.Tools
{
    /// <summary>
    /// 操作ini文件
    /// </summary>
    // [System.Diagnostics.DebuggerStepThrough]
    public class INIFile
    {
        public string path = System.Windows.Forms.Application.StartupPath + @"\datatype.ini";
        public INIFile(string INIPath)
        {
            this.path = INIPath;
        }
        public INIFile( )
        {
            if (!File.Exists(path))
            {
                Utils.CreateFiles(Application.StartupPath + @"\datatype.ini", Utils.ini);
            }
        }
        private string[] ByteToString(byte[] sectionByte)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(sectionByte).Split(new char[1]);
        }
        public void ClearAllSection( )
        {
            this.IniWriteValue(null, null, null);
        }
        public void ClearSection(string Section)
        {
            this.IniWriteValue(Section, null, null);
        }
        /// <summary>
        /// 读取INI
        /// <code>
        ///   proStr = datatype.IniReadValue("DbToCS", row["类型"].ToString());
        /// </code>
        /// </summary>
        /// <param name="Section">DbToCS</param>
        /// <param name="Key">int</param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.path);
            return retVal.ToString();
        }
        public string[] IniReadValues( )
        {
            byte[] sectionByte = this.IniReadValues(null, null);
            return this.ByteToString(sectionByte);
        }
        public string[] IniReadValues(string Section)
        {
            byte[] sectionByte = this.IniReadValues(Section, null);
            return this.ByteToString(sectionByte);
        }
        public byte[] IniReadValues(string section, string key)
        {
            byte[] retVal = new byte[0xff];
            GetPrivateProfileString(section, key, "", retVal, 0xff, this.path);
            return retVal;
        }
        /// <summary>
        /// 写入INI
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }


}
