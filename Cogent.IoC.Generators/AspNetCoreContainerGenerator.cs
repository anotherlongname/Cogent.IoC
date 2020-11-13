using System;
using System.Text;
using Cogent.IoC.Generators.Errors;
using Cogent.IoC.Generators.Extensions;
using Cogent.IoC.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Cogent.IoC.Generators
{
    [Generator]
    internal class AspNetCoreContainerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) 
        {
            //System.Diagnostics.Debugger.Launch();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var registrationSymbols = RegistrationSymbols.FromCompilation(context.Compilation);
                var containerClasses = context.LocateContainerSymbols(registrationSymbols.AspNetCoreContainerSymbol);
                var generator = new AspNetCoreContainerClassContentGenerator(context, registrationSymbols);

                foreach(var containerClass in containerClasses)
                {
                    var hintName = $"Generated.AspNetCore.{containerClass.FullyQualifiedName}";
                    var content = generator.GenerateClassString(containerClass);

                    WriteOutDebugFile(hintName, content, context);
                    context.AddSource(hintName, SourceText.From(content, Encoding.UTF8));
                }
            }
            catch (DiagnosticException ex)
            {
                context.ReportDiagnostic(ex.Diagnostic);
            }
            catch (Exception ex)
            {
                var descriptor = new DiagnosticDescriptor(DiagnosticConstants.UnknownExceptionId, "Unexpected error", $"Unknown error during generation: {ex.GetType()} {ex.Message}", DiagnosticConstants.Category, DiagnosticSeverity.Error, true);
                context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None));
            }
        }

        // TODO : Remove when possible to peek into generated code
        private void WriteOutDebugFile(string hintName, string content, GeneratorExecutionContext context)
        {
#if DEBUG
            try
            {
                var tempFile = $"{System.IO.Path.GetTempPath()}{hintName}.cs";
                System.IO.File.WriteAllText(tempFile, content);
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("GDBG", "Write out debug file", tempFile, DiagnosticConstants.Category, DiagnosticSeverity.Warning, true), Location.None));
            }
            catch { throw; }
#endif
        }
    }
}
