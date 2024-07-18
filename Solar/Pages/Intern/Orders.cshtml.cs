using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Models.Static;
using Solar.Services.StaticServices;

namespace Solar.Pages.Intern
{
    [Authorize(Policy = "admin")]
    public class Orders : PageModel
    {
                private IProjectDataService ProjectDataService;
        public List<Project> AllProjects { get; set; }
        public Orders(IProjectDataService projectDataService)
        {
            ProjectDataService = projectDataService;
        }
        public void OnGet()
        {
            AllProjects = ProjectDataService.SortByStatus(3);
        }
    }
}
