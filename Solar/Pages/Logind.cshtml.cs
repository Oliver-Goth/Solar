using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class LogindModel : PageModel
    {
        private IUsersDataService _service;

        public LogindModel(IUsersDataService userdata)
        {
            _service = userdata;
        }

        public static User LoggedinUser { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; } 


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            LoggedinUser = _service.VerifyUser(UserName, Password);

            if (LoggedinUser == null)
            {
                ErrorMessage = "Forkert Brugernavn eller Password";
                return Page();
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                BuildClaimsPrincipal(LoggedinUser));

            if (LoggedinUser.Username == "admin")
                return RedirectToPage("/Intern/Requests");

            return RedirectToPage("/Ekstern/NewProject");
        }

        private ClaimsPrincipal BuildClaimsPrincipal(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.UserData, user.Id.ToString())
            };
            if(user.Username == "admin")
                claims.Add(new Claim(ClaimTypes.Role, "admin"));

            ClaimsIdentity claimsIdentity= new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
