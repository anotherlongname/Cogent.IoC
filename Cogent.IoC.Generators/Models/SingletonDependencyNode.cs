namespace Cogent.IoC.Generators.Models
{
    internal class SingletonDependencyNode
    {
        public SingletonDependencyNode(DependencyNode[] dependencies, string instanceName, string typeName)
        {
            Dependencies = dependencies;
            InstanceName = instanceName;
            TypeName = typeName;
        }

        public DependencyNode[] Dependencies { get; }
        public string InstanceName { get; }
        public string TypeName { get; }
    }
}
