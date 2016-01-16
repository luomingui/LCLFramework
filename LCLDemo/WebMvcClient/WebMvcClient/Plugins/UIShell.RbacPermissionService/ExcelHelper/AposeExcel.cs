using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspose.Cells;
using System.Collections;
using System.Xml;


namespace HeatingAppTools
{
    ///	类 编 号：UI_PU2010301_AposeExcel
    ///	类 名 称：AposeExcel
    ///	内容摘要：EXCEL通用版本导入导出类
    ///	完成日期：2012-9-19
    ///	编码作者：吴军
    /// </summary>
    public class AposeExcel : FileExcel
    {
        #region 自定义变量区

        /// <summary>
        /// EXCEL最大行数
        /// </summary>
        private const int EXCEL_MAX_LINE = 65534;

        /// <summary>
        /// 配置文件
        /// </summary>
        private XmlDocument xmlConfig;

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
        /// 属性名称：ExcelTempletFile
        /// 内容描述：需要用到的Excel模版文件完整名
        /// </summary>
        public string ExcelTempletFile;

        #endregion 自定义变量区

        #region 构造函数区

        /// <summary>
        /// 构造函数
        /// </summary>
        public AposeExcel()
        {

        }

        #endregion 构造函数区

        #region FileExcel方法实现区

