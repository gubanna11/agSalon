using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Domain.Abstract
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		//public IEntityBaseRepository<T> Repository<T>() where T : class, IEntityBase, new()
		//{
		//	IEntityBaseRepository<T> repository = new EntityBaseRepository<T>(_context);
		//	return repository;
		//}


		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
