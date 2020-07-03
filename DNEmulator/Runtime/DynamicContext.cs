using DNEmulator.Abstractions;
using DNEmulator.Exceptions;
using DNEmulator.Runtime;
using DNEmulator.Values;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNEmulator
{
    public class DynamicContext
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IDictionary<int, MemberInfo> _members = new Dictionary<int, MemberInfo>();
        public DynamicContext(IAssemblyLoader assemblyLoader = null, bool allowInvocation = true)
        {
            _assemblyLoader = assemblyLoader;
            AllowInvocation = allowInvocation;
            if (_assemblyLoader is null)
                _assemblyLoader = new NETStandardAssemblyLoader();
        }

        public DynamicContext(Assembly assembly)
        {
            Assembly = assembly;
        }

        public DynamicContext(byte[] rawAssembly, IAssemblyLoader assemblyLoader = null, bool allowInvocation = true)
            : this(assemblyLoader, allowInvocation)
        {
            Load(rawAssembly);
        }

        public DynamicContext(string assemblyPath, IAssemblyLoader assemblyLoader = null, bool allowInvocation = true)
            : this(assemblyLoader, allowInvocation)
        {
            Load(assemblyPath);
        }

        public void Load(byte[] rawAssembly)
        {
            try
            {
               Assembly = _assemblyLoader.Load(rawAssembly);
            }
            catch (Exception ex)
            {
                throw new DynamicAssemblyLoadingException(ex);
            }
        }
        public void Load(string assemblyPath)
        {
            try
            {
                Assembly = _assemblyLoader.Load(assemblyPath);
            }
            catch (Exception ex)
            {
                throw new DynamicAssemblyLoadingException(ex);
            }
        }

        public T LookupMember<T>(int metadataToken, Module module = null) where T : MemberInfo
        {
            module ??= Assembly.ManifestModule;
            if (!_members.TryGetValue(metadataToken, out var member))
            {
                member = module.ResolveMember(metadataToken);
                _members.Add(metadataToken, member);
            }
            return (T)member;
        }


        public ObjectValue Invoke(MethodBase methodBase, object[] parameters, object thisObject = null)
        {
            var returnValue = new ObjectValue(methodBase.Invoke(thisObject, parameters));
            return returnValue;
        }

        public Assembly Assembly
        {
            get;
            set;
        }

        public bool AllowInvocation
        {
            get;
            set;
        }
    }
}
