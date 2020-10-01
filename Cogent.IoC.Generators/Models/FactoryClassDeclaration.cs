namespace Cogent.IoC.Generators.Models
{
    internal class FactoryClassDeclaration
    {
        public FactoryClassDeclaration(string factoryImplimentationClassName, string factoryTypeName, ServiceConstructor[] serviceConstructorMethods)
        {
            FactoryImplimentationClassName = factoryImplimentationClassName;
            FactoryTypeName = factoryTypeName;
            ServiceConstructorMethods = serviceConstructorMethods;
        }

        public string FactoryImplimentationClassName { get; }
        public string FactoryTypeName { get; }
        public ServiceConstructor[] ServiceConstructorMethods { get; }
    }
}
