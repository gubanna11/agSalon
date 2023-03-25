using agSalon.Domain.Abstract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Domain.Abstract
{
	public interface IUnitOfWork
	{
		//IEntityBaseRepository<T> Repository<T>() where T : class, IEntityBase, new();

		Task SaveChangesAsync();
	}
}
