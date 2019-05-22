using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace restapii.Models
{
    public class tblAsset
    {
        [Key]
        public int assetId { get; set; }
        public string assetLabel { get; set; }
        public string assetDescription { get; set; }
        public string assetType { get; set; }
        public string assetStatus { get; set; }
        public DateTime? purchaseDate { get; set; }
        public string currentOwner { get; set; }
        public bool? deleted { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? lastUpdated { get; set; }
    }
}