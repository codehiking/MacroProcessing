using System;

namespace MacroProcessing
{
    /// <summary>
    /// Attribute to define a method as the getter for a keyword.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class KeywordGetter : Attribute
    {
        public KeywordGetter(string keyword) { this.keyword = keyword; }
        public string keyword;
    }
}
