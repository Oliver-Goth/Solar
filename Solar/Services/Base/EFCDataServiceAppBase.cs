using Microsoft.EntityFrameworkCore;

/// <summary>
/// Application specific base class for Entity Framework Core
/// </summary>
/// <typeparam name="T">Type of entites to interact with</typeparam>
public class EFCDataServiceAppBase<T> : EFCDataServiceBase<T> where T : class, IHasID
{
	/// <summary>
	/// A SolarContext object is created and
	/// returned when needed
	/// </summary>
	/// <returns>DbContext</returns>
	protected override DbContext CreateDBContext()
	{
		return new SolarContext();
	}
}