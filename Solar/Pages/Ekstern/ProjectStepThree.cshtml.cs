using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class ProjectStepThreeModel : PageModel
    {
        private IDimensioningDataService _DimensioningDataService;
        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public List<Dimensioning> Dimensions { get; set; }
        public User LoggedinUser { get; set; }

        public ProjectStepThreeModel(IDimensioningDataService dimensioningDataService, IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataNewProject;
            _DimensioningDataService = dimensioningDataService;
            _usersDataService = usersDataService;
            

        }
        public void OnGet()
        {
            Dimensions = _DimensioningDataService.GetAll();

            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));
        }

        public IActionResult OnPost()
        {
            GlobalProjectDataService.ProjectDataStepThree = ProjectData;

            if(ProjectData.DimensioningId == 1)
            {
                return RedirectToPage("/Ekstern/ProjectStepThreePointFive");
            }

            GlobalProjectDataService.ProjectDataStepThreePointFive = null;


            return RedirectToPage("/Ekstern/ProjectStepFour");
        }
    }
}
