using DNEmulator.Abstractions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DNEmulator.Dynamic.NETCore
{
    public class NETCoreAssemblyLoader : IAssemblyLoader, IDisposable
    {
        private AssemblyLoadContext _loadContext;
        private DependencyContext _dependencyContext;
        private ICompilationAssemblyResolver _assemblyResolver;


        public Assembly Load(byte[] rawAssembly)
        {
            throw new NotImplementedException();
        }
        public Assembly Load(string assemblyPath)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);

            _dependencyContext = DependencyContext.Load(assembly);
            _assemblyResolver = new CompositeCompilationAssemblyResolver
                               (new ICompilationAssemblyResolver[]
          {
                     new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(assemblyPath)),
                     new ReferenceAssemblyPathResolver(),
                     new PackageCompilationAssemblyResolver()
          });

            _loadContext = AssemblyLoadContext.GetLoadContext(assembly);
            _loadContext.Resolving += OnResolving;
            return assembly;
        }

        private Assembly OnResolving(AssemblyLoadContext context, AssemblyName name)
        {
            var library = _dependencyContext.RuntimeLibraries.FirstOrDefault((library) =>
            {
                return string.Equals(library.Name, name.Name, StringComparison.OrdinalIgnoreCase);
            });

            if (library != null)
            {
                var wrapper = new CompilationLibrary(
                    library.Type,
                    library.Name,
                    library.Version,
                    library.Hash,
                    library.RuntimeAssemblyGroups.SelectMany(group => group.AssetPaths),
                    library.Dependencies,
                    library.Serviceable);

                var assemblies = new List<string>();
                _assemblyResolver.TryResolveAssemblyPaths(wrapper, assemblies);
                if (assemblies.Count > 0)
                    return _loadContext.LoadFromAssemblyPath(assemblies[0]);
            }
            return null;
        }


        public void Dispose()
        {
            _loadContext.Resolving -= OnResolving;
        }
    }
}
