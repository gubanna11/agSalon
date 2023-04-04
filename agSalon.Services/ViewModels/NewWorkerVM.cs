using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Services.ViewModels
{
    public class NewWorkerVM
    {
        [Key]
        [Column("id"), Required]
        public string Id { get; set; }

        [Column("name"), Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Column("surname"), Required]
        [StringLength(45)]
        public string Surname { get; set; }


        [Column("phone"), Required]
        [StringLength(13)]
        public string Phone { get; set; }

        [Column("date_birth", TypeName = "date"), Required]
        public DateTime DateBirth { get; set; }

        [Column("address"), Required]
        [StringLength(45)]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }


        public List<int> GroupsIds { get; set; }
    }
}
