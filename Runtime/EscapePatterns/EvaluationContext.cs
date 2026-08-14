using System.Collections.Generic;

namespace Minerva.Localizations.EscapePatterns
{
    internal sealed class EvaluationContext
    {
        public int Depth { get; }
        public ILocalizableContext Context { get; }
        public IReadOnlyDictionary<string, object> Variables { get; }
        public L10nDynamicValueMode DynamicValueMode { get; }

        public EvaluationContext(int depth, ILocalizableContext context, IReadOnlyDictionary<string, object> variables)
            : this(depth, context, variables, L10nDynamicValueMode.Evaluate)
        {
        }

        public EvaluationContext(int depth, ILocalizableContext context, IReadOnlyDictionary<string, object> variables, L10nDynamicValueMode dynamicValueMode)
        {
            Depth = depth;
            Context = context;
            Variables = variables ?? new Dictionary<string, object>();
            DynamicValueMode = dynamicValueMode;
        }

        public EvaluationContext(ILocalizableContext context, L10nParams parameters)
            : this(context, parameters, L10nDynamicValueMode.Evaluate)
        {
        }

        public EvaluationContext(ILocalizableContext context, L10nParams parameters, L10nDynamicValueMode dynamicValueMode)
        {
            Depth = parameters.Depth;
            Context = context;
            Variables = parameters.Variables;
            DynamicValueMode = dynamicValueMode;
        }

        public bool CanRecurse() => Depth < L10n.MAX_RECURSION;

        public EvaluationContext IncreaseDepth()
        {
            return new EvaluationContext(Depth + 1, Context, Variables, DynamicValueMode);
        }
    }
}
