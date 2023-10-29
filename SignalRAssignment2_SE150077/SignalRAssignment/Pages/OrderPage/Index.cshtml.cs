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
    public class IndexModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public IndexModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IList<Order> Orders { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Orders = await _context.Orders.ToListAsync();
            }
        }
    }
}
