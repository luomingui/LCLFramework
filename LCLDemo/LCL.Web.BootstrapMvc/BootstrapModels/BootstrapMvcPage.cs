using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Linq;

namespace LCL.Web.BootstrapMvc.BootstrapModels
{
    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    public class BootstrapMvcPage : WebViewPage
    {
        private BootstrapHelper _bootstrap;

        /// <summary>
        /// Initializes the current page.
        /// </summary>
        protected override void InitializePage()
        {
            base.InitializePage();

            if (Request.Url == null) return;

            var currentUrl = Request.Url.AbsoluteUri;

            if(string.IsNullOrWhiteSpace(ViewBag.Title))
                return;

            if (!NavigationHistory.Any() || !NavigationHistory.Last().Equals(currentUrl))
                AddToHistory(ViewBag.Title, Request.Url);
        }

        /// <summary>
        /// Execute the server code in the current web page that is marked using the Razor syntax.
        /// </summary>
        public override void Execute()
        {

        }

        /// <summary>
        /// Provides a set of methods to generate HTML code for Bootstrap.
        /// </summary>
        public BootstrapHelper Bootstrap
        {
            get { return _bootstrap ?? (_bootstrap = new BootstrapHelper(this)); }
        }

        /// <summary>
        /// Adds an URI to the navigation history
        /// </summary>
        /// <param name="title">The visited page title</param>
        /// <param name="uri">The visited page URI</param>
        internal void AddToHistory(string title, Uri uri)
        {
            var history = Session["history"] as List<VisitedPage> ?? new List<VisitedPage>();
            history.Add(new VisitedPage { Title = title, Uri = uri});
            Session["history"] = history;
        }

        /// <summary>
        /// Provides the navigation history as a list of visited URLs.
        /// </summary>
        public IEnumerable<VisitedPage> NavigationHistory
        {
            get { return Session["history"] as List<VisitedPage> ?? new List<VisitedPage>(); }
        }
    }

    /// <summary>
    /// Represents an MVC web page with an included Bootstrap HTML helper.
    /// </summary>
    public class BootstrapMvcPage<TModel> : BootstrapMvcPage
        where TModel : class 
    {
        // The generic Bootstrap Helper
        public new BootstrapHelper<TModel> Bootstrap { get; set; }


        // code copied from source of ViewPage<T>

        private ViewDataDictionary<TModel> _viewData;

        public new AjaxHelper<TModel> Ajax { get; set; }

        public new HtmlHelper<TModel> Html { get; set; }

        public new TModel Model
        {
            get
            {
                return ViewData.Model;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public new ViewDataDictionary<TModel> ViewData
        {
            get
            {
                if (_viewData == null)
                {
                    SetViewData(new ViewDataDictionary<TModel>());
                }
                return _viewData;
            }
            set
            {
                SetViewData(value);
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            Ajax = new AjaxHelper<TModel>(ViewContext, this);
            Html = new HtmlHelper<TModel>(ViewContext, this);
        }

        protected override void SetViewData(ViewDataDictionary viewData)
        {
            _viewData = new ViewDataDictionary<TModel>(viewData);

            base.SetViewData(_viewData);
        }

    }
}
