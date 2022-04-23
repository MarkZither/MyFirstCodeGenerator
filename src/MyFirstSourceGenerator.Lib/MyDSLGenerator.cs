using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFirstSourceGenerator.Lib
{
    // based on https://github.com/dotnet/roslyn-sdk/blob/main/samples/CSharp/SourceGenerators/SourceGeneratorSamples/MathsGenerator.cs
    // and more generally https://devblogs.microsoft.com/dotnet/using-c-source-generators-to-create-an-external-dsl/
    // with additional context from https://github.com/terrajobst/minsk
    [Generator]
    public class MyDSLGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            foreach (AdditionalText file in context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".math", StringComparison.OrdinalIgnoreCase))
                {
                    // Load formulas from .math files
                    var mathText = file.GetText();
                    var mathString = "";

                    if (mathText != null)
                    {
                        mathString = mathText.ToString();
                    }
                    else
                    {
                        throw new Exception($"Cannot load file {file.Path}");
                    }

                    // Get name of generated namespace from file name
                    string fileName = Path.GetFileNameWithoutExtension(file.Path);

                    // Parse and gen the formulas functions
                    var tokens = Lexer.Tokenize(mathString);
                    var code = Parser.Parse(tokens);

                    var codeFileName = $@"{fileName}.cs";

                    context.AddSource(codeFileName, SourceText.From(code, Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
