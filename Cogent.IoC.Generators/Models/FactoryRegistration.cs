using System.Linq;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators.Models
{
    internal class FactoryRegistration
    {
        public FactoryRegistration(ITypeSymbol service)
        {
            Service = service;
            ImplimentationName = $"{service.Name}Impl";
            ServiceMethods = service.GetMembers().OfType<IMethodSymbol>().ToArray();
        }

        public ITypeSymbol Service { get; }
        public string ImplimentationName { get; }
        public IMethodSymbol[] ServiceMethods { get; }
    }
}
