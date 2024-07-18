using Microsoft.EntityFrameworkCore;

public class EFCRoofTypeDataService : EFCDataServiceAppBase<RoofType>, IRoofTypeDataService
{
	protected override IQueryable<RoofType> GetAllWithIncludes(DbContext dbContext)
	{
		return dbContext.Set<RoofType>()
			.Include(r => r.RoofMaterials);
	}
}