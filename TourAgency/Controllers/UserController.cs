namespace TourAgency.Controllers
{
    using global::TourAgency.Models;
    using global::TourAgency.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    namespace TourAgency.Controllers
    {
        public class UserController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public UserController(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            // READ: Отримати всіх користувачів
            public IActionResult Index()
            {
                var users = _userManager.Users.ToList();
                return View(users);
            }

            // CREATE: Форма для створення нового користувача
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // CREATE: Додати нового користувача
            [HttpPost]
            public async Task<IActionResult> Create(ApplicationUser model, string password)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        DateOfBirth = model.DateOfBirth
                    };

                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(model);
            }



            // UPDATE: Форма для редагування користувача
            [HttpGet]
            public async Task<IActionResult> Edit(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }

            // UPDATE: Оновити користувача
            [HttpPost]
            public async Task<IActionResult> Edit(ApplicationUser model)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.DateOfBirth = model.DateOfBirth;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser(ApplicationUser model, string password)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        DateOfBirth = model.DateOfBirth
                    };

                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(model);
            }
        

        // DELETE: Видалити користувача
        [HttpPost]
            public async Task<IActionResult> Delete(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Помилка при видаленні користувача");
                    }
                }

                return RedirectToAction("Index");
            }
        }
    }

}
