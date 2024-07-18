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

            await _emailSender.SendEmailAsync(Sender, Reciever, "Tilbudsanmodning er sendt til Solar", $"Info indtastet p� sagen:" +
                $"\nSagsinfo: {InfoDump.CaseName}" +
                $"\nAdresse: {InfoDump.Address}" +
                $"\nPostnr: {InfoDump.Zip}" +
                $"\nStart dato: {InfoDump.StartDate}" +
                $"\nFollowup dato: {InfoDump.Followup}" +
                $"\nDeadline dato: {InfoDump.Deadline}" +
                $"\nMonteage type: {RoofType.RoofType1}" +
                $"\ntag type: {RoofMaterial.Material}" +
                $"\n�nskes �st/vest placering: {(InfoDump.Assembly.EastWestDirection == true ? "Ja"
                : "Nej")}" +
                $"\nH�ldning: {InfoDump.Assembly.Slope}" +
                $"\nH�jde p� bygning: {InfoDump.Assembly.BuildingHeight}" +
                $"\nForberedes til batteri: {InfoDump.Battery.BatteryPrepare}" +
                $"\nSt�rrelse: {InfoDump.BatteryRequest.Capacity}kWh" +
                $"\n{(InfoDump.DimensioningId == 1 ? "Solecelle �nske: Solcelleanl�g dimensioneres efter forbrug"
                : InfoDump.DimensioningId == 2 ? "Solecelle �nske: Solcelleanl�g dimensioneres efter st�rst mulig anl�g/stor produktion"
                : InfoDump.DimensioningId == 3 ? "Solecelle �nske: Solcelleanl�g dimensioneres efter �nsket" + InfoDump.DimensioningConsumption
                : "Solecelle �nske: Ikke angivet")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.CategoryId == 1 ? "Anl�gskategori: Privat"
                : InfoDump.DimensioningConsumption.CategoryId == 2 ? "Anl�gskategori: Erhverv"
                : InfoDump.DimensioningConsumption.CategoryId == 3 ? "Anl�gskategori: Offentlig"
                : "Anl�gskategori: Ikke angivet")
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? "Nuv�rende forbrug i kWh: " + InfoDump.DimensioningConsumption.CurrentConsumption
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.HeatPump == true ? "Er/kommer der varmepumpe: Ja"
                : "Er/kommer der varmepumpe: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.HeatPump == true ? (InfoDump.DimensioningConsumption.HeatPumpIncluded == true ? "Er varmepumpe inkluderet i nuv�rende forbrug: Ja"
                : "Er varmepumpe inkluderet i nuv�rende forbrug: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? "St�rrelse hus i m2: " + InfoDump.DimensioningConsumption.HouseSize
                : "")}" +
                $"\n{(InfoDump.DimensioningId == 1 ? (InfoDump.DimensioningConsumption.ElectricVehicle == true ? "Er/kommer der elbil: Ja"
                : "Er/kommer der elbil: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.ElectricVehicle == true ? (InfoDump.DimensioningConsumption.Evincluded == true ? "Er elbil inkluderet i nuv�rende forbrug: Ja"
                : "Er elbil inkluderet i nuv�rende forbrug: Nej")
                : "")}" +
                $"\n{(InfoDump.DimensioningConsumption.ElectricVehicle == true ? "Hvor mange km k�res pr. �r: " + InfoDump.DimensioningConsumption.EvKilometer
                : "")}"
                );


            await _emailSender.SendEmailAsync(Sender, Sender, $"Ny tilbudsanmodning p� adressen {InfoDump.Address}", "Find sagen her: https://philipv.dk/Intern/Requests");

            InfoDump.StatusId = 1;
            InfoDump.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);

            _service.Create(InfoDump);

            return RedirectToPage("/Ekstern/SuccesRequest");
        }

    }
}
