﻿using Cogent.IoC.Generators.Models;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators
{
    internal class AspNetCoreContainerClassContentGenerator
    {
        private readonly GeneratorExecutionContext _context;
        private readonly RegistrationSymbols _registrationSymbols;

        public AspNetCoreContainerClassContentGenerator(GeneratorExecutionContext context, RegistrationSymbols registrationSymbols)
        {
            _context = context;
            _registrationSymbols = registrationSymbols;
        }

        public string GenerateClassString(ContainerClassDescription containerClassDescription)
        {
            var content = new AspNetCoreContainerClassContent(containerClassDescription.FullyQualifiedNamespace, containerClassDescription.ShortName);
            return content.AsString();
        }

    }
}
