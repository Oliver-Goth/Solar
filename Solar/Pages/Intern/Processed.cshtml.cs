using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;

namespace Solar.Pages.Intern
{
    [Authorize(Policy = "admin")]
    public class Processed : PageModel
    {
        private IProjectDataService ProjectDataService;
        public List<Project> AllProjects { get; set; }
        public Processed(IProjectDataService projectDataService)
        {
            ProjectDataService = projectDataService;
        }
        public void OnGet()
        {
            AllProjects = ProjectDataService.SortByStatus(2);
        }

        public IActionResult OnPost(int id)
        {
            Project ProjectDataChange = ProjectDataService.Read(id);
            ProjectDataChange.StatusId = 3;
            ProjectDataService.Update(ProjectDataChange);

            return RedirectToPage("Processed");
        }
    }
}
