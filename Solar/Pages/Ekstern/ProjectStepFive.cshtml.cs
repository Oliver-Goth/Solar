using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Solar.Models.Static;
using Solar.Services.Interfaces;
using Solar.Services.StaticServices;
using System.Diagnostics;
using System.Security.Claims;

namespace Solar.Pages.Ekstern
{
    public class ProjectStepFiveModel : PageModel
    {
        private IEmailSenderService _emailSender;

        private IRoofTypeDataService _roofTypeDataService;

        private IRoofMaterialDataService _roofMaterielService;

        private IProjectDataService _service;

        private IUsersDataService _usersDataService;

        [BindProperty]
        public Project ProjectData { get; set; }
        public Project ExistingData { get; set; }
        public Project InfoDump { get; set; }
        private EmailClient Sender { get; set; }
        private EmailClient Reciever { get; set; }
        public RoofType RoofType { get; set; }
        public RoofMaterial RoofMaterial { get; set; }
        public User LoggedinUser { get; set; }


        public ProjectStepFiveModel(IEmailSenderService emailSender, IRoofTypeDataService roofTypeDataService, IRoofMaterialDataService roofMaterielService, IProjectDataService service, IUsersDataService usersDataService)
        {
            ExistingData = GlobalProjectDataService.ProjectDataNewProject;
            InfoDump = GlobalProjectDataService.Merge();
            _emailSender = emailSender;
            _usersDataService = usersDataService;
            _service = service;
            _roofTypeDataService = roofTypeDataService;
            _roofMaterielService = roofMaterielService;

        }

        public void OnGet()
        {

            RoofType = _roofTypeDataService.Read((int)InfoDump.Assembly.RoofTypeId);
            RoofMaterial = _roofMaterielService.Read((int)InfoDump.Assembly.RoofMaterialId);

            LoggedinUser = _usersDataService.Read(int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value));

        }

        public async Task<IActionResult> OnPost() 
        {
            Sender = new EmailClient("SolarTestClient@hotmail.com", "Solar123456");
            Reciever = new EmailClient(User.Identity.Name);

            OnGet();

            await _emailSender.SendEmailAsync(Sender, Reciever, "Tilbudsanmodning er sendt til Solar", $"Info indtastet på sagen:" +
                $"\nSagsinfo: {InfoDump.CaseName}" +
                $"\nAdresse: {InfoDump.Address}" +
                $"\nPostnr: {InfoDump.Zip}" +
                $"\nStart dato: {InfoDump.StartDate}" +
                $"\nFollowup dato: {InfoDump.Followup}" +
                $"\nDeadline dato: {InfoDump.Deadline}" +
                $"\nMonteage type: {RoofType.RoofType1}" +
                $"\ntag type: {RoofMaterial.Material}" +
                $"\nØnskes øst/vest placering: {(InfoDump.Assembly.EastWestDirection == true ? "Ja"
                : "Nej")}" +
                $"\nHældning: {InfoDump.Assembly.Slope}" +
                $"\nHøjde på bygning: {InfoDump.Assembly.BuildingHeight}" +
                $"\nForberedes til batteri: {InfoDump.Battery.BatteryPrepare}" +
                $"\nStørrelse: {InfoDump.BatteryRequest.Capacity}kWh" +
                $"\n{(InfoDump.DimensioningId == 1 ? "Solecelle ønske: Solcelleanlæg dimensioneres efter forbrug"
                : InfoDump.DimensioningId == 2 ? "Solecelle ønske: Solcelleanlæg dimensioneres efter størst mulig anlæg/stor produktion"
                : InfoDump.DimensioningId == 3 ? "Solecelle ønske: Solcelleanlæg dimensioneres efter ønsket" + InfoDump.DimensioningConsumption
                : "Solecelle ønske: Ikke angivet")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.CategoryId == 1 ? "Anlægskategori: Privat"
                : InfoDump.DimensioningConsumption.CategoryId == 2 ? "Anlægskategori: Erhverv"
                : InfoDump.DimensioningConsumption.CategoryId == 3 ? "Anlægskategori: Offentlig"
                : "Anlægskategori: Ikke angivet")
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? "Nuværende forbrug i kWh: " + InfoDump.DimensioningConsumption.CurrentConsumption
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.HeatPump == true ? "Er/kommer der varmepumpe: Ja"
                : "Er/kommer der varmepumpe: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.HeatPump == true ? (InfoDump.DimensioningConsumption.HeatPumpIncluded == true ? "Er varmepumpe inkluderet i nuværende forbrug: Ja"
                : "Er varmepumpe inkluderet i nuværende forbrug: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? "Størrelse hus i m2: " + InfoDump.DimensioningConsumption.HouseSize
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.ElectricVehicle == true ? "Er/kommer der elbil: Ja"
                : "Er/kommer der elbil: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.ElectricVehicle == true ? (InfoDump.DimensioningConsumption.Evincluded == true ? "Er elbil inkluderet i nuværende forbrug: Ja"
                : "Er elbil inkluderet i nuværende forbrug: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.ElectricVehicle == true ? "Hvor mange km køres pr. år: " + InfoDump.DimensioningConsumption.EvKilometer
                : "")}"
                );


            await _emailSender.SendEmailAsync(Sender, Sender, $"Ny tilbudsanmodning på adressen {InfoDump.Address}", "Find sagen her: https://philipv.dk/Intern/Requests");

            InfoDump.StatusId = 1;
            InfoDump.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);

            _service.Create(InfoDump);

            return RedirectToPage("/Ekstern/SuccesRequest");
        }

    }
}
