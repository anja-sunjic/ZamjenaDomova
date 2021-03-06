﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZamjenaDomova.Model
{
    public class WishlistListing
    {
        public int WishlistListingId { get; set; }
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public int ListingId { get; set; }
        public virtual Listing Listing { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
