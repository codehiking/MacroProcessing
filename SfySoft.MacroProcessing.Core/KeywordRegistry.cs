using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SfySoft.MacroProcessing.Core
{
    public class KeywordRegistry
    {
        IDictionary<string, Func<object, string>> _getters = new Dictionary<string, Func<object, string>>();

        public IReadOnlyList<string> RegisteredKeywords => _getters.Keys.ToList();

        /// <summary>
        /// To call in your implementation of RegisterGetters, to create the bridge between a keyword and its getter
        /// </summary>
        private void RegisterGetter(string keyword, Func<object, string> getter)
        {
            _getters[keyword.ToLowerInvariant()] = getter;
        }

        public void Register<T>()
        {
            Type type = typeof(T);

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(KeywordGetter), true);

                foreach (KeywordGetter attribute in attributes)
                {
                    if (!method.ReturnType.Equals(typeof(string)))
                        continue;

                    ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "target");

                    Func<object, string> result = Expression.Lambda<Func<object, string>>(
                        Expression.Call(Expression.Convert(parameterExpression, type), method), parameterExpression).Compile();

                    _getters.Add(attribute.keyword, result);
                }
            }
        }

        public Func<object, string> GetDelegate(string keyword)
        {
            if (!_getters.Keys.Contains(keyword))
                return null;

            return _getters[keyword];
        }
    }
}
