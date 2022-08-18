using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Models
{
    [Table("Place")]
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Place ID")]
        public int PlaceId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} cannot be empty")]
        [MinLength(2, ErrorMessage = "{0} cannot have lesser than {1} characters")]
        [Display(Name = "Place Name")]
        public string PlaceName { get; set; }

        [StringLength(250, ErrorMessage = "{0} cannot be empty")]
        public string Description { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Is enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Required]
        public string ImageUrl { get; set; } = null;

        #region Navigation Properties for State Model

        [Required]
        [Display(Name = "State")]
        public short StateId { get; set; }


        [ForeignKey(nameof(Place.StateId))]
        public State State { get; set; }
        #endregion

        #region Navigation Properties for PlaceDetail Model

        public ICollection<PlaceDetail> PlaceDetails { get; set; }

        #endregion
    }
}
