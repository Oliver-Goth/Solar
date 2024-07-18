public interface IUsersDataService : IDataService<User> 
{
	/// <summary>
	/// Used to check if a user login credentials is correct
	/// </summary>
	/// <param name="username">Provided username</param>
	/// <param name="password">Procided password</param>
	/// <returns>A user object. Null if login credentials are incorrect</returns>
	User? VerifyUser(string username, string password);

	/// <summary>
	/// Check if a username is already in use
	/// </summary>
	/// <param name="username">Request username</param>
	/// <returns>True or false</returns>
	bool CheckUsernameExist(string username);
}