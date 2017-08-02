using System;
using System.Web.Mvc;

namespace LCL.Web.BootstrapMvc.BootstrapModels
{
    /// <summary>
    /// Represents a Bootstrap-themed breadcrumb 
    /// </summary>
    public class BootstrapBreadcrumb : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilder _breadcrumbsTag;

        /// <summary>
        /// Creates a new Bootstrap Breadcrumbs container for an MVC view.
        /// </summary>
        internal BootstrapBreadcrumb(ViewContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the opening tag of a Bootstrap breadcrumb
        /// </summary>
        internal void BeginBreadcrumb()
        {
            _breadcrumbsTag = new TagBuilderExt("ul");
            _breadcrumbsTag.AddCssClass("breadcrumb");
            _context.Writer.WriteLine(MvcHtmlString.Create(_breadcrumbsTag.ToString(TagRenderMode.StartTag)));
        }

        internal void StopBreadcrumb()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_breadcrumbsTag.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }


        private void Dispose(bool cleanManaged)
        {
            StopBreadcrumb();

            if (!cleanManaged) return;

            // Clean up managed resources
            _breadcrumbsTag = null;
        }

        /// <summary>
        /// Dispose the BootstrapBreadcrumb and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
