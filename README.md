# SfySoft.MacroProcessing
A simple macro-processing POC that relies on [Expression Tress](https://msdn.microsoft.com/en-us/library/mt654263.aspx)
to create a list of Func<> to represent a template made of string literals and macros.

Macro are between square brackets.

```
CompiledTemplate compiledTemplate = MacroProcessor.Compile("Hello, my name is [foo] and I'm [bar] years old");
string result = compiledTemplate.Render(new Foo(42, "Josh"));
```

or simply:

```
stirng result = MacroProcessor.Render("Hello, my name is [foo] and I'm [bar] years old", new Foo(42, "Josh"));
```
