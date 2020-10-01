namespace Cogent.IoC.Generators.Models
{
    internal class SingletonVariableDeclaration
    {
        public SingletonVariableDeclaration(string singletonTypeName, string singletonVariableName)
        {
            SingletonTypeName = singletonTypeName;
            SingletonVariableName = singletonVariableName;
        }

        public string SingletonTypeName { get; }
        public string SingletonVariableName { get; }
    }
}
