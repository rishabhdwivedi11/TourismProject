using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Models
{
    [Table("PlaceDetails")]
    public class PlaceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaceDetailId { get; set; }

        #region Navigation Properties to Place Model 

        [Required]
        public int PlaceId { get; set; }


        [ForeignKey(nameof(PlaceDetail.PlaceId))]
        public Place Place { get; set; }

        #endregion
    }
}
