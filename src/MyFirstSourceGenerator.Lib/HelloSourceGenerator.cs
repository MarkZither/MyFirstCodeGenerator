﻿using Microsoft.CodeAnalysis;

using System;

namespace MyFirstSourceGenerator.Lib.SourceGenerator
{
    [Generator]
    public class HelloSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
        // Find the main method
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        // Build up the source code
        string source = $@" // Auto-generated code
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public static partial class {mainMethod.ContainingType.Name}
    {{
        static partial void HelloFrom(string name){{
            System.Console.WriteLine($""Generator says: Hi from '{{name}}'"");
            }}
    }}
}}
";
        var typeName = mainMethod.ContainingType.Name;

        // Add the source code to the compilation
        context.AddSource($"{typeName}s.g.cs", source);
    }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}
