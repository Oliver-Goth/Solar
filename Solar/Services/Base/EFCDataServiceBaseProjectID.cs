using Microsoft.EntityFrameworkCore;
/// <summary>
/// Base implementation of Entity Framework Core
/// of all classes with an ProjectId in the model
/// </summary>
/// <typeparam name="T">Class</typeparam>
public abstract class EFCDataServiceBaseProjectID<T> : IDataServiceProjectID<T> where T : class, IHasProjectId
{
    /// <summary>
    /// Base method to insert into the database
    /// </summary>
    /// <param name="entity">Entity to be inserted</param>
    /// <returns>Generated number from database</returns>
    public int Create(T entity)
	{
		using DbContext context = CreateDBContext();

		context.Set<T>().Add(entity);
		context.SaveChanges();

		return entity.ProjectId;
	}

    /// <summary>
    /// Base method to read from the database
    /// </summary>
    /// <param name="id">ProjctId of the entity</param>
    /// <returns>Object of the class</returns>
    public T? Read(int projectId)
	{
		using DbContext context = CreateDBContext();

		return GetAllWithIncludes(context).FirstOrDefault(x => x.ProjectId == projectId);
	}

    /// <summary>
    /// Base method to update entity in the database
    /// </summary>
    /// <param name="entity">Object to be updated</param>
    public void Update(T entity)
	{
		using DbContext context = CreateDBContext();

		context.Update(entity);
	}

    /// <summary>
    /// Base method to delete entity from the database
    /// </summary>
    /// <param name="id">ProjectId of the entity</param>
    public void Delete(int projectId)
	{
		using DbContext context = CreateDBContext();

		context.Set<T>().Remove(context.Set<T>().Find(projectId));
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

	protected abstract DbContext CreateDBContext();

	protected virtual IQueryable<T> GetAllWithIncludes(DbContext dbContext)
	{
		return dbContext.Set<T>();
	}

}