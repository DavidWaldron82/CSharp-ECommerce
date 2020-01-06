using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
namespace ECommerce.Models {
    public class ViewModel {

        public User OneUser { get; set; }
        List<User> AllUsers {get; set;}
        public Product OneProduct {get;set;}
        public List<Product> AllProducts {get;set;}
        public Review OneReview {get;set;}
        public List<Review> AllReviews {get;set;}

          }
}