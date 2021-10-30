using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social.Entity
{
    [Table(nameof(Message))]
    public class Message
    {
        [Key, Required]
        public virtual int MessageId { get; set; }

        [Required, StringLength(128)]
        public virtual string UserName { get; set; }

        [Required]
        public virtual DateTime DateTime { get; set; }

        [Required]
        public virtual bool Sis { get; set; }

        [Required]
        public virtual bool Bro { get; set; }
    }
}
