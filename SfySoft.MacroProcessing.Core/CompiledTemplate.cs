using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfySoft.MacroProcessing.Core
{
    public class CompiledTemplate
    {
        IReadOnlyList<Func<object, string>> _compiledTemplate = null;

        public CompiledTemplate(IReadOnlyList<Func<object, string>> compiledTemplate)
        {
            _compiledTemplate = compiledTemplate;
        }

        public string Render(object data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Func<object, string> func in _compiledTemplate)
            {
                sb.Append(func(data));
            }

            return sb.ToString();
        }
    }
}
