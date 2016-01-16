using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace HeatingAppTools
{
    ///	类 编 号：UI_PU2010301_FileExcel
    ///	类 名 称：FileExcel
    ///	内容摘要：FileExcel导入导出类
    ///	完成日期：2011-04-06
    ///	编码作者：蔡先锋
    /// </summary>
    public abstract class FileExcel
    {
        public event FileExceptionHandle OnFileException;
       
        /// <summary>
        /// 属性名称：ObjectType
        /// 内容描述：根据构造函数传递的参数IsRequisition设定为是更改单还是通知单
        /// </summary>
        public string ObjectType;

        /// <summary>
        /// 产品大类编号
        /// </summary>
        public string OBJECTTYPE
        {
            get { return ObjectType; }
            set { ObjectType = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileExcel()
        {
        }

        /// <summary> 
        /// 读取Excel文档 
        /// </summary> 
        /// <param name="Path">文件名称</param> 
        /// <returns>返回一个数据集</returns> 
        public abstract DataTable Import(string Path,string strSheetName);

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="saveFileName">导出后的文件名</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public abstract bool Export(DataTable dt, string Path);

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="saveFileName">导出后的文件名</param>
        /// <param name="strTemplatePath">导出模板路径</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public abstract bool Export(DataTable dt, string Path, string strTemplatePath, int iSheetStart);

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="saveFileName">导出后的文件名</param>
        /// <param name="strTemplatePath">导出模板路径</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public abstract bool Export(DataSet ds, string Path, string strTemplatePath);

		/// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="dt"></param>
        /// <param name="strDtName"></param>
        public abstract void DataToExcel(string Path, DataTable dt, string strDtName, string strColName);
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        internal virtual void GenericExceptionHandler(Exception ex)
        {
            if (this.OnFileException != null)
            {
                this.OnFileException(ex);
            }
            throw ex;
        }
        /// <summary>
        /// 用ApplicationClass的方式更新EXCEL指定列
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="strSheetName">sheet页名称</param>
        /// <param name="strColName">指定列,EXCEL的真正列名(列如:AM)</param>
        public abstract void UpdateDataExcel(DataTable dt, string strFilePath, string strSheetName, string strColName1,string strColName2);

        /// <summary>
        /// 用ApplicationClass的方式更新EXCEL指定列
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="strSheetName">sheet页名称</param>
        /// <param name="strColName">指定列,EXCEL的真正列名(列如:AM)</param>
        public abstract void UpdateDataExcel(Hashtable ht);


    }
    public delegate void FileExceptionHandle(Exception ex);
}
