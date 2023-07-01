﻿namespace Minerva.Localizations
{
    /// <summary>
    /// L10n content directly use given string as base key, for short term translation, try to use <see cref="L10n.Tr(string, string[])"/> instead
    /// </summary>
    [CustomContent(typeof(string))]
    public class KeyL10nContent : L10nContent
    {
        public KeyL10nContent(string value) : base(value)
        {
            baseKey = value;
        }

        /// <summary>
        /// Only returns the escape key because escape value cannot be retrieve from string
        /// </summary>
        /// <param name="escapeKey"></param>
        /// <returns></returns>
        public override string GetEscapeValue(string escapeKey, params string[] param)
        {
            return escapeKey;
        }
    }
}