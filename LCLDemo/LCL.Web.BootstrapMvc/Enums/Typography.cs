namespace LCL.Web.BootstrapMvc.Enums
{
    /// <summary>
    /// Bootstrap supported emphasis types.
    /// </summary>
    public enum EmphasisType
    {

        /// <summary>
        /// Muted emphasis paragraph type.
        /// </summary>
        Muted,

        /// <summary>
        /// Warning emphasis paragraph type.
        /// </summary>
        Warning,

        /// <summary>
        /// Error emphasis paragraph type.
        /// </summary>
        Error,

        /// <summary>
        /// Information emphasis paragraph type.
        /// </summary>
        Info,

        /// <summary>
        /// Success emphasis paragraph type.
        /// </summary>
        Success
    }

    /// <summary>
    /// Bootstrap supported list types
    /// </summary>
    public enum ListType
    {
        /// <summary>
        /// A list of items in which the order does not explicitly matter.
        /// </summary>
        Unordered,

        /// <summary>
        /// A list of items in which the order does epxlicitly matter.
        /// </summary>
        Ordered,

        /// <summary>
        /// Unordored list without default style and left padding on list items (immediate children only).
        /// </summary>
        Unstyled,

        /// <summary>
        /// Unordored list with all items on a single line with inline-block and some light padding.
        /// </summary>
        Inline
    }

}
