using System;
using System.Web.Mvc;

namespace LCL.Web.BootstrapMvc.BootstrapModels
{
    /// <summary>
    /// Represents a Bootstrap MVC Menu item container.
    /// </summary>
    public class BootstrapMvcMenuItemsContainer : IDisposable
    {
        private readonly ViewContext _context;
        private bool _isStopped;
        private TagBuilderExt _listTag;

        /// <summary>
        /// Instantiates a Bootstrap MVC Menu item container.
        /// </summary>
        /// <param name="context">The current view context</param>
        internal BootstrapMvcMenuItemsContainer(ViewContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the opening tag for the Menu Item container.
        /// </summary>
        internal void BeginMenuItems()
        {
            _listTag = new TagBuilderExt("ul");
            _listTag.AddCssClass("nav");

            _context.Writer.WriteLine(_listTag.ToString(TagRenderMode.StartTag));
        }

        /// <summary>
        /// Closes the Menu Item container tag.
        /// </summary>
        internal void StopMenu()
        {
            if (_isStopped) return;

            _context.Writer.WriteLine(MvcHtmlString.Create(_listTag.ToString(TagRenderMode.EndTag)));
            _isStopped = true;
        }

        private void Dispose(bool cleanManaged)
        {
            StopMenu();

            if (!cleanManaged) return;

            // Clean up managed resources
            _listTag = null;
        }

        /// <summary>
        /// Dispose the <see cref="BootstrapMvcList"/> and call for garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
