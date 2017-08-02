using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using LCL.Web.BootstrapMvc.BootstrapModels;
using LCL.Web.BootstrapMvc.Enums;

namespace LCL.Web.BootstrapMvc
{
    /// <summary>
    /// Provides methods to generate HTML code for Twitter Bootstrap.
    /// </summary>
    public class BootstrapHelper
    {
        #region Fields
        /// <summary>
        /// Represents the instance of the <see cref="HtmlHelper"/> associated with this <see cref="BootstrapMvcPage"/>.
        /// </summary>
        protected readonly HtmlHelper Html;
        /// <summary>
        /// Represents the instance of the <see cref="UrlHelper"/> associated with this <see cref="BootstrapMvcPage"/>.
        /// </summary>
        protected readonly UrlHelper Url;
        /// <summary>
        /// Represents the instance of the <see cref="BootstrapMvcPage"/> associated to this <see cref="BootstrapHelper"/>
        /// </summary>
        protected readonly BootstrapMvcPage Page;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the Bootstrap helper.
        /// </summary>
        /// <param name="page">The current view</param>
        public BootstrapHelper(BootstrapMvcPage page)
        {
            Page = page;
            Html = page.Html;
            Url = page.Url;
        }
        #endregion

        #region Internal Helpers

        /// <summary>
        /// Checks if an Url is matching the current view.
        /// </summary>
        /// <param name="url">Url to check</param>
        internal bool IsCurrentUrl(string url)
        {
            var currentUrl = Html.ViewContext.RequestContext.HttpContext.Request.Url;
            if (currentUrl == null) return false;
            return currentUrl.AbsoluteUri.Equals(url) || currentUrl.AbsolutePath.Equals(url);
        }
        #endregion

        #region Typography

