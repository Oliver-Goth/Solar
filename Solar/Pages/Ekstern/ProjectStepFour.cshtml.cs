using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class ProjectStepFourModel : PageModel
    {
        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project? ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public User LoggedinUser { get; set; }
        public string StepData { get; set; }
        public ProjectStepFourModel(IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataNewProject;
            _usersDataService = usersDataService;

            if (GlobalProjectDataService.ProjectDataStepThreePointFive == null)
            {
                StepData = "/Ekstern/ProjectStepThree";
            } else
            {
                StepData = "/Ekstern/ProjectStepThreePointFive";
            }

            

    }
        public void OnGet()
        {

            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));

        }

        public IActionResult OnPost() 
        {
            
            GlobalProjectDataService.ProjectDataStepFour = ProjectData;

            return RedirectToPage("/Ekstern/ProjectStepFive");
        }
    }
}
