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
    public class DeleteModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public DeleteModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }
            var supply = await _context.Suppliers.FindAsync(id);

            if (supply != null)
            {
                Supply = supply;
                _context.Suppliers.Remove(Supply);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
