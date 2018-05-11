using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MacroProcessing
{
    public static class MacroProcessor
    {
        private static readonly Func<object, string> _ReturnStringEmptyFunc = (o) => string.Empty;

        private static KeywordRegistry Registry = new KeywordRegistry();

        private static IDictionary<int, CompiledTemplate> _compiledTemplates = 
            new Dictionary<int, CompiledTemplate>();

        public static void Register<T>()
        {
            Registry.Register<T>();
        }

        public static CompiledTemplate Compile(string template)
        {
            IList<Func<object, string>> compilationResult = new List<Func<object, string>>();

            string literal = string.Empty;

            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] == '[')
                {
                    string keyword = string.Empty;

                    for (int j = i + 1; j < template.Length; j++)
                    {
                        i++;

                        if (template[j] == ']')
                        {
                            if (literal.Length > 0)
                            {
                                string capture = literal;

                                compilationResult.Add((o) => capture);

                                literal = string.Empty;
                            }

                            compilationResult.Add(Registry.GetDelegate(keyword) ?? _ReturnStringEmptyFunc);

                            break;
                        }
                        else
                        {
                            keyword += template[j];
                        }
                    }
                }
                else
                {
                    literal += template[i];
                }

                if(literal.Length > 0)
                {
                    string capture = literal;

                    compilationResult.Add((o) => capture);

                    literal = string.Empty;
                }
            }

            return new CompiledTemplate(new ReadOnlyCollection<Func<object, string>>(compilationResult));
        }

        public static string Process(string templateWithMacro, object data)
        {
            CompiledTemplate compiledTemplate = null;

            if (_compiledTemplates.Keys.Contains(templateWithMacro.GetHashCode()))
            {
                compiledTemplate = _compiledTemplates[templateWithMacro.GetHashCode()];
            }
            else
            {
                compiledTemplate = Compile(templateWithMacro);

                _compiledTemplates.Add(templateWithMacro.GetHashCode(), compiledTemplate);
            }

            return compiledTemplate.Render(data);
        }
    }
}
