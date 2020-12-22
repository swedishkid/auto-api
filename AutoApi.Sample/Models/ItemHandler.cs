using System.Threading.Tasks;

namespace AutoApi.Sample.Models
{
    [Route("/items")]
    public partial class ItemHandler
    {
        private readonly IAutoApiDbContext _dbContext;

        public ItemHandler(IAutoApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<GetItemsResult> GetItems()
        {
            return new GetItemsResult();
        }
    }
}