using LCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EFDemo.Document
{
    /// <summary>
    /// 为指定实体模型生成对应的属性文档对象列表
    /// </summary>
    class PropertyDocParser
    {
        public string Comment { get; set; }
        public string ClassContent { get; set; }
        public Type TypeContext { get; set; }
        public Type[] Types { get; set; }
        private List<PropertyDoc> _result;
        public IList<PropertyDoc> Parse()
        {
            if (string.IsNullOrWhiteSpace(this.ClassContent)) throw new ArgumentNullException("this.ClassContent");
            this.ParseCommentsFromContent();
            this.ParseCommentsFromPartialContent();
            this._result = new List<PropertyDoc>();
            this.ParseDoc();
            this.Comment = this.GetComment("class");

            #region MyRegion
            this._result.Add(new PropertyDoc
            {
                PropertyName = "ID",
                Comment = "编号",
            });
            this._result.Add(new PropertyDoc
            {
                PropertyName = "AddDate",
                Comment = "添加时间",
            });
            this._result.Add(new PropertyDoc
            {
                PropertyName = "UpdateDate",
                Comment = "更新时间",
            });
            #endregion

            if (TypeContext.BaseType.Name == "BaseTreeModel")
            {
                this._result.Add(new PropertyDoc
                {
                    PropertyName = "IsLast",
                    Comment = "是否最后一级",
                });
                this._result.Add(new PropertyDoc
                {
                    PropertyName = "Level",
                    Comment = "树形深度",
                });
                this._result.Add(new PropertyDoc
                {
                    PropertyName = "NodePath",
                    Comment = "树形路径",
                });
                this._result.Add(new PropertyDoc
                {
                    PropertyName = "OrderBy",
                    Comment = "排序",
                });
                this._result.Add(new PropertyDoc
                {
                    PropertyName = "ParentId",
                    Comment = "上一级",
                });
            }

            return this._result;
        }
        #region 解析注释
        private Dictionary<string, string> _propertyToComment;
        private void ParseCommentsFromContent()
        {
            this._propertyToComment = new Dictionary<string, string>();
            var matches = Regex.Matches(this.ClassContent, @"<summary\>(?<content>.+?)\</summary\>.+?public partial \S+ (?<name>\S+)\s",
                RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                var name = match.Groups["name"].Value;
                var comment = match.Groups["content"].Value;
                comment = Regex.Replace(comment, @" +/// ", string.Empty).Trim();
                comment = Regex.Replace(comment, "///", string.Empty).Trim();
                this._propertyToComment[name] = comment;
            }
        }
        private void ParseCommentsFromPartialContent()
        {
            this._propertyToComment = new Dictionary<string, string>();
            var matches = Regex.Matches(this.ClassContent, @"<summary\>(?<content>.+?)\</summary\>.+?public \S+ (?<name>\S+)\s",
                RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                var name = match.Groups["name"].Value;
                var comment = match.Groups["content"].Value;
                comment = Regex.Replace(comment, @" +/// ", string.Empty).Trim();
                comment = Regex.Replace(comment, "///", string.Empty).Trim();
                this._propertyToComment[name] = comment;
            }
        }
        private string GetComment(string property)
        {
            string value = null;
            if (!this._propertyToComment.TryGetValue(property, out value))
            {
                value = string.Empty;
            }
            return value;
        }
        #endregion

        private void ParseDoc()
        {
            PropertyInfo[] fields = TypeContext.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (PropertyInfo field in fields)
            {
                var doc = new PropertyDoc();
                doc.Label = field.Name;
                string propertyName = "";
                if (TypeHelper.IsExist(field.PropertyType))
                {
                    propertyName = field.Name;
                    doc.Comment = this.GetComment(field.Name);
                }
                else
                {
                    var _type = Types.SingleOrDefault<Type>(e => e.Name.Trim() == field.Name.Trim());
                    if (_type != null)
                    {
                        propertyName = field.Name + "_ID";
                        doc.Comment = this.GetComment(field.Name);
                    }
                    else
                    {
                        propertyName = field.Name;
                        doc.Comment = this.GetComment(field.Name);
                    }
                }
                doc.PropertyName = propertyName;

                if (propertyName != "ID" || propertyName != "AddDate" || propertyName != "UpdateDate")
                    this._result.Add(doc);

            }

            fields = TypeContext.BaseType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (PropertyInfo field in fields)
            {
                var doc = new PropertyDoc();
                doc.PropertyName = field.Name;
                doc.Label = field.Name;
                doc.Comment = this.GetComment(field.Name);
                this._result.Add(doc);
            }
        }
    }
}