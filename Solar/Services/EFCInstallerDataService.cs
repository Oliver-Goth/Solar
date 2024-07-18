using Microsoft.EntityFrameworkCore;

/// <summary>
/// Entity Framework Core implementation of the installer datamodel
/// This implementation does not fall under the Id or ProjectId
/// therefor it had to have it's own implementation
/// </summary>
public class EFCInstallerDataService
{
	public void Create(Installer installer)
	{
		using DbContext dbContext = CreateDbContext();

		dbContext.Set<Installer>().Add(installer);
		dbContext.SaveChanges();
	}

	public Installer Read(int userId)
	{
		using DbContext dbContext = CreateDbContext();

		return dbContext.Set<Installer>().FirstOrDefault(x => x.UserId == userId);
	}
	private DbContext CreateDbContext()
	{
		return new SolarContext();
	}
}