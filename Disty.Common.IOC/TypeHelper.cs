using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Disty.Common.IOC
{
    public static class TypeHelper
    {
        public static IEnumerable<Type> FindTypesThatExtend<TBaseType>()
        {
            var assemblies = AssemblyHelper.Assemblies;
            return FindTypesThatExtend<TBaseType>(assemblies);
        }

        public static IEnumerable<Type> FindTypesThatExtend<TBaseType>(IEnumerable<Assembly> assemblies)
        {
            return from assembly in assemblies
                from type in GetTypesIgnoringError(assembly)
                where typeof (TBaseType).IsAssignableFrom(type)
                where type != typeof (TBaseType)
                where type.IsClass
                where !type.IsAbstract
                select type;
        }

        [DebuggerNonUserCode]
        private static IEnumerable<Type> GetTypesIgnoringError(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                Trace.WriteLine(string.Format(
                    "Exception when retrieving types from assembly '{0}' with exception '{1}'", assembly,
                    reflectionTypeLoadException));
                foreach (var exception in reflectionTypeLoadException.LoaderExceptions)
                {
                    Trace.WriteLine(string.Format("Loader Exception '{0}'", exception));
                }
                return Enumerable.Empty<Type>();
            }
        }

        public static object GetDefault(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        public static T GetDefault<T>()
        {
            var t = typeof (T);
            return (T) GetDefault(t);
        }

        public static bool IsDefault<T>(T other)
        {
            var defaultValue = GetDefault<T>();
            if (other == null) return defaultValue == null;
            return other.Equals(defaultValue);
        }

        public static bool HidesProperty<T>(PropertyInfo property)
        {
            var getMethod = property.GetGetMethod();
            if ((getMethod.Attributes & MethodAttributes.Virtual) != 0 &&
                (getMethod.Attributes & MethodAttributes.NewSlot) == 0)
            {
                // the property's 'get' method is an override
                return false;
            }
            if (getMethod.IsHideBySig)
            {
                var flags = getMethod.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic;
                flags |= getMethod.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
                var paramTypes = getMethod.GetParameters().Select(p => p.ParameterType).ToArray();
                if (getMethod.DeclaringType.BaseType.GetMethod(getMethod.Name, flags, null, paramTypes, null) != null)
                {
                    // the property's 'get' method shadows by signature which is ok since all datatypes will match
                    return false;
                }
            }
            else
            {
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
                if (getMethod.DeclaringType.BaseType.GetMethods(flags).Any(m => m.Name == getMethod.Name))
                {
                    // the property's 'get' method shadows by name which is not ok since datatypes may not match
                    return true;
                }
            }

            return false;
        }
    }
}