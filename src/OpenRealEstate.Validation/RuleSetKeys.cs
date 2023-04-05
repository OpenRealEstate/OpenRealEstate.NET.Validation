namespace OpenRealEstate.Validation
{
    public static class RuleSetKeys
    {
        public const string NormalRuleSet = "default," + NormalRuleSetKey;
        public const string StrictRuleSet = NormalRuleSet + "," + StrictRuleSetKey;
        public const string NormalRuleSetKey = "Normal";
        public const string StrictRuleSetKey = "Strict";
    }
}
