/// <summary>
/// General interface to ensure all classes has CRUD and get all operations
/// that all has an Id in the model
/// </summary>
/// <typeparam name="T">Class</typeparam>
public interface IDataService<T> where T : class, IHasID
{
	int Create(T entity);
	T? Read(int id);
	void Update(T entity);
	void Delete(int id);
	List<T> GetAll();
}