using Minerva.Module;

namespace Amlos.Localizations
{
    /// <summary>
    /// Common interface use to get the localization information from an object
    /// </summary>
    public interface ILocalizable
    {
        /// <summary>
        /// Get the key represent for this localizable object
        /// <br></br>
        /// Override to get keys from the object
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetLocalizationKey(params string[] param)
        {
            var key = GetType().FullName;
            return Localizable.AppendKey(key, param);
        }

        /// <summary>
        /// Get the raw content, override this for creating custom format of localized content
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetRawContent(params string[] param)
        {
            var key = GetLocalizationKey(param);
            var rawString = Localization.GetContent(key, param);
            return rawString;
        }

        /// <summary>
        /// Get escape value from the object
        /// </summary>
        /// <param name="escapeKey"></param>
        /// <returns></returns>
        string GetEscapeValue(string escapeKey, params string[] param)
        {
            var value = Reflection.GetLastObject(this, escapeKey);
            if (value == null) return escapeKey;
            return value.ToString();
        }
    }
}