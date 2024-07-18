using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace Solar.Pages.Ekstern
{
    public class RoofMaterialModel : PageModel
    {
        private IRoofMaterialDataService RoofMaterialDataService;
        public RoofMaterialModel(IRoofMaterialDataService roofMaterialDataService) 
        {
            RoofMaterialDataService = roofMaterialDataService;
        }
        public IActionResult OnGet(int id)
        {
            return new JsonResult(JsonSerializer.Serialize(RoofMaterialDataService.GetAll().Where(r => r.RoofTypeId == id)));
        }
    }
}
