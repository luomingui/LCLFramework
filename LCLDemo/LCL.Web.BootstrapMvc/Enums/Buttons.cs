namespace LCL.Web.BootstrapMvc.Enums
{
    /// <summary>
    /// Defines the different button types.
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// An HTML Input type button.
        /// </summary>
        Input,
        /// <summary>
        /// And HTML Button type button.
        /// </summary>
        Button
    }

    /// <summary>
    /// Defines the different button styles.
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// Standard gray button with gradient.
        /// </summary>
        Default,
        /// <summary>
        /// Provides extra visual weight and identifies the primary action in a set of buttons.
        /// </summary>
        Primary,
        /// <summary>
        /// Used as an alternative to the default styles.
        /// </summary>
        Info,
        /// <summary>
        /// Indicates a successful or positive action.
        /// </summary>
        Success,
        /// <summary>
        /// Indicates caution should be taken with this action.
        /// </summary>
        Warning,
        /// <summary>
        /// Indicates a dangerous or potentially negative action.
        /// </summary>
        Danger,
        /// <summary>
        /// Alternate dark gray button, not tied to a semantic action or use.
        /// </summary>
        Inverse,
        /// <summary>
        /// Deemphasize a button by making it look like a link while maintaining button behavior.
        /// </summary>
        Link
    }

    /// <summary>
    /// Defines the possible button sizes.
    /// </summary>
    public enum ButtonSize
    {
        /// <summary>
        /// Large button
        /// </summary>
        Large,
        /// <summary>
        /// Default button
        /// </summary>
        Default,
        /// <summary>
        /// Small button
        /// </summary>
        Small,
        /// <summary>
        /// Mini button
        /// </summary>
        Mini
    }
}
