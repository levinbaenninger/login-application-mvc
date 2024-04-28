namespace LoginApplication.Models
{
	public class Login : User
	{
		private static Login? _session = null;

		public static Login GetSession()
		{
			return _session;
		}

		public static Login LogIn(User user)
		{
			if (_session == null)
			{
				_session = new Login();
			}

			_session.Id = user.Id;
			_session.Username = user.Username;
			_session.Password = user.Password;
			_session.IsAdmin = user.IsAdmin;
			_session.IsLoggedIn = true;

			return _session;
		}

		public static void LogOut()
		{
			_session = null;
		}
	}
}
