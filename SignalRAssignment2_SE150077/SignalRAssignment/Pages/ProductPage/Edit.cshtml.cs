using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.ProductPage
{
    public class EditModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;
        public List<Category> listCat { get; set; }
        public List<Supplier> listSup { get; set; }

        public EditModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Products { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products =  await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            Products = products;
           ViewData["categoryId"] = new SelectList(_context.Categories, "categoryId", "categoryId");
           ViewData["supplyId"] = new SelectList(_context.Suppliers, "supplierId", "supplierId");
            listCat = _context.Categories.ToList();
            listSup = _context.Suppliers.ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["categoryId"] = new SelectList(_context.categories, "categoryId", "categoryId");
            //    ViewData["supplyId"] = new SelectList(_context.supplies, "supplierId", "supplierId");
            //    return Page();
            //}

            _context.Attach(Products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ProductPage/Index");
        }

        private bool ProductsExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
