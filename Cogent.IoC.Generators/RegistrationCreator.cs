﻿using System.Linq;
using Cogent.IoC.Generators.Models;
using Microsoft.CodeAnalysis;

namespace Cogent.IoC.Generators
{
    internal static class RegistrationCreator
    {
        public static Registration[] GetRegistrationsFromInterfaces(RegistrationSymbols registrationSymbols, INamedTypeSymbol[] interfaces)
            => interfaces.Select(i =>
                    registrationSymbols.TryCreateTransientRegistration(i, out var reg1) ? (true, reg1) :
                    registrationSymbols.TryCreateSingletonRegistration(i, out var reg2) ? (true, reg2) :
                    registrationSymbols.TryCreateDelegateRegistration(i, out var reg3) ? (true, reg3) :
                    registrationSymbols.TryCreateFactoryRegistration(i, out var reg4) ? (true, reg4) :
                    (false, default))
                .Where(i => i.Item1)
                .Select(i => i.Item2)
                .ToArray();

        private static bool TryCreateTransientRegistration(this RegistrationSymbols registrationSymbols, INamedTypeSymbol typeSymbol, out Registration registration)
        {
            if (registrationSymbols.TransientSymbols.Any(s => SymbolEqualityComparer.Default.Equals(s, typeSymbol.OriginalDefinition)))
            {
                registration = CreateTransientRegistration((INamedTypeSymbol)typeSymbol.TypeArguments.First(), (INamedTypeSymbol)typeSymbol.TypeArguments.Last());
                return true;
            }

            registration = default;
            return false;
        }

        private static bool TryCreateSingletonRegistration(this RegistrationSymbols registrationSymbols, INamedTypeSymbol typeSymbol, out Registration registration)
        {
            if (registrationSymbols.SingletonSymbols.Any(s => SymbolEqualityComparer.Default.Equals(s, typeSymbol.OriginalDefinition)))
            {
                registration = CreateSingletonRegistration((INamedTypeSymbol)typeSymbol.TypeArguments.First(), (INamedTypeSymbol)typeSymbol.TypeArguments.Last());
                return true;
            }

            registration = default;
            return false;
        }

        private static bool TryCreateDelegateRegistration(this RegistrationSymbols registrationSymbols, INamedTypeSymbol typeSymbol, out Registration registration)
        {
            var delegateMatch = registrationSymbols.DelegateSymbols.FirstOrDefault(s => SymbolEqualityComparer.Default.Equals(s, typeSymbol.OriginalDefinition));
            if (delegateMatch != null)
            {
                registration = CreateDelegateRegistration(delegateMatch, (INamedTypeSymbol)typeSymbol.TypeArguments.First(), typeSymbol.TypeArguments.Skip(1).OfType<INamedTypeSymbol>().ToArray());
                return true;
            }

            registration = default;
            return false;
        }

        private static bool TryCreateFactoryRegistration(this RegistrationSymbols registrationSymbols, INamedTypeSymbol typeSymbol, out Registration registration)
        {
            if (SymbolEqualityComparer.Default.Equals(registrationSymbols.FactorySymbol, typeSymbol.OriginalDefinition))
            {
                registration = CreateFactoryRegistration((INamedTypeSymbol)typeSymbol.TypeArguments.First());
                return true;
            }

            registration = default;
            return false;
        }


        private static Registration CreateTransientRegistration(INamedTypeSymbol serviceType, INamedTypeSymbol implementationType)
            => new Registration(
                new TransientRegistration(
                    serviceType,
                    implementationType,
                    implementationType
                        .InstanceConstructors
                        .OrderByDescending(ctor => ctor.Parameters.Length)
                        .Select(ctor => ctor
                            .Parameters
                            .Select(p => p.Type)
                            .ToArray())
                        .ToArray()));

        private static Registration CreateSingletonRegistration(INamedTypeSymbol serviceType, INamedTypeSymbol implementationType)
            => new Registration(
                new SingletonRegistration(
                    serviceType,
                    implementationType,
                    implementationType
                        .InstanceConstructors
                        .OrderByDescending(ctor => ctor.Parameters.Length)
                        .Select(ctor => ctor
                            .Parameters
                            .Select(p => p.Type)
                            .ToArray())
                        .ToArray()));

        private static Registration CreateDelegateRegistration(INamedTypeSymbol delegateType, INamedTypeSymbol serviceType, INamedTypeSymbol[] dependencyTypes)
            => new Registration(new DelegateRegistration(delegateType, serviceType, dependencyTypes));

        private static Registration CreateFactoryRegistration(INamedTypeSymbol factoryType)
            => new Registration(new FactoryRegistration(factoryType));
    }
}
