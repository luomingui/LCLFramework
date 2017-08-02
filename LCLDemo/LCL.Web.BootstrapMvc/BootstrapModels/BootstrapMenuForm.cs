using LCL.Web.BootstrapMvc.Enums;
using System;
using System.Web.Mvc;

namespace LCL.Web.BootstrapMvc.BootstrapModels
{
    /// <summary>
    /// Represents a Bootstrap menu form element in an MVC view.
    /// </summary>
    public class BootstrapMenuForm : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilderExt _formTag;

        /// <summary>
        /// Create a new Bootstrap menu form in an MVC view.
        /// </summary>
        internal BootstrapMenuForm(ViewContext context)
        {
            _context = context;
        }

        internal void BeginList(MenuFormType menuFormType, HorizontalAlignment horizontalAlignment)
        {
            _formTag = new TagBuilderExt("form");

            switch (menuFormType)
            {
                case MenuFormType.MenuSearchForm:
                    _formTag.AddCssClass("navbar-search");
                    break;
                default:
                    _formTag.AddCssClass("navbar-form");
                    break;
            }

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    _formTag.AddCssClass("pull-right");
                    break;
                default:
                    _formTag.AddCssClass("pull-left");
                    break;
            }

            _context.Writer.WriteLine(_formTag.ToMvcHtmlString(TagRenderMode.StartTag));
        }

        internal void StopList()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(_formTag.ToMvcHtmlString(TagRenderMode.EndTag));
            _isStopped = true;
        }

        private void Dispose(bool cleanManaged)
        {
            StopList();

            if (!cleanManaged) return;

            // Clean up managed resources
            _formTag = null;
        }

        /// <summary>
        /// Dispose the BootstrapMenuForm and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}