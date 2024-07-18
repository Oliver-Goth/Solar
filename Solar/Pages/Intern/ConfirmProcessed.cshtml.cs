using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Solar.Pages.Intern
{
    [Authorize(Policy = "admin")]
    public class ConfirmProcessedModel : PageModel
    {
        private IProjectDataService _projectDataService;

        public ConfirmProcessedModel(IProjectDataService projectDataService)
        {
            _projectDataService = projectDataService;
        }

        public IActionResult OnGet(int id)
        {
            Project ConfirmProject = _projectDataService.Read(id);
            ConfirmProject.StatusId = 3;
            _projectDataService.Update(ConfirmProject);

            return RedirectToPage("Processed");
        }
    }
}
