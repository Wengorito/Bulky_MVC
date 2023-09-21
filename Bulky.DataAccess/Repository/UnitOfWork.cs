using BulkyBook.DataAccess.Migrations;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository category,
            IProductRepository product
            )
        {
            _context = context;
            Category = category;
            Product = product;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
