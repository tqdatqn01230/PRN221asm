using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;
        public List<Category> listCat;
        public List<Supplier> listSup;


        public CreateModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["categoryId"] = new SelectList(_context.Categories, "categoryId", "categoryId");
            ViewData["supplyId"] = new SelectList(_context.Suppliers, "supplierId", "supplierId");
            listCat = _context.Categories.ToList();
            listSup = _context.Suppliers.ToList();
            return Page();
        }

        [BindProperty]
        public Product Products { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //  {
            //      listCat = _context.categories.ToList();
            //      listSup = _context.supplies.ToList();
            //      return Page();
            //  }
            if (string.IsNullOrEmpty(Products.ProductImage))
            {
                Products.ProductImage = "https://i.stack.imgur.com/V0Tjp.png";
            }
            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ProductPage/Index");
        }
    }
}
