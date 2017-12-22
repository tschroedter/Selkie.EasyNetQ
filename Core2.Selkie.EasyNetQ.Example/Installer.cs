using System;
using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Core2.Selkie.Windsor;

namespace Selkie.EasyNetQ.Examples
{
    [ExcludeFromCodeCoverage]
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Selkie.EasyNetQ",
                                           StringComparison.Ordinal) ||
                   assemblyName.StartsWith("Core2.Selkie.EasyNetQ",
                                           StringComparison.Ordinal);
        }
    }
}