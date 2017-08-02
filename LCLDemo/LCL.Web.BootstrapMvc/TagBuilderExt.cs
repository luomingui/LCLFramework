using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Linq;

namespace LCL.Web.BootstrapMvc
{
    /// <summary>
    /// Extends the basic TagBuilder in order to better support hierarchical tags
    /// </summary>
    public class TagBuilderExt : TagBuilder
    {
        private Collection<TagBuilderExt> _childrenTags;

        #region Constructors

        /// <summary>
        /// Instantiates a new tag with a given name.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public TagBuilderExt(string tagName)
            : base(tagName)
        {
        }

        /// <summary>
        /// Instantiates a new tag with a given name and a set of child tags.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="childs">Child tags collection.</param>
        public TagBuilderExt(string tagName, IEnumerable<TagBuilderExt> childs)
            : base(tagName)
        {
            if (childs == null)
                return;

            foreach (var tag in childs)
            {
                ChildrenTags.Add(tag);
            }
        }

        /// <summary>
        /// Instantiates a new tag with a given name and a simple text content.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="innerText">Inner text of the tag.</param>
        public TagBuilderExt(string tagName, string innerText)
            : base(tagName)
        {
            if (!string.IsNullOrWhiteSpace(innerText))
                SetInnerText(innerText);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Lists all the tag inner tag.
        /// </summary>
        public Collection<TagBuilderExt> ChildrenTags
        {
            get { return _childrenTags ?? (_childrenTags = new Collection<TagBuilderExt>()); }
        }
        #endregion

        /// <summary>
        /// Add a tag as an inner tag of the current instance.
        /// </summary>
        /// <param name="child">Child tag</param>
        public void AddChildTag(TagBuilderExt child)
        {
            ChildrenTags.Add(child);
        }

        /// <summary>
        /// Creates a child tag with the specified name and HTML Content.
        /// </summary>
        /// <param name="tagName">Name of the child tag</param>
        /// <param name="content">Content of the child tag</param>
        /// <returns>Returns the <see cref="TagBuilderExt"/> for the child tag</returns>
        public TagBuilderExt CreateChildTag(string tagName, string content)
        {
            var child = new TagBuilderExt(tagName, content);
            AddChildTag(child);

            return child;
        }

        /// <summary>
        /// Creates a child tag with the specified name and HTML Content.
        /// </summary>
        /// <param name="tagName">Name of the child tag</param>
        /// <returns>Returns the <see cref="TagBuilderExt"/> for the child tag</returns>
        public TagBuilderExt CreateChildTag(string tagName)
        {
            return CreateChildTag(tagName, null);
        }

        /// <summary>
        /// Renders HTML code for the tag.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Renders HTML code for the tag as an <see cref="MvcHtmlString"/>.
        /// </summary>
        public MvcHtmlString ToMvcHtmlString()
        {
            return ToMvcHtmlString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Renders HTML code for the tag as an <see cref="MvcHtmlString"/>.
        /// </summary>
        /// <param name="tagRenderMode">Rendering mode used to create the HTML code.</param>
        public MvcHtmlString ToMvcHtmlString(TagRenderMode tagRenderMode)
        {
            return new MvcHtmlString(ToString(tagRenderMode));
        }

        /// <summary>
        /// Renders HTML code for the tag.
        /// </summary>
        /// <param name="tagRenderMode">Rendering mode used to create the HTML code.</param>
        /// <returns>HTML code for the tag and its child tags.</returns>
        public new string ToString(TagRenderMode tagRenderMode)
        {
            string result;

            switch (tagRenderMode)
            {
                case TagRenderMode.Normal:
                    result = base.ToString(TagRenderMode.StartTag);
                    result += InnerHtml;
                    result += string.Concat(ChildrenTags.SelectMany(c => c.ToString(TagRenderMode.Normal)));
                    result += base.ToString(TagRenderMode.EndTag);
                    break;
                case TagRenderMode.StartTag:
                    result = base.ToString(TagRenderMode.StartTag);
                    result += string.Concat(ChildrenTags.SelectMany(c => c.ToString(TagRenderMode.StartTag)));
                    break;
                case TagRenderMode.EndTag:
                    result = string.Concat(ChildrenTags.Reverse().SelectMany(c => c.ToString(TagRenderMode.EndTag)));
                    result += base.ToString(TagRenderMode.EndTag);
                    break;
                default:
                    result = base.ToString(tagRenderMode);
                    break;
            }
            return result;
        }

    }
}
