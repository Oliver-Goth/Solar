namespace Solar.Services.StaticServices
{
    public static class GlobalProjectDataService
    {
        public static Project? ProjectDataNewProject { get; set; }
        public static Project? ProjectDataStepOne { get; set; }
        public static Project? ProjectDataStepTwo { get; set; }
        public static Project? ProjectDataStepThree { get; set; }
        public static Project? ProjectDataStepThreePointFive { get; set; }
        public static Project? ProjectDataStepFour { get; set; }



        public static Project Merge()
        {
            Project? mergedProject = new Project();

            mergedProject.CaseName = ProjectDataNewProject.CaseName;
            mergedProject.Address = ProjectDataNewProject.Address;
            mergedProject.Zip = ProjectDataNewProject.Zip;
            mergedProject.StartDate = ProjectDataNewProject.StartDate;
            mergedProject.Followup = ProjectDataNewProject.Followup;
            mergedProject.Deadline = ProjectDataNewProject.Deadline;

            mergedProject.Assembly = new Assembly();
            mergedProject.Assembly.RoofTypeId = ProjectDataStepOne.Assembly.RoofTypeId;
            mergedProject.Assembly.RoofMaterialId = ProjectDataStepOne.Assembly.RoofMaterialId;
            mergedProject.Assembly.EastWestDirection = ProjectDataStepOne.Assembly.EastWestDirection;
            mergedProject.Assembly.Slope = ProjectDataStepOne.Assembly.Slope;
            mergedProject.Assembly.BuildingHeight = ProjectDataStepOne.Assembly.BuildingHeight;

            mergedProject.Battery = new Battery();
            mergedProject.Battery.BatteryPrepare = ProjectDataStepTwo.Battery.BatteryPrepare;

            mergedProject.BatteryRequest = new BatteryRequest();
            mergedProject.BatteryRequest.Capacity = ProjectDataStepTwo.BatteryRequest.Capacity;

            mergedProject.DimensioningId = ProjectDataStepThree.DimensioningId;

            mergedProject.DimensioningkWp = new DimensioningkWp();
            mergedProject.DimensioningkWp.KiloWattPeak = ProjectDataStepThree.DimensioningkWp.KiloWattPeak;

            mergedProject.Remarks = ProjectDataStepFour.Remarks;

            mergedProject.DimensioningConsumption = new DimensioningConsumption();

            if(ProjectDataStepThreePointFive != null)
            {
                mergedProject.DimensioningConsumption.CategoryId = ProjectDataStepThreePointFive.DimensioningConsumption.CategoryId;
                mergedProject.DimensioningConsumption.CurrentConsumption = ProjectDataStepThreePointFive.DimensioningConsumption.CurrentConsumption;
                mergedProject.DimensioningConsumption.HeatPump = ProjectDataStepThreePointFive.DimensioningConsumption.HeatPump;
                mergedProject.DimensioningConsumption.HeatPumpIncluded = ProjectDataStepThreePointFive.DimensioningConsumption.HeatPumpIncluded;
                mergedProject.DimensioningConsumption.HouseSize = ProjectDataStepThreePointFive.DimensioningConsumption.HouseSize;
                mergedProject.DimensioningConsumption.EvKilometer = ProjectDataStepThreePointFive.DimensioningConsumption.EvKilometer;
                mergedProject.DimensioningConsumption.ElectricVehicle = ProjectDataStepThreePointFive.DimensioningConsumption.ElectricVehicle;
                mergedProject.DimensioningConsumption.Evincluded = ProjectDataStepThreePointFive.DimensioningConsumption.Evincluded;
            }

            return mergedProject;

        }
        // Make new project object and merge from others 

    }
}
