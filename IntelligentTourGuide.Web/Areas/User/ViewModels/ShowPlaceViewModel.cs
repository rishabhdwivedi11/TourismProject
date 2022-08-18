using IntelligentTourGuide.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Areas.User.ViewModels
{
    public class ShowPlaceViewModel
    {
        [Required(ErrorMessage = "{0} cannot be empty")]
        [Display(Name = "State or UT")]
        public int StateId { get; set; }

        public string ImageUrl
        {
            get; set;
        }


        public ICollection<Place> Places { get; set; }
    }
}
