using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class ProjectStepThreePointFiveModel : PageModel
    {
        private IConsumptionCategoryDataService _ConsCat;
        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public List<ConsumptionCategory> ConsCats { get; set; }
        public User LoggedinUser { get; set; }
        public ProjectStepThreePointFiveModel(IConsumptionCategoryDataService consCat, IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataNewProject;
            _ConsCat = consCat;
            _usersDataService = usersDataService;
            
        }
        public void OnGet()
        {
            ConsCats = _ConsCat.GetAll();

            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));
        }

        public IActionResult OnPost() 
        {
            
            GlobalProjectDataService.ProjectDataStepThreePointFive = ProjectData;
            return RedirectToPage("/Ekstern/ProjectStepFour");
        }
    }
}
