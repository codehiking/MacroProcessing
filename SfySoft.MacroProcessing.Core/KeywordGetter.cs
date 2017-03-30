using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfySoft.MacroProcessing.Core
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
