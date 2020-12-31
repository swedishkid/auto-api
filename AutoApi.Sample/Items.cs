using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutoApi.Sample
{
    [Route("items")]
    public class Items
    {
        private readonly ItemsDbContext _context;

        public Items(ItemsDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("")]
        public async Task<GetItemsResult> GetItems(CancellationToken cancellationToken)
        {
            var items = await _context.Items.ToListAsync(cancellationToken);
            var result = new GetItemsResult(items);
            return result;
        }
    }
}