using MyImplementation.ConcreteClasses;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Implements;
using Ninject.Modules;
using TAP2018_19.AuctionSite.Interfaces;


namespace MyImplementation.Bindings
{
   public class InjectonModule : NinjectModule
    {
        public override void Load()
        {
           Bind<ISiteFactory>().To<SiteFactory>();
        }
    }
}
