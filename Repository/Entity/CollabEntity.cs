using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string EmailId { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual NoteEntity Note { get; set; }
    }
}
