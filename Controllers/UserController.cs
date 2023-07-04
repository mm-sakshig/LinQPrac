using LinQ_Prac.Models;
using LinQ_Prac.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LinQ_Prac.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var users = _repository.GetUsers();
            return View(users);
        }

        public ActionResult Create()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        public ActionResult Details(int id)
        {
            UserModel model = _repository.GetUserById(id);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            UserModel model = _repository.GetUserById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            UserModel user = _repository.GetUserById(id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                UserModel user = _repository.GetUserById(id);
                _repository.DeleteUser(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                new RouteValueDictionary {
                  { "id", id },
                  { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}
