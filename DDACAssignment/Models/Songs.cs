using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDACAssignment.Models
{
    public class Songs
    {
        [Key]
        public int SongID { get; set; }

        [Display(Name = "Song Name")]  //If without this, will be defaulted to be same as the variable name
        [Required]
        [StringLength(100, ErrorMessage = "The Flower Name between 6 - 100 chars", MinimumLength = 6)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Should Start with Capital")]
        public string SongName { get; set; }

        [Display(Name = "Song Upload Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime SongUploadDate { get; set; }

        [Display(Name = "Song Genre")]  //If without this, will be defaulted to be same as the variable name
        [Required]
        public string SongGenre { get; set; }

        [Display(Name = "Producer Name")]
        [Required]
        public string ProducerName { get; set; }
        
        //Indictae the filename of the songs

        [Display(Name = "FileName")]
        public string FileName { get; set; }
    }
}
