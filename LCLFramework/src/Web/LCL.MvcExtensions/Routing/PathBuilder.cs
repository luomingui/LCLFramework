/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Web;

namespace LCL.MvcExtensions
{
    internal class PathBuilder
    {
        public PathBuilder(string pattern, RouteValueDictionary defaults, RouteValueDictionary constraints)
        {
            this.Pattern = pattern;

            this.Defaults = (defaults ?? new RouteValueDictionary()).ToDictionary(
                p => p.Key,
                p => p.Value.ToString(),
                StringComparer.OrdinalIgnoreCase);

            this.Constraints = (constraints ?? new RouteValueDictionary()).ToDictionary(
                p => p.Key,
                p => new Regex(p.Value.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase),
                StringComparer.OrdinalIgnoreCase);

            this.ParsePattern(this.Pattern);
        }

        public string Pattern { get; set; }

        public Dictionary<string, Regex> Constraints { get; private set; }

        public Dictionary<string, string> Defaults { get; private set; }

        private void ParsePattern(string input)
        {
            var regex = @"\{\*?([^*]+?)\}";
            var matches = Regex.Matches(input, regex).Cast<Match>().ToArray();

            var isConstant = new List<bool>();
            var values = new List<string>();

            if (matches.Length <= 0)
            {
                isConstant.Add(true);
                values.Add(input);
            }
            else
            {
                if (matches[0].Index != 0)
                {
                    isConstant.Add(true);
                    values.Add(input.Substring(0, matches[0].Index));
                }

                for (int i = 0; i < matches.Length; i++)
                {
                    isConstant.Add(false);
                    values.Add(matches[i].Groups[1].Value);

                    var nextIndex = matches[i].Index + matches[i].Length;
                    if (i < matches.Length - 1)
                    {
                        isConstant.Add(true);
                        values.Add(input.Substring(nextIndex, matches[i + 1].Index - nextIndex));
                    }
                    else if (nextIndex < input.Length)
                    {
                        isConstant.Add(true);
                        values.Add(input.Substring(nextIndex));
                    }
                }
            }

            this.PartConstantMap = isConstant.ToArray();
            this.PartValues = values.ToArray();

            this.UsefulNames = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < this.PartValues.Length; i++)
            {
                if (!this.PartConstantMap[i])
                {
                    this.UsefulNames[this.PartValues[i]] = null;
                }
            }

            foreach (var pair in this.Defaults)
            {
                this.UsefulNames[pair.Key] = null;
            }
        }

        public bool[] PartConstantMap { get; private set; }

        public string[] PartValues { get; private set; }

        public Dictionary<string, object> UsefulNames { get; private set; }

        public string Build(RouteValueDictionary routeValues)
        {
            return Build_V2(routeValues);
        }

        public string Build_V2(RouteValueDictionary routeValues)
        {
            var queries = this.GetQueries(routeValues);
            var parts = new string[this.PartValues.Length + queries.Count * 4];

            if (!this.FillPath(parts, routeValues)) return null;

            if (queries.Count > 0) this.FillQueries(parts, queries);

            return String.Concat(parts);
        }

        private void FillQueries(string[] parts, List<KeyValuePair<string, object>> queries)
        {
            int i = this.PartValues.Length;
            foreach (var p in queries)
            {
                parts[i++] = "&";
                parts[i++] = p.Key;
                parts[i++] = "=";
                parts[i++] = HttpUtility.UrlEncode(p.Value.ToString());
            }

            parts[this.PartValues.Length] = "?";
        }

        private bool FillPath(string[] parts, RouteValueDictionary routeValues)
        {
            var canOmitPart = true;

            for (int i = this.PartValues.Length - 1; i >= 0; i--)
            {
                var partValue = this.PartValues[i];

                if (this.PartConstantMap[i])
                {
                    if (canOmitPart && partValue == "/")
                    {
                        parts[i] = "";
                    }
                    else
                    {
                        parts[i] = partValue;
                        canOmitPart = false;
                    }
                }
                else
                {
                    string defaultValue;

                    var routeValue = routeValues[partValue];
                    if (routeValue == null)
                    {
                        if (!this.Defaults.TryGetValue(partValue, out defaultValue)) return false;

                        if (canOmitPart)
                        {
                            parts[i] = "";
                        }
                        else
                        {
                            parts[i] = defaultValue;
                        }

                        continue;
                    }

                    var value = routeValue.ToString();

                    Regex constaint;
                    if (this.Constraints.TryGetValue(partValue, out constaint) &&
                        !constaint.IsMatch(partValue))
                    {
                        return false;
                    }

                    if (!this.Defaults.TryGetValue(partValue, out defaultValue))
                    {
                        parts[i] = value;
                        canOmitPart = false;
                    }
                    else if (canOmitPart && defaultValue.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        parts[i] = "";
                    }
                    else
                    {
                        parts[i] = value;
                        canOmitPart = false;
                    }
                }
            }

            return true;
        }

        private List<KeyValuePair<string, object>> GetQueries(RouteValueDictionary routeValues)
        {
            var result = new List<KeyValuePair<string, object>>(routeValues.Count);
            foreach (var p in routeValues)
            {
                if (!this.UsefulNames.ContainsKey(p.Key))
                {
                    result.Add(p);
                }
            }

            return result;
        }
    }
}
