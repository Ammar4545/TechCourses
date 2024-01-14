using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechTree.Entities
{
    public class CategoryItem
    {
        //this priavte prop enable to enter 
        private DateTime _releaseDate = DateTime.MinValue;
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaType { get; set; }  
        public int CategoryId { get; set; }//foregin key for Category
        public int MediaTypeId { get; set; }//foregin key for MediaType
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime DateTimeItemRelease
        {
            get
            {
                return (_releaseDate == DateTime.MinValue) ? DateTime.Now : _releaseDate;
            } 
            set 
            {
                _releaseDate = value;
            } 
        }
        [NotMapped]
        public int ContentId { get; set; }




    }
}
