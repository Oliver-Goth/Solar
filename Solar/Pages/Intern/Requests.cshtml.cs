using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Services.StaticServices;

namespace Solar.Pages.Intern
{
    [Authorize(Policy = "admin")]
    public class Request : PageModel
    {
        private IProjectDataService ProjectDataService;
        public List<Project> AllProjects { get; set; }
        public Dictionary<int, int> ProjectMissingInformation { get; set; } = new Dictionary<int, int>();
        public Request(IProjectDataService projectDataService)
        {
            ProjectDataService = projectDataService;
        }
        public void OnGet()
        {
            AllProjects = ProjectDataService.SortByStatus(1);
            foreach (Project project in AllProjects) 
            {
                ProjectMissingInformation.Add(project.Id,
                    MissingInformationCounterService.CountMissingInformation(project.Id));
            }
        }
    }
}
