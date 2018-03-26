using Microsoft.AspNet.Identity;
using Projeto.Data;
using Projeto.Data.Interfaces;
using Projeto.Domain;
using Projeto.Domain.Interfaces;
using System;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;
using Unity.Registration;

namespace GameEmprestimo
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> LazyContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            InitializeUnityContext(container);
            return container;
        });

        public static IUnityContainer Container => LazyContainer.Value;


        public static void InitializeUnityContext(IUnityContainer container)
        {

            RegisterBusinessTypes(container);
            RegisterDataTypes(container);
        }


        private static void RegisterDataTypes(IUnityContainer container)
        {
            RegisterWithHierarchicalLifetimeManager<ProjetoContext, ProjetoContext>(container);

            RegisterWithHierarchicalLifetimeManager<IAmigoData, AmigoData>(container);
            RegisterWithHierarchicalLifetimeManager<IConsoleData, ConsoleData>(container);
            RegisterWithHierarchicalLifetimeManager<IEmprestimoData, EmprestimoData>(container);
            RegisterWithHierarchicalLifetimeManager<ITituloData, TituloData>(container);

        }

        private static void RegisterBusinessTypes(IUnityContainer container)
        {
            RegisterWithHierarchicalLifetimeManager<IAmigoBusiness, AmigoBusiness>(container);
            RegisterWithHierarchicalLifetimeManager<IConsoleBusiness, ConsoleBusiness>(container);
            RegisterWithHierarchicalLifetimeManager<IEmprestimoAmigoTituloBusiness, EmprestimoAmigoTituloBusiness>(container);
            RegisterWithHierarchicalLifetimeManager<IEmprestimoBusiness, EmprestimoBusiness>(container);
            RegisterWithHierarchicalLifetimeManager<ITituloBusiness, TituloBusiness>(container);


        }

        public static void RegisterWithHierarchicalLifetimeManager<TFrom, TTo>(IUnityContainer container, params InjectionMember[] injectionMembers)
           where TTo : TFrom
        {
            using (var hlm = new HierarchicalLifetimeManager())
            {
                container.RegisterType<TFrom, TTo>(hlm, injectionMembers);
            }
        }

        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}