using System;
using Ninject;
using NUnit.Framework;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace TAP2018_19.TestBaseClasses {
    internal static class Configuration {
        internal const string ImplementationAssembly =
            @"..\..\..\MyImplementation\bin\Debug\MyImplementation.dll";

        internal const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=TapPrj;Integrated Security=True;";
    }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestFixtureAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestFixture' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
    [TestFixture]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestFixture' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestFixtureAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
    public abstract class AbstractTest {
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClockFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected static readonly IAlarmClockFactory AnAlarmClockFactory;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClockFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected static readonly ISiteFactory AnAuctionSiteFactory;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected static readonly string ImplementationAssembly = Configuration.ImplementationAssembly;

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public static ISiteFactory LoadSiteFactoryFromModule() {
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            var kernel = new StandardKernel();
            ISiteFactory result = null;
            try {
                kernel.Load(Configuration.ImplementationAssembly);
                result = kernel.Get<ISiteFactory>();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            return result;
        }

        static AbstractTest() {
            var kernel = new StandardKernel();

            try {
                AnAuctionSiteFactory = LoadSiteFactoryFromModule();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }

    public abstract class AuctionSiteTest : AbstractTest {
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISiteFactory GetSiteFactory() {
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            return AbstractTest.AnAuctionSiteFactory;
        }

        protected string GetConnectionString() {
            return Configuration.ConnectionString;
        }
    }
}