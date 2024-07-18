namespace Solar.Services.StaticServices
{
    public static class MissingInformationCounterService
    {
        static EFCProjectDataService projectDataService { get; set; } = new EFCProjectDataService();
        public static int CountMissingInformation(int projectId)
        {
            // 1. get project, by id
            Project project = projectDataService.Read(projectId);

            // 2. define counter
            int missingInformationCounter = 0;

            // 3. check if fields are null, add up counter
            if(project != null) 
            {
                if (project.CaseName == null)
                    missingInformationCounter++;

                if(project.Deadline == null)
                    missingInformationCounter++;

                if(project.Followup == null)
                    missingInformationCounter++;

                if(project.StartDate == null)
                    missingInformationCounter++;

                if(project.Assembly.Slope == null)
                    missingInformationCounter++;

                if(project.Assembly.BuildingHeight == null)
                    missingInformationCounter++;

                if(project.BatteryRequest.Capacity <= 0 && project.Battery.BatteryPrepare == true)
                    missingInformationCounter++;

                if (project.DimensioningId == null)
                    missingInformationCounter++;

                if (project.DimensioningId == 1)
                {
                    if (project.DimensioningConsumption.CurrentConsumption == null)
                        missingInformationCounter++;

                    if(project.DimensioningConsumption.CategoryId == null)
                        missingInformationCounter++;


                    if (project.DimensioningConsumption.HouseSize == null || project.DimensioningConsumption.HouseSize <= 0)
                        missingInformationCounter++;

                    if ((project.DimensioningConsumption.EvKilometer == null || project.DimensioningConsumption.EvKilometer <= 0) && project.DimensioningConsumption.ElectricVehicle == true)
                        missingInformationCounter++;
                }

                if(project.DimensioningId == 3 && project.DimensioningkWp.KiloWattPeak == 0)
                {
                    
                    missingInformationCounter++;
                }
            }

            return missingInformationCounter;
        }
    }
}
