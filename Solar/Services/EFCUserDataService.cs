using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class EFCUserDataService : EFCDataServiceAppBase<User>, IUsersDataService
{
	private PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
	public bool CheckUsernameExist(string username)
	{
		User? user = GetAll().FirstOrDefault(x => x.Username == username);
		return user != null ? true : false;
	}

	public override int Create(User entity)
	{
		entity.Password = passwordHasher.HashPassword(entity.Username, entity.Password);
		return base.Create(entity);
	}

	public User? VerifyUser(string username, string password)
	{
		User? user = GetAll().FirstOrDefault(u => u.Username == username);

		if (user == null ||
			passwordHasher.VerifyHashedPassword(
				username,
				user.Password,
				password)
			!= PasswordVerificationResult.Success)
			return null;

		return user;
	}

    protected override IQueryable<User> GetAllWithIncludes(DbContext dbContext)
    {
		return dbContext.Set<User>()
			.Include(u => u.Installer);
    }
}