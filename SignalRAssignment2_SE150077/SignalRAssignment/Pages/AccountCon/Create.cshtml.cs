using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.AccountCon
{
    public class CreateModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public CreateModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var acc = new Account();
            acc.UserName = Account.UserName;
            acc.Password = Account.Password;
            acc.FullName = Account.FullName;
            acc.Type = 2;
            _context.Accounts.Add(acc);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
