using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace ECommerce.Models {
    public class Product {
        [Key]
        public int ProductId {get;set;}
        [Required]
        public string ProductName {get;set;}
        [Required]

        public string Category {get;set;}
        [Required]

        public int Price {get;set;}
        [Required]

        public string Description {get;set;}
        public int UserId {get;set;}
        public List<Review> CReviews {get;set;}
        [Required]

        public string Image {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}