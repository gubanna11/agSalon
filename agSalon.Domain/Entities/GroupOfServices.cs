﻿using agSalon.Domain.Abstract.Repositories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agSalon.Domain.Entities
{
    [Table("groups_of_services")]
	public class GroupOfServices:IEntityBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(45)]
        [Column("name")]
		
		public string Name { get; set; }


		[StringLength(100)]
		[Column("img_url")]

		public string? ImgUrl { get; set; }

		[Required(ErrorMessage = "Please choose image")]
        [NotMapped]
        public IFormFile Img { get; set; }


        
        public List<ServiceGroup> Services_Groups { get; set; }
        public List<Worker_Group> Workers_Groups { get; set; }
    }
}
