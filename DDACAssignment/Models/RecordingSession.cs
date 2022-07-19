using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DDACAssignment.Models
{
    public class RecordingSession
    {
        public int ID { get; set; }

        public int SessionID { get; set; }

        [Display(Name = "Song Name")]
        [Required]
        [StringLength(100, ErrorMessage = "The song name has to be between 5 - 100 chars!", MinimumLength = 5)]
        public string SongName { get; set; }

        [Display(Name = "Start Date Time")]
        //[DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "End Date Time")]
        //[DataType(DataType.Date)]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Producer Name")]
        [Required]
        [StringLength(100, ErrorMessage = "The producer name has to be between 6 - 100 chars!", MinimumLength = 6)]
        public string ProducerName { get; set; }//change to drop down list if possible

        [Display(Name = "Composer Name")]
        [Required]
        [StringLength(100, ErrorMessage = "The flower name has to be between 6 - 100 chars!", MinimumLength = 6)]
        public string ComposerName { get; set; }
    }
}
