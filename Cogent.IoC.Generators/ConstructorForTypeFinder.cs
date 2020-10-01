using System.Linq;
using Cogent.IoC.Generators.Extensions;
using Cogent.IoC.Generators.Models;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators
{
    internal class ConstructorForTypeFinder
    {
        public static INamedTypeSymbol[] GetTypesRequiringConstructorsFromContainerAttributes(RegistrationSymbols registrationSymbols, AttributeData[] attributes)
        {
            var constructorForAttributes = attributes.Where(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, registrationSymbols.ConstructorForAttributeSymbol)).ToArray();
            return constructorForAttributes.Select(attr => attr.GetAttributeConstructorValueByParameterName<INamedTypeSymbol>("constructType")).ToArray();
        }
    }
}
