namespace Minerva.Localizations
{
    /// <summary>Controls whether dynamic localization tokens are evaluated or preserved.</summary>
    public enum L10nDynamicValueMode
    {
        /// <summary>Resolve dynamic values through the supplied context and parameters.</summary>
        Evaluate,

        /// <summary>Keep dynamic values in their authored form while evaluating other escapes.</summary>
        Preserve
    }
}
