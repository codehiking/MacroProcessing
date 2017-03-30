using SfySoft.MacroProcessing.Core;
using System;
using System.Diagnostics;

namespace SfySoft.MacroProcessing.Sandbox
{
    class Foo
    {
        private string Name;

        private int Age;

        public Foo(int age, string s)
        {
            Age = age;

            Name = s;
        }

        [KeywordGetter("foo")]
        public string GetName()
        {
            return Name;
        }

        [KeywordGetter("bar")]
        public string GetAge()
        {
            return Age.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Core.MacroProcessor.Register<Foo>();

            double mSecFrequency = (Stopwatch.Frequency / 1000);

            string result = null;

            Stopwatch sw = Stopwatch.StartNew();

            CompiledTemplate compiledTemplate = MacroProcessor.Compile("Hello, my name is [foo] and I'm [bar] years old");

            Console.WriteLine($"Compilation duration : {sw.ElapsedTicks / mSecFrequency} msecs");

            int iteration = 1000 * 1000;

            sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000 * 1000; i++)
            {
                result = Core.MacroProcessor.Process("I'm [foo], I'm [bar] years old", new Foo(42, "Josh"));
            }

            Console.WriteLine($"Result: {result}");

            Console.WriteLine($"AVG rendering duration : {sw.ElapsedTicks / iteration / mSecFrequency * 1000} usecs over {iteration} iterations");

            System.Console.ReadKey();
        }
    }
}
