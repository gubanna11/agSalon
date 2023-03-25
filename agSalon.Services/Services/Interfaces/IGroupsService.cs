using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Services.Services.Interfaces
{
    public interface IGroupsService : IEntityBaseRepository<GroupOfServices>
	{
		Task AddNewGroupAsync(GroupOfServices newGroup);
		Task UpdateGroupAsync(GroupOfServices group);
		Task DeleteGroupAsync(int groupId);
	}	
}
