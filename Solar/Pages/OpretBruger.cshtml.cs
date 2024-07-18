using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Solar.Pages
{
    public class OpretBruger : PageModel
    {
        private IUsersDataService _service;

        public OpretBruger(IUsersDataService service)
        {
            _service = service;
        }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string RepeatedPassword { get; set; }
        [BindProperty]
        public string Department { get; set; }
        [BindProperty]
        public string CVR { get; set; }
        [BindProperty]
        public string Installer { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {

            if (Username == null || Password == null)
            {
                ErrorMessage = "Udfyldt venligst felterne";
                return Page();
            }

            if (_service.CheckUsernameExist(Username))
            {
                ErrorMessage = "Brugernavn er allerede i brug";
                return Page();
            }

            if (Password != RepeatedPassword)
            {
                ErrorMessage = "Passwords matcher ikke";
                return Page();
            }

            User user = new User
            {
                Username = Username,
                Password = Password
            };
            user.Installer = new Installer();
            user.Installer.Installer1 = Installer;
            user.Installer.Department = Department;
            user.Installer.AccountNumber = CVR;
            user.Installer.PhoneNumber = PhoneNumber;

            _service.Create(user);
            return RedirectToPage("/Logind");
        }
    }
}
