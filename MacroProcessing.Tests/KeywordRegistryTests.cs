using System;
using Xunit;

namespace MacroProcessing.Tests
{
    public class Foo
    {
        [KeywordGetter("foobar")]
        public string Bar() => "foobar";
    }

    public class KeywordRegistryTests
    {
        [Fact]
        public void WithFooClass_CheckThatEmbodiedExpressionAreDetected()
        {
            KeywordRegistry registry = new KeywordRegistry();

            registry.Register<Foo>();

            Assert.NotEmpty(registry.RegisteredKeywords);
        }
    }
}
