namespace LCL.Web.BootstrapMvc.Enums
{
    /// <summary>
    /// Defines the different visual styles for a Bootstrap progress bar.
    /// </summary>
    public enum ProgressBarStyle
    {
        /// <summary>
        /// Represents a default progress bar with a vertical gradient.
        /// </summary>
        Default,
        /// <summary>
        /// Uses a gradient to create a striped effect. Not available in IE7 and IE8.
        /// </summary>
        Striped,
        /// <summary>
        /// Represents a Striped bar with animated stripes from right to left. Not available on IE9 and lower.
        /// </summary>
        Animated
    }

    /// <summary>
    /// Defines the different colors available for a progress bar.
    /// </summary>
    public enum ProgressBarColor
    {
        /// <summary>
        /// Uses the default deep blue color.
        /// </summary>
        Default,
        /// <summary>
        /// Uses a lighter blue color for an informational case.
        /// </summary>
        Info,
        /// <summary>
        /// Uses a light green color for a successful case.
        /// </summary>
        Success,
        /// <summary>
        /// Uses a light orange color for a warning case.
        /// </summary>
        Warning,
        /// <summary>
        /// Uses a red color for a danger & error case.
        /// </summary>
        Danger
    }

}
