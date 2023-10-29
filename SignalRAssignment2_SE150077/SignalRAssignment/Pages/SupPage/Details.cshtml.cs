using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.SupPage
{
    public class DetailsModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public DetailsModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

      public Supplier Supply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supply = await _context.Suppliers.FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supply == null)
            {
                return NotFound();
            }
            else 
            {
                Supply = supply;
            }
            return Page();
        }
    }
}
