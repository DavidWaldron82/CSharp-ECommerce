using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace ECommerce.Models {
    public class Product {
        [Key]
        public int ProductId {get;set;}
        public string ProductName {get;set;}
        public string Category {get;set;}
        public int Price {get;set;}
        public int UserId {get;set;}
        public List<Review> CReviews {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}