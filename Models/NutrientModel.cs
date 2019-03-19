using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroDB.Models
{
    public class NutrientModel
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)] 
        public int id {get; set;}

        public string name {get; set;}
        
        public float protein {get; set;}
        
        public float carbohydrate {get; set;}
        
        public float lipid {get; set;}
        
        public float calorie {get; set;}
        
        public bool approval {get; set;}
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_at{get; set;}
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime updated_at{get; set;}
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime approvaled_at{get; set;}
    }
}