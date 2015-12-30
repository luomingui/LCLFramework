using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.JsonService
{
    /// <summary>
    /// Flow 的摘要说明
    /// </summary>
    public class Flow : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["method"];
            if (!string.IsNullOrWhiteSpace(method))
            {
                switch (method.ToLower())
                {
                    case "legend"://图例   
                        GetRoutJsonById(context);
                        break;
                    default:
                        break;
                }
            }

        }
        public void GetRoutJsonById(HttpContext context)
        {
            string routId = context.Request["routId"];
            var actor = RF.Concrete<IWFActorRepository>();
            var list = actor.GetByRoutId(Guid.Parse(routId));
            List<NodeModle> legend = new List<NodeModle>();
            foreach (var item in list)
            {
                NodeModle model = new NodeModle();

                model.NodeName = item.Name;
                model.Text = item.Name;
                model.Visible = true;
                model.Status = NodeModle.STATUS.W;
                model.DisplayType = NodeModle.DISPALY_TYPE.TEXT;
                model.NodeType = NodeModle.NODE_TYPE.FLOWNODE;

                var actorUsers = actor.GetActorUserByActorId(item.ID);
                if (actorUsers != null && actorUsers.Count > 0)
                {
                    model.subNodes = new List<NodeModle>();
                    foreach (var au in actorUsers)
                    {
                        model.subNodes.Add(new NodeModle
                        {
                            NodeName = au.User.Name,
                            Text = au.User.Name,
                            Visible = true,
                            Status = NodeModle.STATUS.W,
                            DisplayType = NodeModle.DISPALY_TYPE.TEXT,
                            NodeType = NodeModle.NODE_TYPE.TASKNODE
                        });
                    }
                }
                legend.Add(model);
            }

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string jsonStr =jsonSerializer.Serialize(legend);
            context.Response.Write(jsonStr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 节点模型类
    /// </summary>
    public class NodeModle
    {
        private string _NodeName = string.Empty;
        private string _Text = string.Empty;
        private NODE_TYPE _NODE_TYPE = NODE_TYPE.FLOWNODE;
        private DISPALY_TYPE _DISPALY_TYPE = DISPALY_TYPE.TEXT;
        private List<NodeModle> _Listnode = new List<NodeModle>();
        private bool _Visible = true;
        private STATUS _status = STATUS.W;

        /// <summary>
        /// 显示类型
        /// </summary>
        public enum DISPALY_TYPE : int
        {
            /// <summary>
            /// 文本
            /// </summary>
            TEXT = 0,
            /// <summary>
            /// 超链接
            /// </summary>
            LINK = 1
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public enum NODE_TYPE : int
        {
            /// <summary>
            /// 阶段
            /// </summary>
            FLOWNODE = 0,
            /// <summary>
            /// 阶段--任务
            /// </summary>
            TASKNODE = 1,
            /// <summary>
            /// 任务字段
            /// </summary>
            DETAILNODE = 2
        }

        /// <summary>
        /// 节点状态：F-完成，R-进行中，其它表示未开始
        /// </summary>
        public enum STATUS : int
        {
            /// <summary>
            /// 完成
            /// </summary>
            F = 1,
            /// <summary>
            /// 运行中
            /// </summary>
            R = 2,
            /// <summary>
            /// 未处理
            /// </summary>
            W = 3,
            /// <summary>
            /// 取消(流程正常结束)
            /// </summary>
            C = -1
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName
        {
            get
            {
                return this._NodeName.Trim();
            }
            set
            {
                this._NodeName = value;
            }
        }

        /// <summary>
        /// 节点值文本
        /// </summary>
        public string Text
        {
            get
            {
                return this._Text.Trim();
            }
            set
            {
                this._Text = value;
            }
        }

        /// <summary>
        /// 显示类型，如：text，link等
        /// </summary>
        public DISPALY_TYPE DisplayType
        {
            get
            {
                return this._DISPALY_TYPE;
            }
            set
            {
                this._DISPALY_TYPE = value;
            }
        }

        /// <summary>
        /// 节点类型：如flownode,tasknode,detailnode等
        /// </summary>
        public NODE_TYPE NodeType
        {
            get
            {
                return this._NODE_TYPE;
            }
            set
            {
                this._NODE_TYPE = value;
            }
        }

        /// <summary>
        /// 下层节点集合
        /// </summary>
        public List<NodeModle> subNodes
        {
            get
            {
                return this._Listnode;
            }
            set
            {
                this._Listnode = value;
            }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return this._Visible;
            }
            set
            {
                this._Visible = value;
            }
        }

        /// <summary>
        /// 节点状态
        /// </summary>
        public STATUS Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
    }
}