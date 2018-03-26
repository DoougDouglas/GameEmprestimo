// Copyright (C) ANAC - Todos os direitos reservados.

using GameEmprestimo.App_Start;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(UnityWebActivator), "Start")]
[assembly: ApplicationShutdownMethod(typeof(UnityWebActivator), "Shutdown")]

namespace GameEmprestimo.App_Start
{
    using System.Web.Http;
    using System.Web.Mvc;
    using Unity.Mvc5;

    public class UnityWebActivator
    {
        public static void Shutdown()
        {
            var container = UnityConfig.Container;
            container.Dispose();
        }

        public static void Start()
        {
            var container = UnityConfig.Container;

            // MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
