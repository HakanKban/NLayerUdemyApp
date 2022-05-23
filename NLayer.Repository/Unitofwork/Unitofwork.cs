using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Unitofwork
{
    public class Unitofwork : IUnitofwork
    {
        private readonly AppDbContext _context;

        public Unitofwork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
           _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
