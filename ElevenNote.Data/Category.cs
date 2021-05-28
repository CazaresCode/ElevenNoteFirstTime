using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        public int CateogryId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public int NoteId { get; set; }
        [ForeignKey(nameof(NoteId))]
        public virtual Note Note { get; set; }
    }
}
