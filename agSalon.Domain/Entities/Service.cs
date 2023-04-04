﻿using agSalon.Domain.Abstract.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agSalon.Domain.Entities
{
    [Index(propertyNames: nameof(Name), IsUnique = true)]
    public class Service:IEntityBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [Column("price")]
        public double Price { get; set; }

        public ServiceGroup Service_Group { get; set; }
    }
}
