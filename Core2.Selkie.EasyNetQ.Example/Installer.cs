using System;
using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;

namespace Core2.Selkie.EasyNetQ.Example
{
    [ExcludeFromCodeCoverage]
    public class Installer
        : EasyNetQBasicConsoleInstaller,
          IWindsorInstaller
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.EasyNetQ",
                                           StringComparison.Ordinal);
        }
    }
}