using LoginApplication.Data;
using LoginApplication.Models;
using LoginApplication.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{

	public class UsersController : Controller
	{
		private ApplicationDbContext _db;
		public UsersController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			Login session = Login.GetSession();

			if (session != null && session.IsAdmin)
			{
				return RedirectToAction("Admin");
			}
			else if (session != null)
			{
				return View();
			}

			return RedirectToAction("Index", "Login");
		}

		public IActionResult LogOut()
		{
			Login session = Login.GetSession();
			session.IsLoggedIn = false;
			Login.LogOut();
			TempData["success"] = "Logged out successfully";

			_db.Update(session);
			_db.SaveChanges();
			return RedirectToAction("Index", "Login");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user, string confirmPassword)
		{
			User existingUser = _db.Users.FirstOrDefault(u => u.Username == user.Username);

			// Server-side validation	
			if (existingUser != null)
			{
				ModelState.AddModelError("Username", "Username already exists");
			}

			if (user.Password != confirmPassword)
			{
				ModelState.AddModelError("", "Passwords do not match");
			}

			if (user.Password.Length < 4)
			{

				ModelState.AddModelError("Password", "Password must be at least 4 characters long");
			}

			if (user.Username.Length < 4)
			{
				ModelState.AddModelError("Username", "Username must be at least 4 characters long");
			}

			if (ModelState.IsValid)
			{
				user.IsAdmin = false;
				user.IsLoggedIn = false;

				_db.Users.Add(user);
				_db.SaveChanges();
				TempData["success"] = "User was registered successfully";

				return RedirectToAction("Index");
			}
			else
			{
				TempData["error"] = "Registration failed";
				return View();
			}
		}

		public IActionResult Admin()
		{
			Login session = Login.GetSession();

			if (session != null)
			{
				if (session.IsAdmin)
				{
					List<User> usersList = _db.Users.ToList();
					return View(usersList);
				}
				else
				{
					return RedirectToAction("Index", "Users");
				}
			}
			else
			{
				return RedirectToAction("Index", "Users");
			}
		}

		public IActionResult Edit(int id)
		{
			Login _session = Login.GetSession();
			User user = _db.Users.FirstOrDefault(u => u.Id == id);

			if (user == null)
			{
				return RedirectToAction("Index", "Users");
			}

			if (_session != null && _session.IsAdmin)
			{
				if (_session != null && _session.Id == id)
				{
					return RedirectToAction("Index", "Users");
				}

				EditRoleVM editRoleVM = new EditRoleVM
				{
					Id = user.Id,
					Username = user.Username,
					IsAdmin = user.IsAdmin
				};

				return View(editRoleVM);
			}

			return RedirectToAction("Index", "Users");
		}

		[HttpPost]
		public IActionResult Edit(EditRoleVM editRoleVM)
		{
			User existingUser = _db.Users.FirstOrDefault(u => u.Username == editRoleVM.Username);
			//user.Password = existingUser.Password;

			// Server-side validation
			if (existingUser != null && existingUser.Id != editRoleVM.Id)
			{

				ModelState.AddModelError("Username", "Username already exists");
			}

			if (editRoleVM.Username.Length < 4)
			{
				ModelState.AddModelError("Username", "Username must be at least 4 characters long");
			}

			if (ModelState.IsValid)
			{
				User user = _db.Users.FirstOrDefault(u => u.Id == editRoleVM.Id);
				user.Username = editRoleVM.Username;
				user.IsAdmin = editRoleVM.IsAdmin;

				_db.Users.Update(user);
				_db.SaveChanges();
				TempData["success"] = "User was updated successfully";

				return RedirectToAction("Index");
			}
			else
			{
				TempData["error"] = "User update failed";
				return View();
			}
		}

		public IActionResult Delete(int id)
		{
			User user = _db.Users.FirstOrDefault(u => u.Id == id);
			Login _session = Login.GetSession();

			if (user == null)
			{
				return RedirectToAction("Index", "Users");
			}

			if (_session != null && _session.IsAdmin)
			{
				if (_session != null && _session.Id == id)
				{
					return RedirectToAction("Index", "Users");
				}

				return View(user);
			}
			return RedirectToAction("Index", "Users");
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int id)
		{
			User user = _db.Users.FirstOrDefault(u => u.Id == id);

			_db.Users.Remove(user);
			_db.SaveChanges();
			TempData["success"] = "User was deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
