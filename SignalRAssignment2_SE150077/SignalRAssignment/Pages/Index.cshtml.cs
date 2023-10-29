using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages
{
    public class IndexModel : PageModel
    {
        public List<Category> listCat;
        public string searchName { get; set; }
        public int? catSearch { get; set; }
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;
        private readonly ILogger<IndexModel> _logger;
        public const string SessionKeyLogin = "_login";
        public Account acc { get; set; }
        public Customer cust { get; set; }

        public IndexModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        public async Task OnGetAsync(string searchName, int? catSearch)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyLogin)))
            {
                string json = HttpContext.Session.GetString(SessionKeyLogin);
                try
                {
                    acc = JsonConvert.DeserializeObject<Account>(json);
                }
                catch (Exception ex)
                {
                    cust = JsonConvert.DeserializeObject<Customer>(json);
                }
            }

            if (_context.Products != null)
            {
                Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                this.searchName = searchName;
                Products = Products.Where(x => x.ProductName.ToLower().Contains(
                    searchName.Trim().ToLower()
                    )).ToList();
            }
            if (catSearch != null)
            {
                this.catSearch = catSearch;
                Products = Products.Where(x => x.CategoryId == catSearch).ToList();
            }
            listCat = _context.Categories.ToList();
        }
    }
}