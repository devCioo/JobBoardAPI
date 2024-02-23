using JobBoardAPI.Entities;

namespace JobBoardAPI.Services
{
    public class DataSeeder
    {
        private readonly JobBoardDbContext _dbContext;

        public DataSeeder(JobBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.AddRange(categories);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Category> GetCategories()
        {
            string[] names =
                ["Accounting", "Construction", "Delivery", "Education", "Gastronomy", "IT", "Logistics", "Mechanics", "Production", "Sales"];
            var categories = new List<Category>();

            foreach (var name in names)
            {
                categories.Add(new Category()
                {
                    Name = name
                });
            }

            return categories;
        }
    }
}
