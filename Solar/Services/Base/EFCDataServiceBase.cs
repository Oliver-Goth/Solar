using Microsoft.EntityFrameworkCore;
/// <summary>
/// Base implementation of Entity Framework Core
/// of all classes with an Id in the model
/// </summary>
/// <typeparam name="T">Class</typeparam>
public abstract class EFCDataServiceBase<T> : IDataService<T> where T : class, IHasID
{
	/// <summary>
	/// Base method to insert into the database
	/// </summary>
	/// <param name="entity">Entity to be inserted</param>
	/// <returns>Generated number from database</returns>
	public virtual int Create(T entity)
	{
		using DbContext context = CreateDBContext();

		context.Set<T>().Add(entity);
		context.SaveChanges();

		return entity.Id;
	}

	/// <summary>
	/// Base method to read from the database
	/// </summary>
	/// <param name="id">id of the entity</param>
	/// <returns>Object of the class</returns>
	public virtual T? Read(int id)
	{
		using DbContext context = CreateDBContext();

		return GetAllWithIncludes(context).FirstOrDefault(x => x.Id == id);
	}

	/// <summary>
	/// Base method to update entity in the database
	/// </summary>
	/// <param name="entity">Object to be updated</param>
	public void Update(T entity)
	{
		using DbContext context = CreateDBContext();

		context.Update(entity);

		context.SaveChanges();
	}

	/// <summary>
	/// Base method to delete entity from the database
	/// </summary>
	/// <param name="id">id of the entity</param>
	public void Delete(int id)
	{
		using DbContext context = CreateDBContext();

		context.Set<T>().Remove(context.Set<T>().Find(id));

		context.SaveChanges();
	}

	/// <summary>
	/// Base method to get all objects of class
	/// </summary>
	/// <returns>List of objects, of generic</returns>
	public List<T> GetAll()
	{
		using DbContext context = CreateDBContext();

		return GetAllWithIncludes(context).ToList();
	}

	/// <summary>
	/// Decides on the context of which Entity Framework Core should use
	/// </summary>
	/// <returns></returns>
	protected abstract DbContext CreateDBContext();

	/// <summary>
	/// Incase of the need for further includes
	/// override this method
	/// </summary>
	/// <param name="dbContext"></param>
	/// <returns></returns>
	protected virtual IQueryable<T> GetAllWithIncludes(DbContext dbContext) 
	{
		return dbContext.Set<T>();
	}
}