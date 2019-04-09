using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.Implements
{
    class ManagerUser
    {
        private readonly string _connectionString = ManagerSetup.ConnectionString;
        private readonly ManagerEntitySite _managerEntitySite = new ManagerEntitySite();

        private SiteEntity GetSite(string name)
        {
           var site = _managerEntitySite.FindByKey(name);
           if(site is null)
               throw new InexistentNameException(name);

           return site;
        }
    }
}
