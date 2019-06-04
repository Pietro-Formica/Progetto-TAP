using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.DataEntities
{

    public class AuctionEntity
    {
        public int ID { get; set; }
        public string SellerId { get; set; }
        public  UserEntity Seller { get; set; }
        public string WinnerId { get; set; }
        public UserEntity  CurrentWinner { get; set; }
        public string SiteID { get; set; }
        public virtual SiteEntity Site { get; set; }
        public string Description { get; set; }
        public DateTime EndsOn { get; set; }
        public double MaxOffer { get; set; }
        public double CurrentOffer { get; set; }
        public double StartingPrice { get; set; }

    }
}
