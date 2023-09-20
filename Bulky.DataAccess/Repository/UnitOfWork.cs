using BulkyBook.DataAccess.Migrations;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository category
            )
        {
            _context = context;
            Category = category;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
