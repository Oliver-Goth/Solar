using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class NewProjectModel : PageModel
    {
        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project? ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public User LoggedinUser { get; set; }
        public string InstallerDepartment { get; set; }
        public string InstallerName { get; set; }
        public string ErrorMessage { get; set; }

        public NewProjectModel(IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataNewProject;
            _usersDataService = usersDataService;
        }

        public void OnGet()
        {
            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));

        }

        public IActionResult OnPost() 
        {
            
            GlobalProjectDataService.ProjectDataNewProject = ProjectData;

            if(ProjectData.Address == null)
            {
                ErrorMessage = "Du skal udfylde en adresse og postNr";
                OnGet();
                return Page();
            }
            
            return RedirectToPage("/Ekstern/ProjectStepOne");

        }

    }
}
