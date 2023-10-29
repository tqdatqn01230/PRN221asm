using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.OrderPage
{
    public class DetailsModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public DetailsModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

      public Order Orders { get; set; }
        public List<OrderDetail> orderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            orderDetails = await _context.OrderDetails
                .Where(x => x.Order.OrderId == id)
                .Include(x => x.Product)
                .Include(x=> x.Product.Supplier)
                .Include(x => x.Product.Category)
                .ToListAsync();
            if (orders == null)
            {
                return NotFound();
            }
            else 
            {
                Orders = orders;
            }
            return Page();
        }
    }
}
