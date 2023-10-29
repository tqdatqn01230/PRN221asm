using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignalRAssignment.Data;
using SignalRAssignment.Models;


namespace SignalRAssignment.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;
        [BindProperty]
        public Customer customers { get; set; }

        public RegisterModel(PRN221_As02Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            try
            {
                var c = _context.Customers.FirstOrDefault(
                    x => x.Phone == customers.Phone && x.Password == customers.Password
                    );
                if( c != null )
                {
                    throw new Exception();
                }
                _context.Customers.Add(customers);
                _context.SaveChanges();
                return RedirectToPage("/Login/Login");
            }catch(Exception ex)
            {
                return RedirectToPage();
            }
        }
    }
}
