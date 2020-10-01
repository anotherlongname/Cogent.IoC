using System;

namespace Cogent.IoC.Errors
{
    public class ContainerClassImproperlyGeneratedException : Exception
    {
        internal ContainerClassImproperlyGeneratedException()
            : base("Container class has not been properly inherited or generated") { }
    }
}
