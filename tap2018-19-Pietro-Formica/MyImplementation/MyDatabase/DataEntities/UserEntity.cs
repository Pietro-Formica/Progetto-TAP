﻿using System.Collections.Generic;

namespace MyImplementation.MyDatabase.DataEntities
{

    public class UserEntity
    {

        public string Id { get; set; }
        public string Password { get; set; }
        public string SiteId { get; set; }
        public virtual SiteEntity Site { get; set; }
        public  virtual SessionEntity Session { get; set; }
        public virtual ICollection<AuctionEntity> SellerAuctionEntities { get; set; }
        public virtual ICollection<AuctionEntity> WinnerAuctionEntities { get; set; }


    }

}
