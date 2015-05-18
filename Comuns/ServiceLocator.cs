using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Modules;

namespace ArquiteturaBasica
{
    public static class ServiceLocator
    {
        /// <summary>
        /// Kernel do Ninject
        /// </summary>
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get { return ServiceLocator._kernel; }
            set { ServiceLocator._kernel = value; }
        }

        private static bool _initialized = false;

        /// <summary>
        /// Inicializa o ServiceLocator
        /// </summary>
        /// <param name="kernel">Kernel do Ninject</param>
        public static void Initialize()
        {
            if (!_initialized)
            {
                if(_kernel == null)
                    _kernel = new StandardKernel();

                _kernel.Load(AppDomain.CurrentDomain.GetAssemblies());

                _initialized = true;
            }
        }

        public static void Initialize(params string[] assemblies)
        {
            if (!_initialized)
            {
                if (_kernel == null)
                    _kernel = new StandardKernel();

                IList<Assembly> list = new List<Assembly>();

                foreach (var item in assemblies)
                {
                    list.Add(Assembly.Load(item));
                }

                _kernel.Load(list.ToArray());
                _initialized = true;
            }
        }

        /// <summary>
        /// Inicializa o ServiceLocator
        /// </summary>
        /// <param name="kernel">Kernel do Ninject</param>
        internal static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
                _kernel = new StandardKernel(modules);

        }

        /// <summary>
        /// Retorna uma instancia do tipo <typeparamref name="T"/>
        /// </summary>
        /// <remarks>
        /// Se a instancia não existir dentro do kernel, é criada uma 
        /// nova instancia e adicionada no kernel
        /// </remarks>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <returns>Uma instancia de <typeparamref name="T"/></returns>
        public static T Get<T>()
        {

            return _kernel.Get<T>();
        }

        /// <summary>
        /// Retorna uma instancia do tipo <paramref name="type"/>
        /// </summary>
        /// <param name="type">Tipo do parametro</param>
        /// <returns>Uma instancia do tipo especificado no parametro <paramref name="type"/></returns>
        public static object Get(Type type)
        {
            return _kernel.Get(type);
        }

        public static void BindToSelf<T>() where T : class
        {
            _kernel.Bind<T>().ToSelf();
        }
    }

    public static class NinjectModuleScanner
    {
        public static IEnumerable<INinjectModule>
            GetNinjectModules(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetNinjectModules());
        }
    }

    public static class AssemblyExtensions
    {
        public static IEnumerable<INinjectModule>
            GetNinjectModules(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(IsLoadableModule)
                .Select(type => Activator.CreateInstance(type) as INinjectModule);
        }

        private static bool IsLoadableModule(Type type)
        {
            return typeof(INinjectModule).IsAssignableFrom(type)
                && !type.IsAbstract
                && !type.IsInterface
                && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
