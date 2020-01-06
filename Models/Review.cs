using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ECommerce.Models {
    public class Review {
        [Key]
        public int ReviewId {get;set;}
        public string Name {get;set;}
        public string Category {get;set;}
        public int Price {get;set;}
        public int ProductId {get;set;}
        public int UserId {get;set;}
        public User Reviewer {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}