using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace SourceGeneratorLibrary
{
    [Generator]
    public class HelloSourceGenerator: ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }

            SpinWait.SpinUntil(() => Debugger.IsAttached);
#endif 
            Debug.WriteLine("No initialization is required here");
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var mainMethodNameSpace = mainMethod?.ContainingNamespace.ToDisplayString();
            var typeName = mainMethod?.ContainingType.Name;
            
            Debug.WriteLine("Begin: Preparing Source");
            var source = $@"// Auto-generated code
                            using System;
                            namespace {mainMethodNameSpace};
                            
                            public static partial class {typeName}
                            {{
                                static partial void HelloFrom(string name) => 
                                    Console.WriteLine($""Generator says: Hi from '{{name}}'"");
                            }}";
            Debug.WriteLine($"Source: {source}");

            Debug.WriteLine("Finish: Preparing Source");

            context.AddSource($"{typeName}.g.cs", source);
            Debug.WriteLine("Source successfully added.");
        }
    }
}