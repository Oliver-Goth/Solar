/// <summary>
/// General interface to ensure all classes has CRUD and get all operations
/// that all has an ProjectId in the model
/// </summary>
/// <typeparam name="T">Class</typeparam>
public interface IDataServiceProjectID<T> where T : class, IHasProjectId
{
	int Create(T entity);
	T? Read(int projectId);
	void Update(T entity);
	void Delete(int projectId);
	List<T> GetAll();
}