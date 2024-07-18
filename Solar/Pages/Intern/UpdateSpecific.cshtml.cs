using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Solar.Pages.Intern
{
    [Authorize(Policy = "admin")]
    public class UpdateSpecificModel : PageModel
    {
        private IDimensioningDataService _dimensioningDataService;
        private IProjectDataService _projectDataService;
        private IRoofTypeDataService _roofTypeDataService;
        private IConsumptionCategoryDataService _ConsCat;

        [BindProperty]
        public Project DataBaseInfo { get; set; }
        public RoofType RoofType { get; set; }
        public List<Dimensioning> Dimensions { get; set; }
        public List<ConsumptionCategory> ConsCats { get; set; }

        [BindProperty]
        public string CaseName { get; set; }
        [BindProperty]
        public int Zip { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime Followup { get; set; }
        [BindProperty]
        public DateTime Deadline { get; set; }
        [BindProperty]
        public bool EastWestDirection { get; set; }
        [BindProperty]
        public decimal Slope { get; set; }
        [BindProperty]
        public decimal BuildingHeight { get; set; }
        [BindProperty]
        public int BatteryCapacity { get; set; }
        [BindProperty]
        public int DimensioningId { get; set; }
        [BindProperty]
        public int CurrentConsumption { get; set; }
        [BindProperty]
        public int HouseSize { get; set; }
        [BindProperty]
        public int KiloWattPeak { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public int EvKilometer { get; set; }



        public UpdateSpecificModel(IProjectDataService projectDataService, IRoofTypeDataService roofTypeDataService, IDimensioningDataService dimensioningDataService, IConsumptionCategoryDataService consCat)
        {
            _projectDataService = projectDataService;
            _dimensioningDataService = dimensioningDataService;
            _roofTypeDataService = roofTypeDataService;
            _ConsCat = consCat;
        }

        public void OnGet(int id)
        {

            DataBaseInfo = _projectDataService.Read(id);

            Dimensions = _dimensioningDataService.GetAll();

            RoofType = _roofTypeDataService.Read((int)DataBaseInfo.Assembly.RoofTypeId);

            ConsCats = _ConsCat.GetAll();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            OnGet(id);

            if(DataBaseInfo.CaseName == null)
            {
                DataBaseInfo.CaseName = CaseName;
            }

            if(DataBaseInfo.Zip == null)
            {
                DataBaseInfo.Zip = Zip;
            }

            if (DataBaseInfo.StartDate == null)
            {
                DataBaseInfo.StartDate = StartDate;
            }

            if (DataBaseInfo.Followup == null)
            {
                DataBaseInfo.Followup = Followup;
            }

            if (DataBaseInfo.Deadline == null)
            {
                DataBaseInfo.Deadline = Deadline;
            }

            if (DataBaseInfo.Assembly.Slope == null)
            {
                DataBaseInfo.Assembly.Slope = Slope;
            }

            if(DataBaseInfo.Assembly.BuildingHeight == null)
            {
                DataBaseInfo.Assembly.BuildingHeight = BuildingHeight;
            }

            if(DataBaseInfo.BatteryRequest.Capacity <= 0)
            {
                DataBaseInfo.BatteryRequest.Capacity = BatteryCapacity;
            }

            if(DataBaseInfo.DimensioningConsumption.CurrentConsumption == null)
            {
                DataBaseInfo.DimensioningConsumption.CurrentConsumption = CurrentConsumption;
            }

            if(DataBaseInfo.DimensioningConsumption.HouseSize == null || DataBaseInfo.DimensioningConsumption.HouseSize <= 0)
            {
                DataBaseInfo.DimensioningConsumption.HouseSize = HouseSize;
            }

            if(DataBaseInfo.DimensioningId == 3 && DataBaseInfo.DimensioningkWp.KiloWattPeak == null || DataBaseInfo.DimensioningkWp.KiloWattPeak <= 0)
            {
                DataBaseInfo.DimensioningkWp.KiloWattPeak = KiloWattPeak;
            }

            if (DataBaseInfo.DimensioningId == null)
            {
                DataBaseInfo.DimensioningId = DimensioningId;
            }

            if (DataBaseInfo.DimensioningConsumption.CategoryId == null && DataBaseInfo.DimensioningId == 1)
            {
                DataBaseInfo.DimensioningConsumption.CategoryId = null;
                
            }

            if( DataBaseInfo.DimensioningId == 1 && CategoryId != 0)
            {
                DataBaseInfo.DimensioningConsumption.CategoryId = CategoryId;
            }

            if(DataBaseInfo.DimensioningConsumption.EvKilometer == null || DataBaseInfo.DimensioningConsumption.EvKilometer <=0)
            {
                DataBaseInfo.DimensioningConsumption.EvKilometer = EvKilometer;
            }



            System.Diagnostics.Debug.WriteLine(DataBaseInfo.DimensioningConsumption.CategoryId);
            _projectDataService.Update(DataBaseInfo);

            return RedirectToPage("/Intern/Requests");
        }
    }
}