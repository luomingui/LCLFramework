using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace LCL.Tools
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 版本号： 如 1.0.0
        /// </summary>
        public const string ASSEMBLY_VERSION = "1.0.0";
        /// <summary>
        /// 版本年份： 2010
        /// </summary>
        public const string ASSEMBLY_YEAR = "2010";

        /// <summary>
        /// 命名空间
        /// </summary>
        public static string NameSpace = "SF.Demo";
        public static string NameSpaceEntities = NameSpace + ".Entities";
        public static string NameSpaceUI = NameSpace + ".WinUI";
        
        /// <summary>
        ///  字符串 截取位： 如： NT_CLCS_|TM|QQ ......
        /// </summary>
        public static string TargetFolder = "";

        public static string Author = "罗敏贵";
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string Sqlserver = ".";
        /// <summary>
        /// 数据库SA账号
        /// </summary>
        public static string User = "sa";
        /// <summary>
        /// 数据库SA密码
        /// </summary>
        public static string Pwd = "123456";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnStr
        {
            get
            {
                string temp = "user id=" + User + ";password=" + Pwd + ";initial catalog=" + dbName + ";data source=" + Sqlserver;
                return temp;
            }
        }
        /// <summary>
        /// 数据库名 默认是：master
        /// </summary>
        public static string dbName = "master";

        public static DataBaseType DataBaseType = DataBaseType.Sql2008;

        public static List<DataBaseModel> DBModelList;

        #region

        /// <summary>
        /// 截取字符串 截取  过滤 _ 首字母大写
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetTagFolder(string strName)
        {
            if (string.IsNullOrEmpty(strName)) return "";
            string[] split = TargetFolder.Split(new char[] { '|' });
            if (split.Length > 1)
            {
                for (int i = 0; i < split.Length; i++)
                {
                    strName = strName.Replace(split[i], "").Replace("_", "");
                }
            }
            else
            {
                strName.Replace("_", "");
            }
            return GetCapFirstName(strName);
        }
        /// <summary>
        /// 将指定的字符串转换为词首字母大写。
        /// </summary>
        public static string GetCapFirstName(string strName)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strName.ToLower());
        }
        /// <summary>
        /// 清楚 \n 换行
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLine(string str)
        {
            string xmlstr = "";
            try
            {
                string[] split = str.Split(new char[] { '\n' });
                if (split.Length > 0)
                {
                    for (int i = 0; i < split.Length; i++)
                    {
                        xmlstr = xmlstr + split[i].Trim();
                    }
                    return xmlstr;
                }
                else
                {
                    xmlstr = str.Trim(); ;
                    return xmlstr;
                }
            }
            catch
            {
                xmlstr = str.Trim();
                return xmlstr;
            }


        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="Folder"></param>
        public static void FolderCheck(string Folder)
        {
            DirectoryInfo info = new DirectoryInfo(Folder);
            if (!info.Exists)
            {
                info.Create();
            }
        }
        /// <summary>
        ///  创建文件
        /// </summary>
        /// <param name="path">创建路径</param>
        /// <param name="filescontent">文件内容</param>
        public static void CreateFiles(string path, string filescontent)
        {
            StreamWriter sw2 = new StreamWriter(path, false, Encoding.Default);
            sw2.WriteLine(filescontent);
            sw2.Flush();
            sw2.Close();
            sw2.Dispose();
        }
        public static string MGRTrim(string str)
        {
            try
            {
                return str.Substring(0, str.Length - 1);
            }
            catch
            {
                str.Remove(str.Length - 1);
                return str;
            }
        }
        #endregion

        #region ini 文件内容
        public static string ini = @"[DbToCS]
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

    public enum DataBaseType
    {
        Sql2000,
        Sql2005,
        Sql2008,
    }
}
