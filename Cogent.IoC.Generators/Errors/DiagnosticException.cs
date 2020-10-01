using System;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators.Errors
{
    internal class DiagnosticException : Exception
    {
        public DiagnosticException(Diagnostic diagnostic)
        {
            Diagnostic = diagnostic;
        }

        public Diagnostic Diagnostic { get; }
    }
}