        /// <summary>
        /// Creates an HTML abbreviation with it's corresponding definition.
        /// </summary>
        /// <param name="title">Definition of the abbreviation.</param>
        /// <param name="value">The abbreviation.</param>
        public MvcHtmlString Abbreviation(string title, string value)
        {
            return Abbreviation(title, value, false);
        }
        /// <summary>
        /// Creates an HTML abbreviation with it's corresponding definition.
        /// </summary>
        /// <param name="title">Definition of the abbreviation.</param>
        /// <param name="value">The abbreviation.</param>
        /// <param name="isReduced">
        /// Defines if the abbreviation uses the <c>initialism</c> class for a slightly
        /// smaller font-size.
        /// </param>
        public MvcHtmlString Abbreviation(string title, string value, bool isReduced)
        {
            var abbr = new TagBuilderExt("abbr", value);
            if (isReduced)
                abbr.AddCssClass("initialism");
            abbr.MergeAttribute("title", title);

            return abbr.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates an HTML block-quote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <param name="author">The author.</param>
        /// <param name="source">The source.</param>
        /// <param name="sourceTitle">
        /// The <paramref name="source"/> title.
        /// </param>
        public MvcHtmlString BlockQuote(string quote, string author, string source, string sourceTitle)
        {
            return BlockQuote(quote, author, source, sourceTitle, false);
        }
        /// <summary>
        /// Creates an HTML block-quote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <param name="author">The author.</param>
        /// <param name="source">The source.</param>
        /// <param name="sourceTitle">
        /// The <paramref name="source"/> title.
        /// </param>
        /// <param name="isPulledRight">
        /// Set to <see langword="true"/> for a floated, right-aligned
        /// blockquote.
        /// </param>
        public MvcHtmlString BlockQuote(string quote, string author, string source, string sourceTitle, bool isPulledRight)
        {
            var blockquote = new TagBuilderExt("blockquote");
            if (isPulledRight)
                blockquote.AddCssClass("pull-right");

            var cite = new TagBuilderExt("cite", source);
            cite.MergeAttribute("title", sourceTitle);

            blockquote.CreateChildTag("p", quote);
            blockquote.CreateChildTag("small").InnerHtml = String.Concat(author, " ", cite.ToString());

            return blockquote.ToMvcHtmlString();
        }

        /// <summary>
        /// Begins a new list tag
        /// </summary>
        /// <param name="listType">Type of the desired list.</param>
        /// <returns>
        /// 
        /// </returns>
        public BootstrapMvcList BeginList(ListType listType)
        {
            var list = new BootstrapMvcList(Html.ViewContext);
            list.BeginList(listType);

            return list;
        }
        /// <summary>
        /// Creates a list with the associated elements.
        /// </summary>
        /// <param name="listType">The type of the list.</param>
        /// <param name="elements">The elements of the list.</param>
        public MvcHtmlString List(ListType listType, IEnumerable<string> elements)
        {
            if (elements == null) return null;

            var root = BootstrapMvcList.GetRootTagBuilder(listType);

            foreach (var element in elements)
            {
                root.CreateChildTag("li", element);
            }

            return root.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates a list of terms with their associated descriptions.
        /// </summary>
        /// <param name="isHorizontal">
        /// Make terms and descriptions in the description list line up side-by-side.
        /// </param>
        public BootstrapMvcList BeginDescriptionList(bool isHorizontal)
        {
            var list = new BootstrapMvcList(Html.ViewContext);
            list.BeginDescriptionList(isHorizontal);

            return list;
        }
        /// <summary>
        /// Creates a description list with the associated descriptions
        /// </summary>
        /// <param name="isHorizontal">
        /// Make terms and descriptions in line up side-by-side.
        /// </param>
        /// <param name="elements">
        /// The dictionary of descriptions by title (key) and description
        /// (value).
        /// </param>
        public MvcHtmlString DescriptionList(bool isHorizontal, IDictionary<string, string> elements)
        {
            if (elements == null) return null;

            var root = new TagBuilderExt("dl");
            if (isHorizontal)
                root.AddCssClass("dl-horizontal");

            foreach (var element in elements)
            {
                root.CreateChildTag("dt", element.Key);
                root.CreateChildTag("dd", element.Value);
            }

            return root.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates an emphasized paragraph
        /// </summary>
        /// <param name="text">Content of the emphasized paragraph</param>
        /// <param name="emphasisType">Type of the emphasis</param>
        public MvcHtmlString EmphasizedParagraph(string text, EmphasisType emphasisType)
        {
            var p = new TagBuilderExt("p");

            switch (emphasisType)
            {
                case EmphasisType.Muted:
                    p.AddCssClass("muted");
                    break;
                case EmphasisType.Warning:
                    p.AddCssClass("text-warning");
                    break;
                case EmphasisType.Error:
                    p.AddCssClass("text-error");
                    break;
                case EmphasisType.Info:
                    p.AddCssClass("text-info");
                    break;
                case EmphasisType.Success:
                    p.AddCssClass("text-success");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("emphasisType");
            }

            p.SetInnerText(text);
            return p.ToMvcHtmlString();
        }

        /// <summary>
        /// Makes a paragraph using the lead class to make it stand out.
        /// </summary>
        /// <param name="text">Content of the paragraph</param>
        public MvcHtmlString LeadBody(string text)
        {
            var p = new TagBuilderExt("p");
            p.AddCssClass("lead");
            p.SetInnerText(text);

            return p.ToMvcHtmlString();
        }

        #endregion

        #region Buttons

        private TagBuilderExt CreateBaseButton(string tagName, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = new TagBuilderExt(tagName);
            tag.AddCssClass("btn");

            switch (buttonStyle)
            {
                case ButtonStyle.Primary:
                    tag.AddCssClass("btn-primary");
                    break;
                case ButtonStyle.Info:
                    tag.AddCssClass("btn-info");
                    break;
                case ButtonStyle.Success:
                    tag.AddCssClass("btn-success");
                    break;
                case ButtonStyle.Warning:
                    tag.AddCssClass("btn-warning");
                    break;
                case ButtonStyle.Danger:
                    tag.AddCssClass("btn-danger");
                    break;
                case ButtonStyle.Inverse:
                    tag.AddCssClass("btn-inverse");
                    break;
                case ButtonStyle.Link:
                    tag.AddCssClass("btn-link");
                    break;
            }

            switch (buttonSize)
            {
                case ButtonSize.Large:
                    tag.AddCssClass("btn-large");
                    break;
                case ButtonSize.Small:
                    tag.AddCssClass("btn-small");
                    break;
                case ButtonSize.Mini:
                    tag.AddCssClass("btn-mini");
                    break;
            }

            if (isDisabled)
                tag.AddCssClass("disabled");

            return tag;
        }

        #region Link buttons

        /// <summary>
        /// Creates an "a" tag styled as a button.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="href">Link destination.</param>
        /// <param name="buttonType">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">
        /// Makes the button looks unclickable by fading them back 50%. True to
        /// disable, False to enable.
        /// </param>
        public MvcHtmlString LinkButton(string text, string href, ButtonStyle buttonType, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("a", buttonType, buttonSize, isDisabled);
            tag.SetInnerText(text);
            tag.MergeAttribute("href", href);

            return tag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates an "a" tag with a default button style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="href">Link destination.</param>
        public MvcHtmlString LinkButton(string text, string href)
        {
            return LinkButton(text, href, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion

        #region Submit buttons
        /// <summary>
        /// Creates a submit button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">
        /// Make buttons look unclickable by fading them back 50%. True to
        /// disable, False to enable.
        /// </param>
        public MvcHtmlString SubmitButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("button", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "submit");
            tag.SetInnerText(text);

            return tag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates a submit button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString SubmitButton(string text)
        {
            return SubmitButton(text, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion

        #region Input buttons
        /// <summary>
        /// Creates an input button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">
        /// Make buttons look unclickable by fading them back 50%. True to
        /// disable, False to enable.
        /// </param>
        public MvcHtmlString InputButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("input", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "button");
            tag.SetInnerText(text);

            return tag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates an input button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString InputButton(string text)
        {
            return InputButton(text, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion

        #region Input Submit buttons
        /// <summary>
        /// Creates an input submit button tag with the given style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        /// <param name="buttonStyle">The button style type.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="isDisabled">
        /// Make buttons look unclickable by fading them back 50%. True to
        /// disable, False to enable.
        /// </param>
        public MvcHtmlString InputSubmitButton(string text, ButtonStyle buttonStyle, ButtonSize buttonSize, bool isDisabled)
        {
            var tag = CreateBaseButton("input", buttonStyle, buttonSize, isDisabled);
            tag.MergeAttribute("type", "submit");
            tag.SetInnerText(text);

            return tag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates an input submit button tag with a default style and size.
        /// </summary>
        /// <param name="text">Inner text of the tag.</param>
        public MvcHtmlString InputSubmitButton(string text)
        {
            return InputSubmitButton(text, ButtonStyle.Default, ButtonSize.Default, false);
        }
        #endregion
        #endregion

        #region Images
        /// <summary>
        /// Define an image
        /// </summary>
        /// <param name="source">the url for the image</param>
        /// <param name="alt">alternate text for the image</param>
        /// <param name="imageType">type of image</param>
        public MvcHtmlString Image(string source, string alt, ImageType imageType)
        {
            var img = new TagBuilderExt("img");
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("src", source);

            switch (imageType)
            {
                case ImageType.Rounded:
                    img.AddCssClass("img-rounded");
                    break;
                case ImageType.Circle:
                    img.AddCssClass("img-circle");
                    break;
                case ImageType.Polaroid:
                    img.AddCssClass("img-polaroid");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("imageType");
            }

            return img.ToMvcHtmlString();
        }
        /// <summary>
        /// Define an Image(source as the alternate text).
        /// </summary>
        /// <param name="source">url and alternate text</param>
        /// <param name="imageType">type of image</param>
        public MvcHtmlString Image(string source, ImageType imageType)
        {
            return Image(source, source, imageType);
        }
        #endregion

        #region Menu
        /// <summary>
        /// Creates a menu (<c>navbar</c>) that can contains menu components.
        /// </summary>
        /// <param name="menuType">The menu type</param>
        /// <param name="isInversed">
        /// Reverse the <c>navbar</c> colors: True to reverse, False (default) for
        /// normal.
        /// </param>
        public BootstrapMvcMenu BeginMenu(MenuType menuType, bool isInversed)
        {
            var menu = new BootstrapMvcMenu(Html.ViewContext);
            menu.BeginMenu(menuType, isInversed);
            return menu;
        }
        /// <summary>
        /// Creates a menu (<c>navbar</c>) that can contains menu components.
        /// </summary>
        /// <param name="menuType">The menu type</param>
        public BootstrapMvcMenu BeginMenu(MenuType menuType)
        {
            return BeginMenu(menuType, false);
        }
        /// <summary>
        /// Creates a basic default menu (<c>navbar</c>) that can contains menu
        /// components.
        /// </summary>
        public BootstrapMvcMenu BeginMenu()
        {
            return BeginMenu(MenuType.Basic, false);
        }



        /// <summary>
        /// Creates a menu <paramref name="title"/> (have to be used inside a
        /// navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="action">The name of the a action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters
        /// are retrieved through reflection by examining the properties of the
        /// object. The object is typically created by using object initializer
        /// syntax.
        /// </param>
        public MvcHtmlString MenuTitle(string title, string action, string controller, object routeValues)
        {
            return MenuTitle(title, Url.Action(action, controller, routeValues));
        }
        /// <summary>
        /// Creates a menu <paramref name="title"/> (have to be used inside a
        /// navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="action">The action name</param>
        /// <param name="controller">The controller name</param>
        public MvcHtmlString MenuTitle(string title, string action, string controller)
        {
            return MenuTitle(title, Url.Action(action, controller));
        }
        /// <summary>
        /// Creates a menu <paramref name="title"/> (have to be used inside a
        /// navbar menu).
        /// </summary>
        /// <param name="title">Title of the menu</param>
        /// <param name="url">A Fully quallified URL</param>
        public MvcHtmlString MenuTitle(string title, string url)
        {
            var a = new TagBuilderExt("a", title);
            a.AddCssClass("brand");
            a.MergeAttribute("href", url);

            return a.ToMvcHtmlString();
        }

        /// <summary>
        /// Begins a menu items container.
        /// </summary>
        public BootstrapMvcMenuItemsContainer BeginMenuItems()
        {
            var menuItems = new BootstrapMvcMenuItemsContainer(Html.ViewContext);
            menuItems.BeginMenuItems();

            return menuItems;
        }

        /// <summary>
        /// Creates one menu link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the menu link</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters
        /// are retrieved through reflection by examining the properties of the
        /// object. The object is typically created by using object initializer
        /// syntax.
        /// </param>
        public MvcHtmlString MenuLink(string title, string action, string controller, object routeValues)
        {
            return MenuLink(title, Url.Action(action, controller, routeValues));
        }
        /// <summary>
        /// Creates one menu link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the menu link</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        public MvcHtmlString MenuLink(string title, string action, string controller)
        {
            return MenuLink(title, Url.Action(action, controller));
        }
        /// <summary>
        /// Creates one menu link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the menu link</param>
        /// <param name="url">The fully quallified URL</param>
        public MvcHtmlString MenuLink(string title, string url)
        {
            var listItem = new TagBuilderExt("li");
            if (IsCurrentUrl(url))
                listItem.AddCssClass("active");
            var link = new TagBuilderExt("a");
            link.MergeAttribute("href", url);
            link.SetInnerText(title);
            listItem.AddChildTag(link);

            return listItem.ToMvcHtmlString();
        }


        /// <summary>
        /// Creates a vertical menu-item separator
        /// </summary>
        public MvcHtmlString MenuItemSeparator()
        {
            var tag = new TagBuilderExt("li");
            tag.AddCssClass("divider-vertical");

            return tag.ToMvcHtmlString();
        }

        /// <summary>
        /// Begins a menu form. Have to be used inside a Bootstrap MVC Menu.
        /// </summary>
        /// <param name="menuFormType">The form type</param>
        /// <param name="horizontalAlignment">
        /// The horizontal alignment applied on the form
        /// </param>
        public BootstrapMenuForm BeginMenuForm(MenuFormType menuFormType, HorizontalAlignment horizontalAlignment)
        {
            var menuForm = new BootstrapMenuForm(Html.ViewContext);
            menuForm.BeginList(menuFormType, horizontalAlignment);
            return menuForm;
        }
        /// <summary>
        /// Begins a default menu form. Have to be used inside a Bootstrap MVC
        /// Menu.
        /// </summary>
        /// <param name="horizontalAlignment">
        /// The horizontal alignment applied on the form
        /// </param>
        public BootstrapMenuForm BeginMenuForm(HorizontalAlignment horizontalAlignment)
        {
            return BeginMenuForm(MenuFormType.Default, horizontalAlignment);
        }
        /// <summary>
        /// Begins a menu form with a default left alignment. Have to be used
        /// inside a Bootstrap MVC Menu.
        /// </summary>
        /// <param name="menuFormType">The form type</param>
        public BootstrapMenuForm BeginMenuForm(MenuFormType menuFormType)
        {
            return BeginMenuForm(menuFormType, HorizontalAlignment.Left);

        }
        /// <summary>
        /// Begins a default menu form with a default left alignment. Have to be
        /// used inside a Bootstrap MVC Menu.
        /// </summary>
        public BootstrapMenuForm BeginMenuForm()
        {
            return BeginMenuForm(MenuFormType.Default, HorizontalAlignment.Left);
        }
        #endregion

        #region Breadcrumbs
        /// <summary>
        /// Begins a breadcrumbs container that can be filled with breadcrumb
        /// links.
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public BootstrapBreadcrumb BeginBreadCrumb()
        {
            var breadCrumb = new BootstrapBreadcrumb(Html.ViewContext);
            breadCrumb.BeginBreadcrumb();
            return breadCrumb;
        }

        /// <summary>
        /// Creates an automatic breadcrumb based on the navigation history.
        /// </summary>
        /// <param name="maximumElements">
        /// The maximum number of breadcrumbs to be displayed.
        /// </param>
        /// <param name="divider">The divider used between breadcrumbs.</param>
        /// <returns>
        /// 
        /// </returns>
        public MvcHtmlString Breadcrumb(int maximumElements, string divider)
        {
            var breadcrumbTag = new TagBuilderExt("ul");
            breadcrumbTag.AddCssClass("breadcrumb");
            var navHistory = Page.NavigationHistory.ToArray();

            var linksToDisplay = navHistory.Skip(Math.Max(0, navHistory.Count() - maximumElements)).Take(maximumElements);
            var visitedPages = linksToDisplay as IList<VisitedPage> ?? linksToDisplay.ToList();
            var lastElement = visitedPages.Last();
            foreach (var visitedPage in visitedPages)
            {
                if (visitedPage == lastElement)
                    divider = null;
                breadcrumbTag.InnerHtml += BreadcrumbLink(visitedPage.Title, visitedPage.Uri.AbsoluteUri, divider);
            }

            return breadcrumbTag.ToMvcHtmlString();
        }

        /// <summary>
        /// Creates one breadcrumb link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the breadcrumb link</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters
        /// are retrieved through reflection by examining the properties of the
        /// object. The object is typically created by using object initializer
        /// syntax.
        /// </param>
        /// <param name="divider">
        /// Specify the divider created after the link. Set to "null" or empty
        /// for no divider.
        /// </param>
        public MvcHtmlString BreadcrumbLink(string title, string action, string controller, object routeValues, string divider)
        {
            return BreadcrumbLink(title, Url.Action(action, controller, routeValues), divider);
        }
        /// <summary>
        /// Creates one breadcrumb link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the breadcrumb link</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="divider">
        /// Specify the divider created after the link. Set to "null" or empty
        /// for no divider.
        /// </param>
        public MvcHtmlString BreadcrumbLink(string title, string action, string controller, string divider)
        {
            return BreadcrumbLink(title, Url.Action(action, controller), divider);
        }
        /// <summary>
        /// Creates one breadcrumb link entry with the specified
        /// <paramref name="title"/> and link.
        /// </summary>
        /// <param name="title">The title of the breadcrumb link</param>
        /// <param name="url">The fully quallified URL</param>
        /// <param name="divider">
        /// Specify the divider created after the link. Set to "null" or empty
        /// for no divider.
        /// </param>
        public MvcHtmlString BreadcrumbLink(string title, string url, string divider)
        {
            var listItem = new TagBuilderExt("li");

            if (IsCurrentUrl(url))
            {
                listItem.AddCssClass("active");
                listItem.SetInnerText(title);
            }
            else
            {
                var link = new TagBuilderExt("a");
                link.MergeAttribute("href", url);
                link.SetInnerText(title);
                listItem.AddChildTag(link);
            }

            if (!string.IsNullOrEmpty(divider))
            {
                var dividerTag = new TagBuilderExt("span", divider);
                dividerTag.AddCssClass("divider");
                listItem.AddChildTag(dividerTag);
            }
            return listItem.ToMvcHtmlString();
        }
        #endregion

        #region Progress bars
        /// <summary>
        /// Creates a Bootstrap progress-bar with a defined 
        /// <paramref name="style"/>, <paramref name="color"/> and 
        /// <paramref name="progress"/>.
        /// </summary>
        /// <param name="style">The style of the progress bar</param>
        /// <param name="color">The color of the progress bar</param>
        /// <param name="progress">The current progress percentage</param>
        public MvcHtmlString ProgressBar(ProgressBarStyle style, ProgressBarColor color, double progress)
        {
            var progressTag = new TagBuilderExt("div");
            progressTag.AddCssClass("progress");

            switch (color)
            {
                case ProgressBarColor.Info:
                    progressTag.AddCssClass("progress-info");
                    break;
                case ProgressBarColor.Success:
                    progressTag.AddCssClass("progress-success");
                    break;
                case ProgressBarColor.Warning:
                    progressTag.AddCssClass("progress-warning");
                    break;
                case ProgressBarColor.Danger:
                    progressTag.AddCssClass("progress-danger");
                    break;
            }

            switch (style)
            {
                case ProgressBarStyle.Animated:
                    progressTag.AddCssClass("active");
                    break;
                    
                case ProgressBarStyle.Striped:
                    progressTag.AddCssClass("progress-striped");
                    break;
            }

            var barTag = progressTag.CreateChildTag("div");
            barTag.AddCssClass("bar");
            barTag.MergeAttribute("style", string.Format("width: {0}%", progress));
            return progressTag.ToMvcHtmlString();
        }
        /// <summary>
        /// Creates a Bootstrap progress-bar with a defined 
        /// <paramref name="style"/>, a default color and 
        /// <paramref name="progress"/>.
        /// </summary>
        /// <param name="style">The style of the progress bar</param>
        /// <param name="progress">The current progress percentage</param>
        public MvcHtmlString ProgressBar(ProgressBarStyle style, double progress)
        {
            return ProgressBar(style, ProgressBarColor.Default, progress);
        }
        /// <summary>
        /// Creates a Bootstrap progress-bar with a default 
        /// style, a specified <paramref name="color"/> and 
        /// <paramref name="progress"/>.
        /// </summary>
        /// <param name="color">The color of the progress bar</param>
        /// <param name="progress">The current progress percentage</param>
        public MvcHtmlString ProgressBar(ProgressBarColor color, double progress)
        {
            return ProgressBar(ProgressBarStyle.Default, color, progress);
        }
        /// <summary>
        /// Creates a Bootstrap progress-bar with a default color and style.
        /// </summary>
        /// <param name="progress">The current progress percentage</param>
        public MvcHtmlString ProgressBar(double progress)
        {
            return ProgressBar(ProgressBarStyle.Default, ProgressBarColor.Default, progress);
        }
        #endregion

        #region Forms

        #endregion

    }

    /// <summary>
    /// Provides methods to generate HTML code for Twitter Bootstrap on a generic view.
    /// </summary>
    /// <typeparam name="TModel">The generic page-view view-model</typeparam>
    public class BootstrapHelper<TModel> : BootstrapHelper
        where TModel : class
    {
        private BootstrapMvcPage _page;
        private HtmlHelper<TModel> _html; 
        /// <summary>
        /// Creates a new instance of the Bootstrap helper.
        /// </summary>
        /// <param name="page">The current view.</param>
        public BootstrapHelper(BootstrapMvcPage<TModel> page) : base(page)
        {
            _page = page;
            _html = page.Html;
        }

    }
}
