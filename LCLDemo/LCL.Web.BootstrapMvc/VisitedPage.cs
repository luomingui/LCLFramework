using System;

namespace LCL.Web.BootstrapMvc
{
    /// <summary>
    /// Represents a visited page. Used for navigation history purposes.
    /// </summary>
    public class VisitedPage : IEquatable<VisitedPage>, IEquatable<string>
    {


        /// <summary>
        /// Title of the page (from <c>Viewbag.Title</c>).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Page Uri
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Compares the current <see cref="VisitedPage"/> to another one by
        /// checking the URL Path and Uri
        /// </summary>
        /// <param name="other">A VisitedPage</param>
        /// <returns>Returns true if the current <see cref="VisitedPage"/> have
        /// the same Uri or Path than the <see cref="VisitedPage"/> in parameter
        /// </returns>
        public bool Equals(VisitedPage other)
        {
            return Uri.AbsolutePath.Equals(other.Uri.AbsolutePath) || Uri.AbsoluteUri.Equals(other.Uri.AbsoluteUri);
        }

        /// <summary>
        /// Compares the current <see cref="VisitedPage"/> to an Uri or Path.
        /// </summary>
        /// <param name="uri">An Uri or Path</param>
        /// <returns>Returns true if the current <see cref="VisitedPage"/> have
        /// the same Uri or Path than the parameter</returns>
        public bool Equals(string uri)
        {
            return Uri.AbsolutePath.Equals(uri) || Uri.AbsoluteUri.Equals(uri);
        }
    }
}
