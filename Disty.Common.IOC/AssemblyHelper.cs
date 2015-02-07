using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using DiagnosticsContract = System.Diagnostics.Contracts.Contract;

namespace Disty.Common.IOC
{
    // Code adapted from WebActivator project
    public static class AssemblyHelper
    {
        private static List<Assembly> _assemblies;

        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    // Cache the list of relevant assemblies, since we need it for both Pre and Post
                    _assemblies = new List<Assembly>();
                    var assemblyFileNames = GetAssemblyFileNames();
                    foreach (var assemblyFileName in assemblyFileNames)
                    {
                        LoadAssemblyIgnoringErrors(assemblyFileName);
                    }
                }

                return _assemblies.Concat(AppCodeAssemblies);
            }
        }

        // Return all the App_Code assemblies
        private static IEnumerable<Assembly> AppCodeAssemblies
        {
            get
            {
                // Return an empty list if we're not hosted or there aren't any
                if (!HostingEnvironment.IsHosted || BuildManager.CodeAssemblies == null)
                {
                    return Enumerable.Empty<Assembly>();
                }

                return BuildManager.CodeAssemblies.OfType<Assembly>();
            }
        }

        public static IEnumerable<Assembly> AssembliesOrdered(string firstStartsWith, string last)
        {
            var assembliesThatAreFirst = new List<Assembly>();
            var assembliesThatAreLast = new List<Assembly>();
            var assembliesThatAreOther = new List<Assembly>();

            var currentAssemblyList = Assemblies;

            foreach (var currentAssembly in currentAssemblyList)
            {
                if (currentAssembly.FullName.Equals(last))
                {
                    assembliesThatAreLast.Add(currentAssembly);
                }
                else if (currentAssembly.FullName.StartsWith(firstStartsWith))
                {
                    assembliesThatAreFirst.Add(currentAssembly);
                }
                else
                {
                    assembliesThatAreOther.Add(currentAssembly);
                }
            }

            return assembliesThatAreFirst.Concat(assembliesThatAreOther).Concat(assembliesThatAreLast);
        }

        [DebuggerNonUserCode]
        private static void LoadAssemblyIgnoringErrors(string assemblyFileName)
        {
            try
            {
                // Ignore assemblies we can't load. They could be native, etc...
                _assemblies.Add(Assembly.LoadFrom(assemblyFileName));
                Trace.WriteLine("Loaded assembly " + assemblyFileName);
            }
            catch (Win32Exception)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (FileNotFoundException)
            {
            }
            catch (PathTooLongException)
            {
            }
            catch (BadImageFormatException)
            {
            }
            catch (SecurityException)
            {
            }
        }

        private static IEnumerable<string> GetAssemblyFileNames()
        {
            // When running under ASP.NET, find assemblies in the bin folder.
            // Outside of ASP.NET, use whatever folder WebActivator itself is in
            var directory = HostingEnvironment.IsHosted
                ? HttpRuntime.BinDirectory
                : Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            DiagnosticsContract.Assert(directory != null);

            return Directory.GetFiles(directory, "*.dll").Concat(Directory.GetFiles(directory, "*.exe"));
        }
    }
}