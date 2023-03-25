using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Services.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace agSalon.Services.Services.Implementations
{
	public class GroupsService : EntityBaseRepository<GroupOfServices>, IGroupsService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		public GroupsService(AppDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task AddNewGroupAsync(GroupOfServices newGroup)
		{
			newGroup.ImgUrl = UploadFIle(newGroup.Img);
			await AddAsync(newGroup);
		}

		public async Task UpdateGroupAsync(GroupOfServices group)
		{
			if (group.Img != null)
				group.ImgUrl = UploadFIle(group.Img);

			await UpdateAsync(group);
		}

		private string UploadFIle(IFormFile file)
		{
			string uniqueFileName = null;

			if (file != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/groups");
				if (File.Exists(uploadsFolder + "/" + file.FileName))
					return file.FileName;

				uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
			}

			return uniqueFileName;
		}


		public async Task DeleteGroupAsync(int groupId)
		{
			var group = await GetByIdAsync(groupId);

			
			string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/groups");
			string path = uploadsFolder + "/" + group.ImgUrl;
			if (File.Exists(path))
				File.Delete(path);

			await DeleteAsync(groupId);
		}
	}
}
