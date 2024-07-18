using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Solar.Services.StaticServices;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class ProjectStepOneModel : PageModel
    {
        private IRoofTypeDataService _roofTypeDataService;

        private IRoofMaterialDataService _roofMaterielService;

        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project? ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public Project CaseData { get; set; }
        public User LoggedinUser { get; set; }
        public List<RoofType> Roofs { get; set; }
        public List<RoofMaterial> RoofMaterials { get; set; }
        public string ErrorMessage { get; set; }

        public ProjectStepOneModel(IRoofTypeDataService roofTypeDataService, IRoofMaterialDataService roofMaterielService, IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataStepOne;
            CaseData = GlobalProjectDataService.ProjectDataNewProject;
            _roofTypeDataService = roofTypeDataService;
            _roofMaterielService = roofMaterielService;
            _usersDataService = usersDataService;
            

        }

        public void OnGet()
        {
            Roofs = _roofTypeDataService.GetAll();
            RoofMaterials = _roofMaterielService.GetAll();

            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));
        }

        public IActionResult OnPost()
        {
            GlobalProjectDataService.ProjectDataStepOne = ProjectData;

            if (ProjectData.Assembly.RoofMaterialId == null || ProjectData.Assembly.RoofTypeId == null)
            {
                ErrorMessage = "Du skal udfylde Montage type og Tag Type";
                OnGet();
                return Page();
            }

            return RedirectToPage("/Ekstern/ProjectStepTwo");
        }
    }
}
