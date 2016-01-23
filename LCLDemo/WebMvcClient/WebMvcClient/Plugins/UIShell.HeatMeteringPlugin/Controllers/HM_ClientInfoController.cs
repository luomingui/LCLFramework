/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 客户信息
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2016年1月12日
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIShell.HeatMeteringService;
using UIShell.RbacPermissionService;
using System.IO;
using System.Data;
using HeatingAppTools;
using System.Collections;

namespace UIShell.HeatMeteringService.Controllers
{
    public class HM_ClientInfoController : RbacController<HM_ClientInfo>
    {
        public HM_ClientInfoController()
        {

        }

        #region 一键导入
        public ActionResult ImportClient()
        {
            return View("ImportClient", null);
        }
        [HttpPost]
        public ActionResult ImportBizLogic()
        {
            DataTable dtImportDoc = null;
            var result = new Result(false);
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "TempDirectory\\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (Request.Files.Count > 0)
                {
                    foreach (string upload in Request.Files)
                    {
                        if (upload != null && upload.Trim() != "")
                        {
                            System.Web.HttpPostedFileBase postedFile = Request.Files[upload];
                            string filename = Path.GetFileName(postedFile.FileName);
                            if (filename.Length > 4)
                            {
                                string strExName = Path.GetExtension(filename);
                                switch (strExName.ToLower())
                                {
                                    case ".xls":
                                    case ".xlsx":
                                        string fileNamePath = path + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_temp_" + DateTime.Now.Ticks.ToString() + strExName;
                                        postedFile.SaveAs(fileNamePath);
                                        if (postedFile.ContentLength / 1024 <= 5120)
                                        {
                                            var excel = new AposeExcel();
                                            dtImportDoc = excel.Import(fileNamePath, "");
                                            string msg = string.Empty;
                                            if (dtImportDoc.Rows.Count > 3000)
                                            {
                                                result = new Result(false, "导入数据不能大于1000条！");
                                            }
                                            else
                                            {
                                                result = ValidateDataImportDoc(dtImportDoc, null);
                                                if (result.Success)
                                                {
                                                    //入库
                                                    RF.Concrete<IHM_ClientInfoRepository>().ImportClientDbo(dtImportDoc);
                                                    result = new Result(true);
                                                    try
                                                    {
                                                        System.IO.File.Delete(fileNamePath);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        
                                                    }
                                                }
                                                result.Message = result.Message + fileNamePath;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new Result(false,ex.Message);
            }

            return View("ImportClient", result);
        }
        //单机版 验证Excel数据
        private Result ValidateDataImportDoc(DataTable dtImportDoc, string filePath)
        {
            var result = new Result(false);
            // 添加错误列
            if (!dtImportDoc.Columns.Contains("是否成功"))
            {
                dtImportDoc.Columns.Add("是否成功", typeof(string));
            }
            if (!dtImportDoc.Columns.Contains("错误信息"))
            {
                dtImportDoc.Columns.Add("错误信息", typeof(string));
            }
            foreach (DataRow dr in dtImportDoc.Rows)
            {
                dr["是否成功"] = "";
                dr["错误信息"] = "";
            }
            Hashtable ht = new Hashtable();
            ht.Add("datatable", dtImportDoc);
            ht.Add("FilePath", filePath);
            ht.Add("SheetName", "sheet1");
            ht.Add("dtColName1", "是否成功");// 数据集列名
            ht.Add("dtColName2", "错误信息");// 数据集列名
            ht.Add("exColName1", "是否成功");// excel列名
            ht.Add("exColName2", "错误信息");// excel列名
            new AposeExcel().UpdateDataExcel(ht);
            //验证表格数据
            string strErrorMsg = "";
            for (int i = 0; i < dtImportDoc.Rows.Count; i++)
            {
                ValidateDataRow(dtImportDoc.Rows[i]);
            }
            for (int i = 0; i < dtImportDoc.Rows.Count; i++)
            {
                if (dtImportDoc.Rows[i]["是否成功"] != null
                    && dtImportDoc.Rows[i]["是否成功"].ToString().Equals("否"))
                {
                    int intRow = i + 1;
                    strErrorMsg += intRow.ToString() + ", ";
                }
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                if (strErrorMsg.Length > 0)
                {
                    result.DataObject = dtImportDoc;
                }
                else
                {
                    result = new Result(true);
                }
            }
            else
            {
                if (strErrorMsg.Length > 0)
                {
                    ExportDataTableToExcel(dtImportDoc, filePath);
                    result.DataObject = dtImportDoc;
                }
                else
                {
                    result = new Result(true);
                }
            }
            return result;
        }
        private void ExportDataTableToExcel(DataTable dtImportDoc, string strFileName)
        {//将清单数据表的错误信息写入Excel。
            AposeExcel file = new AposeExcel();
            Hashtable ht = new Hashtable();
            ht.Add("datatable", dtImportDoc);
            ht.Add("FilePath", strFileName);
            ht.Add("SheetName", "sheet1");
            ht.Add("dtColName1", "是否成功");// 数据集列名
            ht.Add("dtColName2", "错误信息");// 数据集列名
            ht.Add("exColName1", "是否成功");// excel列名
            ht.Add("exColName2", "错误信息");// excel列名
            file.UpdateDataExcel(ht);
        }
        private void ValidateDataRow(DataRow drExcelDoc)
        {
            if (string.IsNullOrEmpty(drExcelDoc["小区名称"].ToString()))
            {
                drExcelDoc["是否成功"] = "否";
                drExcelDoc["错误信息"] = "小区名称不可为空；";
            }
            if (string.IsNullOrEmpty(drExcelDoc["楼栋名称"].ToString()))
            {
                drExcelDoc["是否成功"] = "否";
                drExcelDoc["错误信息"] = "楼栋名称不可为空；";
            }
            if (string.IsNullOrEmpty(drExcelDoc["单元名称"].ToString()))
            {
                drExcelDoc["是否成功"] = "否";
                drExcelDoc["错误信息"] = "单元名称不可为空；";
            }
            if (string.IsNullOrEmpty(drExcelDoc["客户姓名"].ToString()))
            {
                drExcelDoc["客户姓名"] = "暂无";
            }
            switch (drExcelDoc["楼层"].ToString())
            {
                case "一层":
                    drExcelDoc["楼层"] = "1";
                    break;
                case "二层":
                    drExcelDoc["楼层"] = "2";
                    break;
                case "三层":
                    drExcelDoc["楼层"] = "3";
                    break;
                case "四层":
                    drExcelDoc["楼层"] = "4";
                    break;
                case "五层":
                    drExcelDoc["楼层"] = "5";
                    break;
                case "六层":
                    drExcelDoc["楼层"] = "6";
                    break;
                case "七层":
                    drExcelDoc["楼层"] = "7";
                    break;
                case "八层":
                    drExcelDoc["楼层"] = "8";
                    break;
                case "九层":
                    drExcelDoc["楼层"] = "9";
                    break;
                case "十层":
                    drExcelDoc["楼层"] = "10";
                    break;
                default:
                    drExcelDoc["楼层"] = "0";
                    break;
            }
        }
        #endregion
    }
}

