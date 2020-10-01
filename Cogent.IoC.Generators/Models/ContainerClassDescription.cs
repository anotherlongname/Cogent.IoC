using System.Linq;
using Cogent.IoC.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators.Models
{
    internal class ContainerClassDescription
    {
        public ContainerClassDescription(ITypeSymbol[] containerTypeInstances)
        {
            FullyQualifiedName = containerTypeInstances.First()?.FullyQualifiedTypeName();
            FullyQualifiedNamespace = containerTypeInstances.First()?.ContainingNamespace.FullyQualifiedNamespace();
            ShortName = containerTypeInstances.First()?.Name;
            AllInterfaces = containerTypeInstances.SelectMany(c => c.AllInterfaces).ToArray();
            AllAttributes = containerTypeInstances.SelectMany(c => c.GetAttributes()).ToArray();
        }

        public INamedTypeSymbol[] AllInterfaces { get; }
        public AttributeData[] AllAttributes { get; }
        public string FullyQualifiedName { get; }
        public string FullyQualifiedNamespace { get; }
        public string ShortName { get; }
    }
}
