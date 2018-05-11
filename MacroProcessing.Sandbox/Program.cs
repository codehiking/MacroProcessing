using MacroProcessing;
using System;
using System.Diagnostics;

namespace MacroProcessing.Sandbox
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

        [KeywordGetter("name")]
        public string GetName()
        {
            return Name;
        }

        [KeywordGetter("age")]
        public string GetAge()
        {
            return Age.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MacroProcessor.Register<Foo>();

            double mSecFrequency = (Stopwatch.Frequency / 1000);

            string result = null;

            Stopwatch sw = Stopwatch.StartNew();

            CompiledTemplate compiledTemplate = MacroProcessor.Compile("Hello, my name is [name] and I'm [age] years old");

            Console.WriteLine($"Compilation duration : {sw.ElapsedTicks / mSecFrequency} msecs");

            int iteration = 1000 * 1000;

            sw = Stopwatch.StartNew();

            for (int i = 0; i < 1000 * 1000; i++)
            {
                result = MacroProcessor.Process("I'm [name], I'm [age] years old", new Foo(42, "Josh"));
            }

            Console.WriteLine($"Result: {result}");

            Console.WriteLine($"AVG rendering duration : {sw.ElapsedTicks / iteration / mSecFrequency * 1000} usecs over {iteration} iterations");

            System.Console.ReadKey();
        }
    }
}
