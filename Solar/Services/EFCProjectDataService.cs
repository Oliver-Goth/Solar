using Microsoft.EntityFrameworkCore;

public class EFCProjectDataService : EFCDataServiceAppBase<Project>, IProjectDataService
{
	/// <inheritdoc />
	public List<Project> SortByStatus(int statusId)
	{
		return base.GetAll().Where(p => p.StatusId == statusId).ToList();
	}

    protected override IQueryable<Project> GetAllWithIncludes(DbContext dbContext)
    {
		return dbContext.Set<Project>()
			.Include(x => x.Assembly)
			.Include(x => x.DimensioningConsumption)
			.Include(x => x.DimensioningkWp)
			.Include(x => x.Battery)
			.Include(x => x.BatteryRequest)
			.Include(x => x.Dimensioning)
			.Include(x => x.User)
			.ThenInclude(x => x.Installer)
			.Include(x => x.Assembly.RoofType)
			.ThenInclude(x => x.RoofMaterials);
    }
}