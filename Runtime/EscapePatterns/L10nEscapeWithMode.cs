using System;

namespace Minerva.Localizations.EscapePatterns
{
    /// <summary>Internal bridge that keeps the public localization facade independent of evaluator pooling details.</summary>
    internal static class L10nEscapeWithMode
    {
        /// <summary>Runs the shared tokenizer/evaluator with the requested dynamic-value mode.</summary>
        public static L10nTranslationResult TryEscape(string rawString, ILocalizableContext context, L10nParams parameters, L10nDynamicValueMode dynamicValueMode)
        {
            if (dynamicValueMode == L10nDynamicValueMode.Evaluate)
                return EscapePattern.TryEscape(rawString, context, parameters);
            if (rawString == null)
                return L10nTranslationResult.Empty;

            try
            {
                var tokenizer = L10nObjectPool.RentTokenizer(rawString.AsMemory());
                L10nToken rootToken = null;
                L10nEvaluator evaluator = null;
                try
                {
                    rootToken = tokenizer.Tokenize();
                    evaluator = L10nObjectPool.RentEvaluator(new EvaluationContext(context, parameters, dynamicValueMode));
                    string value = evaluator.Evaluate(rootToken);
                    return new L10nTranslationResult(value, evaluator.GetDiagnostics().Clone());
                }
                finally
                {
                    if (rootToken != null) L10nObjectPool.ReturnToken(rootToken);
                    if (evaluator != null) L10nObjectPool.ReturnEvaluator(evaluator);
                    L10nObjectPool.ReturnTokenizer(tokenizer);
                }
            }
            catch (Exception exception)
            {
                var diagnostics = new L10nEvaluationDiagnostics();
                diagnostics.AddError(L10nErrorSeverity.Fatal, "EscapePattern.TryEscape", "Exception", exception.Message, exception);
                return new L10nTranslationResult(rawString, diagnostics);
            }
        }
    }
}
