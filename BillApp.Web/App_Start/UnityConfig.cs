using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using BillApp.Domain.Interface;
using BillApp.Domain.Repository;

namespace BillApp.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IInvoiceHeader, InvoiceHeaderRepository>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}