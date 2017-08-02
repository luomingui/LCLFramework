using LCL.Web.BootstrapMvc.Enums;
using System;
using System.Web.Mvc;


namespace LCL.Web.BootstrapMvc.BootstrapModels
{
    /// <summary>
    /// Represents a Bootstrap Menu
    /// </summary>
    public class BootstrapMvcMenu : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilderExt _menuTag;

        internal BootstrapMvcMenu(ViewContext context)
        {
            _context = context;
        }

        internal void BeginMenu(MenuType type, bool isInversed)
        {
            _menuTag = new TagBuilderExt("div");
            _menuTag.AddCssClass("navbar");

            switch (type)
            {
                case MenuType.FixedTop:
                    _menuTag.AddCssClass("navbar-fixed-top");
                    break;
                case MenuType.FixedBottom:
                    _menuTag.AddCssClass("navbar-fixed-bottom");
                    break;
                case MenuType.StaticTop:
                    _menuTag.AddCssClass("navbar-static-top");
                    break;
            }

            if (isInversed)
                _menuTag.AddCssClass("navbar-inverse");
            var inner = new TagBuilderExt("div");
            inner.AddCssClass("navbar-inner");
            _menuTag.AddChildTag(inner);

            _context.Writer.WriteLine(_menuTag.ToString(TagRenderMode.StartTag));
        }

        internal void StopMenu()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_menuTag.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }

        private void Dispose(bool cleanManaged)
        {
            StopMenu();

            if (!cleanManaged) return;

            // Clean up managed resources
            _menuTag = null;
        }

        /// <summary>
        /// Dispose the BootstrapMvcList and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
