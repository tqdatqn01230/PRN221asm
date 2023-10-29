using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty(Name = "mess", SupportsGet = true)]
        public string mess { get; set; }

        public const string SessionKeyLogin = "_login";

        private readonly PRN221_As02Context _context;

        public LoginModel(PRN221_As02Context context)
        {
            _context = context;
        }

        public void OnGet(string mess)
        {
            this.mess = mess;
        }

        public IActionResult OnPost(string userName, string password)
        {
            var listAcc = _context.Accounts.ToList();
            var listCus = _context.Customers.ToList();

            userName = string.IsNullOrWhiteSpace(userName) ? "" : userName;
            password = string.IsNullOrWhiteSpace(password) ? "" : password;

            Account a = listAcc.FirstOrDefault(x =>
            x.UserName.ToLower().Equals(userName.ToLower()) &&
            x.Password.Equals(password)
            );
            if (a == null)
            {
                Customer c = listCus.FirstOrDefault(x =>
               x.Phone.ToLower().Equals(userName.ToLower()) &&
               x.Password.Equals(password)
               );
                if(c != null)
                {
                    string json = JsonConvert.SerializeObject(c);
                    HttpContext.Session.SetString(SessionKeyLogin, json);
                    return RedirectToPage("/Index");
                }
                return RedirectToPage("/Login/Login", new { mess = "login fail" });
            }
            else
            {
                string json = JsonConvert.SerializeObject(a);
                HttpContext.Session.SetString(SessionKeyLogin, json);
                return RedirectToPage("/Index");
            }
        }

    }
}
