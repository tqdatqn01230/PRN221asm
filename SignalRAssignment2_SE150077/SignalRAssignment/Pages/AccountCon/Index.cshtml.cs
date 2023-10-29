using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.AccountCon
{
    public class IndexModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;
        public const string SessionKeyLogin = "_login";
        public Account acc { get; set; }
        public Customer cust { get; set; }

        public IndexModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
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
            if (_context.Accounts != null)
            {
                Account = await _context.Accounts.ToListAsync();
            }
        }
    }
}