        /// <summary> 
        /// 读取Excel文档 
        /// </summary> 
        /// <param name="Path">文件名称</param> 
        /// <returns>返回一个数据集</returns> 
        public override System.Data.DataTable Import(string Path, string strSheetName)
        {
            // 1. sheet没有指定,则默认为sheet1
            if (strSheetName.Length == 0)
            {
                strSheetName = "Sheet1";
            }

            // 2. 操作EXCEL

            // 2.a 打开EXCEL
            //  Workbook workBook = new Workbook(Path);
            Workbook workBook = new Workbook(Path, new Aspose.Cells.LoadOptions(Aspose.Cells.LoadFormat.Auto));

            // 2.b 获取当前的Sheet
            Worksheet workSheet = workBook.Worksheets[strSheetName];

            // 2.c 获取所有的Cells
            Cells cells = workSheet.Cells;

            // 2.d 获取数据
            //DataTable dt = cells.ExportDataTableAsString(0,0,cells.MaxDataRow+1,cells.MaxDataColumn+1,true);
            DataTable dt = cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

            return dt;
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="saveFileName">导出后的文件名</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public override bool Export(System.Data.DataTable dt, string Path)
        {
            //EXCEL导出按钮功能,此时设置OBJECTTYPE为"BTNEXPORT"
            this.ObjectType = "BTNEXPORT";
            // 1. 没有数据导出,弹出提示
            if (dt == null || dt.Rows.Count <= 0)
            {
                Exception ex = new Exception("无资料可导出!");
                this.GenericExceptionHandler(ex);
            }

            //1、调用LoadConfig方法，加载配置信息
            this.LoadConfig();
            //文件不存在,抛出异常
            if (!System.IO.File.Exists(this.ExcelTempletFile))
            {
                throw new System.IO.FileNotFoundException();
                return false;
            }


            // 2. 创建一个EXCEL的WorkBook
            Workbook workBook = new Workbook(this.ExcelTempletFile);
            try
            {

                Workbook workBookSave = this.FillDataExcel(workBook, dt);
                workBookSave.Save(Path);
            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }


            return true;
        }



        /// <summary>
        /// 方法名称：LoadConfig
        /// 内容描述：加载配置文件到xmlConfig，并根据objectType在配置文件中查找到到该类型需要用到的模版文件位置。         
        /// </summary>
        protected void LoadConfig()
        {
            try
            {
                //配置XML文件参见ExcelConfig.xml
                xmlConfig = new XmlDocument();
                // 当前运行程序
                string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
                // 运行程序路径
                strPath = strPath.Substring(0, strPath.LastIndexOf("\\") + 1);
                // 载入配置文件
                xmlConfig.Load(strPath + "ExcelConfig.xml");
                // 取得根节点
                XmlNode root = xmlConfig.ChildNodes[0];
                foreach (XmlNode child in root.ChildNodes)
                {
                    string strTempFilePath = "";
                    // 取得单据类型属性
                    XmlAttribute Type = child.Attributes["Name"];
                    // 如果此类型是传递过来的类型
                    if ((Type != null) && (Type.Value.ToString().Equals(this.ObjectType)))
                    {
                        // 模版文件路径
                        strTempFilePath = strPath + child.FirstChild.Attributes["Path"].Value.ToString();
                        // 模版文件
                        this.ExcelTempletFile = strTempFilePath + child.FirstChild.Attributes["Name"].Value.ToString();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("载入模板文件出错!\n" + ex.Message);
            }
        }

        /// <summary>
        /// 方法名称：WriteSecretLevel
        /// 方法描述：从DataSet中获取加密度信息，更改模版的页眉信息中的加密度信息以及水印。
        /// </summary>
        protected bool WriteSecretLevel(string strObjectType,  Workbook workBook)
        {
            try
            {
                // 记录加密度信息
                string strSec_Level = string.Empty;
                switch (strObjectType.ToUpper())
                {
                    //case "REQ":
                    //case "REQNEW":
                    //    // 取得通知单加密度信息
                    //    strSec_Level = this.DataInfo.Tables["CHG_REQUISITION_VIEW"].Rows[0]["SET_LEVEL_NAME"].ToString();
                    //    break;
                    //case "CHG":
                    //    // 取得更改单加密度信息
                    //    strSec_Level = this.DataInfo.Tables["CHG_CHANGEREQUISITION_VIEW"].Rows[0]["SEC_LEVEL_NAME"].ToString();
                    //    break;
                    //case "KIT":
                    //    strSec_Level = this.DataInfo.Tables["CHG_KIT_LIST"].Rows[0]["SEC_LEVEL_NAME"].ToString();
                    //    break;
                    default:
                        break;
                }

                // 修改记录:17 begin
                // 如果没有加密信息或加密信息为内部公开相同，则不需要重写加密信息
                if (strSec_Level == string.Empty
                        || strSec_Level == "内部公开")
                    return true;

                // 需要重写加密信息
                // 单据sheet
                Worksheet ws = workBook.Worksheets[0];
                string strErrMsg = string.Empty;


                //修改记录:18 begin 
                int iPrinterCount = -1;
                try
                {
                    iPrinterCount = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count;
                }
                catch (Exception ex)
                {
                    strErrMsg = "未安装打印驱动程序，不能导出。\r\n:"
                            + "内部错误：" + ex.Message;
                    // 显示提示信息
                    return false;
                }
                //修改记录:18 end

                //检查有没有安装打印机   
                if (iPrinterCount <= 0)
                {
                    strErrMsg = "未安装打印驱动程序，不能导出。请安装打印驱动程序后再导出！";
                }
                else
                {
                    try
                    {
                        // 写入加密度信息
                        //ws.PageSetup.RightHeader = strSec_Level + "▲";
                        ws.PageSetup.SetHeader(2, strSec_Level + "▲");
                    }
                    // 发生异常时，提醒用户,并中止导出
                    catch (Exception ex)
                    {
                        strErrMsg = "程序发生内部错误，无法导出\r\n"
                                + "内部错误:" + ex.Message;
                    }
                }
                // 显示提示信息
                if (strErrMsg != string.Empty)
                {
                    return false;
                }
                // 修改记录:17 end
            }
            catch (Exception ex)
            {
                throw new Exception("导出密级信息出错!\n" + ex.Message);
            }
            return true;
        }




        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="Path">导出后的文件路径</param>
        /// <param name="strTemplatePath">模板路径</param>
        /// <param name="iSheetStart">出数据可能需要多页，根据导出模板从指定sheet开始导出</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public override bool Export(System.Data.DataTable dt, string Path, string strTemplatePath, int iSheetStart)
        {
            // 1. 数据源不存在或者没有数据
            if (dt == null || dt.Rows.Count == 0)
            {
                Exception ex = new Exception("无资料可导出");
                this.GenericExceptionHandler(ex);
            }

            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                Workbook workBookTemplate = new Workbook(strTemplatePath);

                Workbook workBook = this.FillDataExcel(workBookTemplate, dt, iSheetStart);

                workBook.Save(Path);

            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }

            return true;

        }
        // 修改记录 02 开始
        /// <summary>
        /// 模板中多个sheet页数据的导出
        /// </summary>
        /// <param name="Dicvalue"></param>
        /// <param name="Path"></param>
        /// <param name="strTemplatePath"></param>
        /// <returns></returns>
        public bool ExportData(Dictionary<string, DataTable> Dicvalue, string Path, string strTemplatePath)
        {
            // 1. 数据源不存在或者没有数据
            if (Dicvalue == null || Dicvalue.Count == 0)
            {
                Exception ex = new Exception("无资料可导出");
                this.GenericExceptionHandler(ex);
            }
            if (Dicvalue != null)
            {
                foreach (KeyValuePair<string, DataTable> item in Dicvalue)
                {
                    if (item.Key == "" || item.Value == null || item.Value.Rows.Count == 0)
                    {
                        Exception ex = new Exception("无资料可导出");
                        this.GenericExceptionHandler(ex);
                    }
                }
            }

            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                Workbook workBookTemplate = new Workbook(strTemplatePath);

                Workbook workBook = this.FillDataExcel(workBookTemplate, Dicvalue);

                workBook.Save(Path);

            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }
            return true;
        }
        // 修改记录 02 结束
        /// <summary>
        /// 将数据回写至excel
        /// </summary>
        /// <param name="Path">EXCEL的路径</param>
        /// <param name="dt">需要反写的DataTable</param>
        /// <param name="strDtName">Sheet页名称</param>
        /// <param name="strColName">反写列名称</param>
        public override void DataToExcel(string Path, System.Data.DataTable dt, string strDtName, string strColName)
        {
            // 1. sheet没有指定,则默认为sheet1
            if (strDtName.Length == 0)
            {
                strDtName = "Sheet1";
            }

            // 2. 打开EXCEL
            Workbook workBook = new Workbook(Path);

            // 3. 打开Sheet页面
            Worksheet workSheet = workBook.Worksheets[strDtName];

            // 4. 获取Cells
            Cells cells = workSheet.Cells;

            // 5. 获取反写列的所处的位置
            int iColNum = this.GetColNumByColName(cells, strColName);

            if (iColNum == 0)
            {
                Exception ex = new Exception(strColName + "列不存在,请检查模板!");
                this.GenericExceptionHandler(ex);
                return;
            }

            // 6. 循环将相关数据写入数据
            int iRowCount = dt.Rows.Count;

            for (int i = 0; i < iRowCount; i++)
            {
                cells[i + 1, iColNum].PutValue(dt.Rows[i][strColName].ToString());
            }

            // 7. 保存EXCEL
            workBook.Save(Path);


        }
        /// <summary>
        /// 数据导出到Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strFilePath"></param>
        /// <param name="strSheetName"></param>
        /// <param name="strColName1"></param>
        /// <param name="strColName2"></param>
        public override void UpdateDataExcel(System.Data.DataTable dt, string strFilePath, string strSheetName, string strColName1, string strColName2)
        {
            // 1. 文件不存在,抛出异常
            if (!System.IO.File.Exists(strFilePath))
            {
                throw new System.IO.FileNotFoundException();
                return;
            }

            try
            {
                //2. 获取EXCEL 的WorkBook
                Workbook workBook = new Workbook(strFilePath);

                Worksheet workSheet = workBook.Worksheets[strSheetName];

                Cells cells = workSheet.Cells;
                int iColNum1 = this.GetColNumByColName(cells, strColName1);
                int iColNum2 = this.GetColNumByColName(cells, strColName2);

                // 3. 循环处理数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 存在列strColName1
                    if (iColNum1 != -1)
                    {
                        cells[i + 1, iColNum1].PutValue(dt.Rows[i]["是否成功"].ToString());
                    }
                    if (iColNum2 != -1)
                    {
                        cells[i + 1, iColNum2].PutValue(dt.Rows[i]["错误信息"].ToString());
                    }
                }
                workBook.Save(strFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 数据导出到Excel
        /// </summary>
        /// <param name="ht"></param>
        public override void UpdateDataExcel(Hashtable ht)
        {
            DataTable dt = null;
            string strFilePath = "";
            string strSheetName = "";
            string strdtColName1 = "";
            string strdtColName2 = "";
            string strexColName1 = "";
            string strexColName2 = "";
            if (ht.Contains("datatable"))
            {
                dt = (DataTable)ht["datatable"];
            }
            if (ht.Contains("FilePath"))
            {
                strFilePath = ht["FilePath"].ToString();
            }
            if (ht.Contains("SheetName"))
            {
                strSheetName = ht["SheetName"].ToString();
            }
            if (ht.Contains("dtColName1"))
            {
                strdtColName1 = ht["dtColName1"].ToString(); // 数据集列名
            }
            if (ht.Contains("dtColName2"))
            {
                strdtColName2 = ht["dtColName2"].ToString();// 数据集列名
            }
            if (ht.Contains("exColName1"))
            {
                strexColName1 = ht["exColName1"].ToString();// excel列名
            }
            if (ht.Contains("exColName2"))
            {
                strexColName2 = ht["exColName2"].ToString();// excel列名
            }
            // 1. 文件不存在,抛出异常
            if (!System.IO.File.Exists(strFilePath))
            {
                throw new System.IO.FileNotFoundException();
                return;
            }

            try
            {
                //2. 获取EXCEL 的WorkBook
                //  Workbook workBook = new Workbook(strFilePath);
                Workbook workBook = new Workbook(strFilePath, new Aspose.Cells.LoadOptions(Aspose.Cells.LoadFormat.Auto));
                Worksheet workSheet = workBook.Worksheets[strSheetName];

                Cells cells = workSheet.Cells;
                int iColNum1 = -1;
                int iColNum2 = -1;
                if (!string.IsNullOrEmpty(strexColName1))
                {
                    iColNum1 = this.GetColNumByColName(cells, strexColName1);
                    if (iColNum1 == -1)
                    {
                        cells.InsertColumn(cells.MaxDataColumn + 1);
                        cells[0, cells.MaxDataColumn + 1].PutValue(strexColName1);
                        workBook.Save(strFilePath);
                        // iColNum1 = cells.MaxDataColumn + 1;
                        iColNum1 = cells.MaxDataColumn;
                    }
                }

                if (!string.IsNullOrEmpty(strexColName2))
                {
                    iColNum2 = this.GetColNumByColName(cells, strexColName2);
                    if (iColNum2 == -1)
                    {
                        cells.InsertColumn(cells.MaxDataColumn + 1);
                        cells[0, cells.MaxDataColumn + 1].PutValue(strexColName2);
                        workBook.Save(strFilePath);
                        // iColNum2 = cells.MaxDataColumn + 1;
                        iColNum2 = cells.MaxDataColumn;
                    }
                }


                // 3. 循环处理数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 存在列strColName1
                    if (iColNum1 != -1)
                    {
                        cells[i + 1, iColNum1].PutValue(dt.Rows[i][strdtColName1].ToString());
                    }
                    if (iColNum2 != -1)
                    {
                        cells[i + 1, iColNum2].PutValue(dt.Rows[i][strdtColName2].ToString());
                    }
                }
                workBook.Save(strFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 修改记录 03 开始
        /// <summary>
        /// 将dt数据写入EXCEL
        /// 如果写入时超出EXCEL最大行，需新增SHEET页
        /// </summary>
        /// <param name="dt">DataTable(数据源)</param>
        /// <param name="Path">文件路径</param>
        /// <param name="iSheet">指定模板sheet页</param>
        /// <param name="iExportCount">已导出的数据总行数</param>
        /// <returns>导出是否成功，如果成功，则返回:true;否则，返回:false</returns>
        public bool DataToExcel(System.Data.DataTable dt, string Path, int iSheet, int iExportCount)
        {
            // 1. 数据源不存在或者没有数据
            if (dt == null || dt.Rows.Count == 0)
            {
                Exception ex = new Exception("无资料可导出");
                this.GenericExceptionHandler(ex);
            }

            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                Workbook workBook = new Workbook(Path);

                // 当前要写入的数据行数
                int currentRows = dt.Rows.Count;
                // 总的数据
                long totalRows = iExportCount + currentRows;

                // 需要的sheet页总个数
                int sheetsCount = int.Parse(Convert.ToString(Math.Ceiling((decimal)totalRows / (decimal)EXCEL_MAX_LINE)));

                // 已有的sheet页个数
                int oddSheetsCount = int.Parse(Convert.ToString(Math.Ceiling((decimal)iExportCount / (decimal)EXCEL_MAX_LINE)));

                Worksheet workSheet;
                string workSheetName = "Sheet";

                // 获取模板Sheet页
                workSheet = workBook.Worksheets[iSheet - 1];
                workSheetName = workSheet.Name;

                // 取datatable数据时的行和列
                int colIndex = 0;
                int RowIndex = 0;

                // datatable的总列数
                int colCount = dt.Columns.Count;
                // 每次写入EXCEL的行数
                int RowCount;

                // 创建数据缓存
                object[,] objData;
                // 列
                int j = 0;

                // 需要写入EXCEL的起始行
                int iStartRow;

                //如果总的SHEET页数不等于已有的SHEET页数，则需要新增一个SHEET页
                if (sheetsCount != oddSheetsCount)
                {
                    // 先填满当前sheet页，再新增sheet页继续添加
                    // 行数等于现有sheet页可写入的总行数减去已填加的行数
                    RowCount = (sheetsCount - 1) * EXCEL_MAX_LINE - iExportCount;
                    objData = new object[RowCount, colCount];
                    iStartRow = iExportCount - (sheetsCount - 2) * EXCEL_MAX_LINE + 1;
                    // 获取数据
                    for (RowIndex = 0; RowIndex < RowCount; RowIndex++, j++)
                    {
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                        }
                    }
                    // 写入EXCEL 
                    if (sheetsCount == 2)
                    {
                        workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                    }
                    else
                    {
                        workSheet = workBook.Worksheets[workSheetName + (sheetsCount - 1).ToString()];
                        workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                    }

                    //增加一个SHEET页继续添加数据
                    workBook.Worksheets.Add(workSheetName + sheetsCount.ToString());

                    workSheet = workBook.Worksheets[workSheetName + sheetsCount.ToString()];
                    workSheet.AutoFitColumns();
                    // 拷贝第一行数据(即为行标题)
                    workSheet.Cells.CopyRow(workBook.Worksheets[iSheet - 1].Cells, 0, 0);

                    RowCount = currentRows - RowCount;
                    objData = new object[RowCount, colCount];
                    iStartRow = 1;
                    j = 0;
                    // 获取数据
                    for (; RowIndex < currentRows; RowIndex++, j++)
                    {
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                        }
                    }
                    // 写入EXCEL 
                    workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                }
                else
                {
                    RowCount = currentRows;
                    objData = new object[RowCount, colCount];
                    iStartRow = iExportCount - (sheetsCount - 1) * EXCEL_MAX_LINE + 1;
                    // 获取数据
                    for (RowIndex = 0; RowIndex < RowCount; RowIndex++, j++)
                    {
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                        }
                    }
                    // 写入EXCEL 
                    if (sheetsCount == 1)
                    {
                        workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                    }
                    else
                    {
                        workSheet = workBook.Worksheets[workSheetName + sheetsCount.ToString()];
                        workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                    }
                }
                workBook.Save(Path);
            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }

            return true;

        }
        // 修改记录 03 结束

        #endregion FileExcel方法实现区

        #region 私有方法区

        /// <summary>
        /// 根据列名获取列所处的位置
        /// </summary>
        /// <param name="cells">所有单元格</param>
        /// <param name="strColName">列名</param>
        /// <returns>列所处的位置</returns>
        private int GetColNumByColName(Cells cells, string strColName)
        {
            int iColNum = -1;

            // 1. 从Cells获取DataTable
            // DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
            DataTable dt = cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

            // 2.获取DataTable 最大列数
            //   int iColumnCount = dt.Columns.Count;            
            //// 3. 循环判断
            //for (int i = 0; i < iColumnCount; i++)
            //{
            //    if (dt.Columns[i].ColumnName.Equals(strColName))
            //    {
            //        iColNum = i;
            //        break;
            //    }
            //}// end of for (int i = 0; i < iColumnCount; i++)
            iColNum = dt.Columns.IndexOf(strColName);

            // iColNum = iColNum < 1 ? dt.Columns.Count + 1 : iColNum;

            return iColNum;

        }
        // 修改记录 02 开始
        /// <summary>
        /// 将多个数据写入对应的模板sheet页中
        /// </summary>
        /// <param name="workBook">对应的模板</param>
        /// <param name="value">要写入模板的数据</param>
        /// <returns></returns>
        private Workbook FillDataExcel(Workbook workBook, Dictionary<string, DataTable> value)
        {
            // 这里假定要写入模板中的数据量不会超过1个sheet页
            Worksheet workSheet;
            foreach (KeyValuePair<string, DataTable> item in value)
            {
                workSheet = workBook.Worksheets[item.Key];// 模板sheet页
                DataTable dt = item.Value.Copy();// 要写入模板sheet页的数据
                // 行列索引
                int colIndex = 0;
                int RowIndex = 0;
                int colCount = dt.Columns.Count;
                int RowCount = dt.Rows.Count;
                // 创建数据缓存
                object[,] objData;
                // 列
                int j = 0;  // objData 存储数据 从0 开始
                // 需要写入EXCEL的其实行
                int iStartRow = 1;   // 写入EXCEL,从第二行开始
                // (1) 写入模板
                // 模板中最大的行是 2000行
                if (RowCount <= 2000)
                {
                    objData = new object[2000, colCount];
                }
                else
                {
                    objData = new object[RowCount, colCount];
                }
                // 获取数据
                for (RowIndex = 0; RowIndex < RowCount; RowIndex++, j++)
                {
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                    }
                }
                // 3.写入EXCEL   
                workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
            }
            return workBook;
        }
        // 修改记录 02 结束
        /// <summary>
        /// 将DataTable中的数据写入到EXCEL的WorkBook中
        /// </summary>
        /// <param name="workBook">需要写入的workBook</param>
        /// <param name="dt">DataTable(数据源) 传递过来的数据从1开始算起</param>
        /// <param name="iSheetStart">传递过来Sheet页数,数据从1开始算起</param>
        /// <returns>写入后的WorkBook</returns>
        private Workbook FillDataExcel(Workbook workBook, DataTable dt, int iSheetStart)
        {
            // 1. 每页写入的数据

            // 1.a 总的数据
            long totalRows = dt.Rows.Count;

            // 1.b 向上取整得到共需要多少个sheet页存放数据
            int sheetsCount = int.Parse(Convert.ToString(Math.Ceiling((decimal)totalRows / (decimal)EXCEL_MAX_LINE)));

            // 1.d 定义workSheet,
            Worksheet workSheet;
            string workSheetName = "Sheet";
            bool IsDataWriteTemplate = false;

            //2.循环写入数据
            for (int i = 0; i < sheetsCount; i++)
            {
                // (1)模板+第一页
                if (iSheetStart > 0 && i == 0)
                {
                    workSheet = workBook.Worksheets[iSheetStart - 1];
                    // 获取当前的Sheet名称
                    workSheetName = workSheet.Name;

                    // 写入模板
                    IsDataWriteTemplate = true;

                }
                // (2)模板 + 第N页(N>1)
                else if (iSheetStart > 0 && i > 0)
                {
                    workBook.Worksheets.Add(workSheetName + i.ToString());

                    workSheet = workBook.Worksheets[workSheetName + i.ToString()];
                    workSheet.AutoFitColumns();
                    // 拷贝第一行数据(即为行标题)
                    workSheet.Cells.CopyRow(workBook.Worksheets[iSheetStart - 1].Cells, 0, 0);

                    // 写入模板
                    IsDataWriteTemplate = true;

                }
                // (3) 非模板 
                else
                {
                    try
                    {
                        //workBook.Worksheets.Clear();
                        if (workBook.Worksheets[workSheetName + (i + 1).ToString()] == null)
                        {
                            workBook.Worksheets.Add(workSheetName + (i + 1).ToString());
                        }
                    }
                    catch
                    {

                    }
                    workSheet = workBook.Worksheets[workSheetName + (i + 1).ToString()];
                    workSheet.AutoFitColumns();
                    // 写入模板 
                    IsDataWriteTemplate = false;
                }

                // 2.b 列索引,行索引,总列数,总行数
                int colIndex = 0;
                int RowIndex = 0;

                int colCount = dt.Columns.Count;
                int RowCount;

                // 2.c 如果dt的总行数比Excel最后一页最后一笔数据的行号要小
                if (dt.Rows.Count < (i + 1) * EXCEL_MAX_LINE)
                {
                    RowCount = dt.Rows.Count;
                }
                else
                {
                    RowCount = (i + 1) * EXCEL_MAX_LINE;
                }

                // 2.d 相关数据
                // 创建数据缓存
                object[,] objData;

                // 列
                int j;

                // 需要写入EXCEL的其实行
                int iStartRow;

                // (1) 写入模板
                if (IsDataWriteTemplate)
                {
                    // 模板中最大的行是 2000行
                    if (RowCount <= 2000)
                    {
                        objData = new object[2000, colCount];
                    }
                    else
                    {
                        objData = new object[RowCount, colCount];
                    }
                    // objData 存储数据 从0 开始
                    j = 0;
                    // 写入EXCEL,从第二行开始
                    iStartRow = 1;
                }
                // (2) 写入EXCEL 非模板
                else
                {

                    objData = new object[RowCount + 1, colCount];
                    // 写入列数据
                    foreach (DataColumn dc in dt.Columns)
                    {
                        objData[RowIndex, colIndex++] = dc.ColumnName;
                    }
                    // objData 存在列标题,则存储数据 从1 开始
                    j = 1;
                    // 写入EXCEL ,从第一行开始写入
                    iStartRow = 0;
                }

                // 获取数据
                for (RowIndex = i * EXCEL_MAX_LINE; RowIndex < RowCount; RowIndex++, j++)
                {
                    for (colIndex = 0; colIndex < colCount; colIndex++)
                    {
                        objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                    }
                }

                // 3.写入EXCEL   

                workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, 0, true);
                // 修改记录 01 开始
                // 4,非模板数据的设置列字段的显示格式
                if (IsDataWriteTemplate == false)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //字段名称字体样式
                        Cell cell = workSheet.Cells[0, k];
                        Style style = new Style();
                        // 字体黑体
                        style.Font.IsBold = true;
                        style.Font.Size = 10;
                        // 背景色设置的颜色
                        ThemeColor thecolor = new ThemeColor(ThemeColorType.Accent3, 5.00);
                        //style.ForegroundThemeColor = thecolor;
                        //style.BackgroundThemeColor = thecolor;
                        style.ForegroundColor = System.Drawing.Color.Orange;
                        style.Pattern = BackgroundType.Solid;
                        // 居中垂直对齐
                        style.VerticalAlignment = TextAlignmentType.Right;
                        // 边框设置
                        style.Borders.DiagonalStyle = CellBorderType.None;
                        style.Borders.SetColor(System.Drawing.Color.Black);

                        cell.SetStyle(style);
                        workSheet.AutoFitColumn(k);// 列宽自动调整

                    }

                }
                // 修改记录 01 结束

            }// end of  for (int i = 0; i < sheetsCount; i++)
            return workBook;
        }


        /// <summary>
        /// 将DataTable中的数据写入到EXCEL的WorkBook中
        /// </summary>
        /// <param name="workBook">需要写入的workBook</param>
        /// <param name="dt">DataTable(数据源)</param>
        /// <returns>写入后的WorkBook</returns>
        private Workbook FillDataExcel(Workbook workBook, DataTable dt)
        {
            return this.FillDataExcel(workBook, dt, -1);
        }

        public override bool Export(DataSet ds, string Path, string strTemplatePath)
        {
            // 1. 数据源不存在或者没有数据
            if (ds == null || ds.Tables.Count == 0)
            {
                Exception ex = new Exception("无资料可导出");
                this.GenericExceptionHandler(ex);
            }

            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                Dictionary<string, DataTable> value = new Dictionary<string, DataTable>();
                foreach (DataTable dt in ds.Tables)
                {
                    value.Add(dt.TableName, dt);
                }
                Workbook workBookTemplate = new Workbook(strTemplatePath);

                Workbook workBook = this.FillDataExcel(workBookTemplate, value);

                workBook.Save(Path);

            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }

            return true;
        }
    
        /// <summary>
        /// 按导出模型导出数据
        /// </summary>
        /// <param name="direxportModel">导出模型</param>
        /// <param name="Path">导出文件路径</param>
        /// <param name="strTemplatePath">导出模板</param>
        /// <returns></returns>
        public bool ExportByExportModel(List<ExportModel> direxportModel, string Path, string strTemplatePath)
        {
            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                // 创建一个工作薄
                Workbook workBook = new Workbook(strTemplatePath);
                // 定义 sheet页对象
                Worksheet workSheet;
                // 循环
                foreach (ExportModel item in direxportModel)
                {

                    workSheet = workBook.Worksheets[item.SheetName]; // 模板sheet页
                    DataTable dt = item.dt.Copy();// 要写入模板sheet页的数据

                    // 行列索引
                    int colIndex = 0;
                    int RowIndex = 0;
                    int colCount = dt.Columns.Count;
                    int RowCount = dt.Rows.Count;
                    // 创建数据缓存
                    object[,] objData;
                    // 列
                    int j = 0;  // objData 存储数据 从0 开始
                    // 需要写入EXCEL的其实行
                    int iStartRow = item.WriteRowIndex;   // 数据写入EXCEL的行
                    int iFirstColumn = item.WriteColumnIndex;   //    数据写入EXCEL的列
                    // (1) 写入模板
                    //// 模板中最大的行是 2000行
                    if (RowCount <= 2000 && !item.IsSheetContainMoreDiagram)
                    {
                        objData = new object[2000, colCount];
                    }
                    else
                    {
                        objData = new object[RowCount, colCount];
                    }
                    // 获取数据
                    for (RowIndex = 0; RowIndex < RowCount; RowIndex++, j++)
                    {
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                        }
                    }
                    // 3.写入EXCEL   
                    workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, iFirstColumn, true);

                } // end of foreach (ExportModel item in direxportModel)

                workBook.Save(Path);

            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }

            return true;
        }

        //修改记录: 02 开始
        /// <summary>
        /// 按导出模型导出数据
        /// </summary>
        /// <param name="direxportModel">导出模型</param>
        /// <param name="Path">导出文件路径</param>
        /// <param name="strTemplatePath">导出模板</param>
        /// <returns></returns>
        public bool ExportByExportModel_MultiProd(List<ExportModel> direxportModel, string Path, string strTemplatePath,
            int multi_count, int prod_count)
        {
            // 1. 创建模板的EXCEL的WorkBook
            try
            {
                // 创建一个工作薄
                Workbook workBook = new Workbook(strTemplatePath);
                // 定义 sheet页对象
                Worksheet workSheet;
                // 循环
                foreach (ExportModel item in direxportModel)
                {

                    workSheet = workBook.Worksheets[item.SheetName]; // 模板sheet页
                    DataTable dt = item.dt.Copy();// 要写入模板sheet页的数据

                    // 行列索引
                    int colIndex = 0;
                    int RowIndex = 0;
                    int colCount = dt.Columns.Count;
                    int RowCount = dt.Rows.Count;
                    // 创建数据缓存
                    object[,] objData;
                    // 列
                    int j = 0;  // objData 存储数据 从0 开始
                    // 需要写入EXCEL的其实行
                    int iStartRow = item.WriteRowIndex;   // 数据写入EXCEL的行
                    int iFirstColumn = item.WriteColumnIndex;   //    数据写入EXCEL的列
                    // (1) 写入模板
                    //// 模板中最大的行是 2000行
                    if (RowCount <= 2000 && !item.IsSheetContainMoreDiagram)
                    {
                        objData = new object[2000, colCount];
                    }
                    else
                    {
                        objData = new object[RowCount, colCount];
                    }
                    if (item.SheetName == "输出报表2 型号规格发货数量")
                    {
                        int ichange = 3;
                        string compare = string.Empty;
                        //根据多模型号规格的名称来循环查询所有的重复数据,合并单元格.
                        //左上角x,y,行数,列数.
                        for (int i = 0; i < dt.Rows.Count; /*i++*/)
                        {
                            DataRow[] drs = dt.Select("型号规格 = '" + dt.Rows[i][0].ToString() + "'");

                            if (drs != null && drs.Length != 0)
                            {
                                int count = drs.Length + 1;
                                //获取当前的卡页
                                workSheet = workBook.Worksheets["输出报表2 型号规格发货数量"];
                                //获取样式
                                //Style style = workSheet.Cells[1, 32 + count].GetStyle();
                                //合并表格
                                for (int col = 0; col < multi_count + 24; col++)
                                {
                                    workSheet.Cells.Merge(ichange, col, drs.Length, 1);
                                    //填写多模的相关数据.
                                    workSheet.Cells[ichange, col].PutValue(drs[0][col].ToString());
                                    //ichange++;
                                }
                                //ichange++;
                                ichange += drs.Length;
                                i += drs.Length;
                                //修改表格样式
                                //Range range = workSheet.Cells.CreateRange(0, 32, 2, count);
                                //range.ApplyStyle(style, new StyleFlag() { HorizontalAlignment = true });
                            }
                        }
                        // 获取数据
                        for (RowIndex = 0; RowIndex < RowCount; RowIndex++)
                        {
                            for (colIndex = multi_count + 24; colIndex < colCount; colIndex++)
                            {
                                workSheet.Cells[RowIndex + 3, colIndex].PutValue("'" + dt.Rows[RowIndex][colIndex].ToString());
                            }
                        }
                    }
                    else
                    {
                        // 获取数据
                        for (RowIndex = 0; RowIndex < RowCount; RowIndex++, j++)
                        {
                            for (colIndex = 0; colIndex < colCount; colIndex++)
                            {
                                objData[j, colIndex] = "'" + dt.Rows[RowIndex][colIndex].ToString();
                            }
                        }
                        // 特殊处理
                        if (item.SheetName == "输出报表3 按型号规格统计报表（按多模汇总的型号规格）"
                     || item.SheetName == "输出报表4（按整机装配部件代码统计）"
                      || item.SheetName == "输出报表3 采购件生产领用数量统计报表")
                        {
                            // 先删除模板的行内容，三行
                            workSheet.Cells.DeleteRows(iStartRow, 4);
                            // 根据输出表的内容 再插入记录行
                            workSheet.Cells.InsertRows(iStartRow, RowCount);
                            //// 设置列的格式 为普通模式
                            for (int k = iStartRow; k < iStartRow + RowCount; k++)
                            {
                                for (colIndex = 0; colIndex < colCount; colIndex++)
                                {
                                    Cell cell = workSheet.Cells[k, colIndex];
                                    Style NormalStyle = workSheet.Cells[3, 13].GetStyle();
                                    NormalStyle.BackgroundColor = System.Drawing.Color.White;
                                    cell.SetStyle(NormalStyle);
                                }
                            }

                        }
                        // 3.写入EXCEL   
                        workSheet.Cells.ImportTwoDimensionArray(objData, iStartRow, iFirstColumn, true);
                    }
                  
                }  // end of foreach (ExportModel item in direxportModel)
                workBook.Save(Path);

            }
            catch (Exception ex)
            {
                this.GenericExceptionHandler(ex);
            }

            return true;
        }
        //修改记录: 02 结束

        #endregion 私有方法区
    }

    /// <summary>
    /// 导出模型
    /// </summary>
    public class ExportModel
    {
        /// <summary>
        /// 导入的DataTable
        /// </summary>
        public DataTable dt;
        /// <summary>
        /// Sheet页名称
        /// </summary>
        public string SheetName;

        private int _WriteRowIndex = 0;
        /// <summary>
        /// 写入Excel的行索引
        /// </summary>
        public int WriteRowIndex
        {
            get { return _WriteRowIndex; }
            set { _WriteRowIndex = value; }
        }

        private int _WriteColumnIndex = 0;
        /// <summary>
        /// 写入Excel的列索引
        /// </summary>
        public int WriteColumnIndex
        {
            get { return _WriteColumnIndex; }
            set { _WriteColumnIndex = value; }
        }
        private bool _IsSheetContainMoreDiagram = false;
        /// <summary>
        /// Sheet页是否包含多个图表 false 不包含，true 包含
        /// </summary>
        public bool IsSheetContainMoreDiagram
        {
            get { return _IsSheetContainMoreDiagram; }
            set { _IsSheetContainMoreDiagram = value; }
        }


    }
}
