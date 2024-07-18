public interface IProjectDataService : IDataService<Project>
{
	/// <summary>
	/// Used to sort projects by statusID
	/// </summary>
	/// <param name="statusId">id of the statusID</param>
	/// <returns>List of projects</returns>
	public List<Project> SortByStatus(int statusId);
}