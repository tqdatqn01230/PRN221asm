using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.ProductPage
{
    public class DetailsModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public DetailsModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

      public Product Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            else 
            {
                Products = products;
                ViewData["cat"] = _context.Categories.FirstOrDefault(x => x.CategoryId == products.CategoryId).CategoryName;
                ViewData["sup"] = _context.Suppliers.FirstOrDefault(x => x.SupplierId == products.SupplierId).CompanyName;
            }
            return Page();
        }
    }
}
