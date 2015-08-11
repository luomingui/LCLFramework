using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LCL.Tools
{
    /// <summary>
    /// 操作ini文件
    /// </summary>
    // [System.Diagnostics.DebuggerStepThrough]
    public class INIFile
    {
        public string path = System.IO.Path.GetTempPath() + @"\datatype.ini";
        public INIFile(string INIPath)
        {
            this.path = INIPath;
        }
        public INIFile( )
        {
            string path = System.IO.Path.GetTempPath() + @"\datatype.ini";
            if (!File.Exists(path))
            {
                StreamWriter sw2 = new StreamWriter(path, false, Encoding.Default);
                sw2.WriteLine(ini);
                sw2.Flush();
                sw2.Close();
                sw2.Dispose();
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


        #region ini 文件内容
        public  string ini = @"[DbToCS]
varchar=string
varchar2=string
nvarchar=string
nvarchar2=string
char=string
nchar=string
text=string
ntext=string
string=string
date=DateTime
datetime=DateTime
smalldatetime=DateTime
smallint=int
int=int
number=int
bigint=int
tinyint=int
float=decimal
numeric=decimal
decimal=decimal
money=decimal
smallmoney=decimal
real=decimal
bit=bool
binary=byte[]
varbinary=byte[]
image=byte[]
raw=byte[]
long=byte[]
long raw=byte[]
blob=byte[]
bfile=byte[]
uniqueidentifier=Guid
integer=int
double=decimal
enum=Enum
timestamp=DateTime
[ToSQLProc]
varchar=VarChar
string=VarChar
nvarchar=NVarChar
char=Char
nchar=NChar
text=Text
ntext=NText
datetime=DateTime
smalldatetime=SmallDateTime
smallint=SmallInt
tinyint=TinyInt
int=Int
bigint=BigInt
float=Float
real=Real
numeric=Decimal
decimal=Decimal
money=Money
smallmoney=SmallMoney
bool=Bit
bit=Bit
binary=Binary
varbinary=VarBinary
image=Image
uniqueidentifier=UniqueIdentifier
timestamp=Timestamp
[ToOraProc]
char=Char
varchar2=VarChar
string=VarChar
nvarchar2=NVarChar
nchar=NChar
long=LongVarChar
number=Number
int=Number
date=DateTime
raw=Raw
long raw=LongRaw
blob=Blob
bit=Clob
clob=Clob
nclob=NClob
bfile=BFile
[ToMySQLProc]
binary=Binary
bool=Bit
bit=Bit
blob=Blob
double=Double
Date=DateTime
datetime=DateTime
numeric=Decimal
decimal=Decimal
float=Float
enum=Enum
geometry=Geometry
longBlob=LongBlob
longText=LongText
varchar=VarChar
string=String
char=Char
text=Text
longtext=LongText
time=Time
SmallInt=Int32
TinyInt=Int32
timestamp=Timestamp
tinyText=TinyText
tinyBlob=TinyBlob
int=Int32
varbinary=VarBinary
varstring=VarString
year=Year
varchar=VarChar
[ToOleDbProc]
varchar=VarChar
string=VarChar
nvarchar=LongVarChar
char=Char
nchar=NChar
text=LongVarChar
ntext=LongVarChar
datetime=Date
smalldatetime=Date
smallint=SmallInt
tinyint=TinyInt
int=Integer
bigint=BigInt
money=Decimal
smallmoney=Decimal
float=Decimal
numeric=Decimal
decimal=Decimal
bool=Boolean
bit=Bit
binary=Binary
[IsAddMark]
nvarchar=true
nchar=true
ntext=true
varchar=true
varchar2=true
nvarchar2=true
char=true
clob=true
string=true
text=true
date=true
datetime=true
smalldatetime=true
uniqueidentifier=true
[isValueType]
int=true
Int32=true
Int16=true
Int64=true
DateTime=true
decimal=true
Decimal=true
";
        #endregion
    }


}
