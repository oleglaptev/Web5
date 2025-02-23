using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web5.Models;
using Web5.Models.ViewModels;

namespace Web5.Controllers
{
    public class Lab2Controller : Controller
    {
        //Просмотр
        [AllowAnonymous]
        public ActionResult StudentsList()
        {
            List<Студент> students = new List<Студент>();
            using (var db = new WEBEntities())
            {
                students = db.Студент.OrderBy(x => x.Фамилия).ThenBy(x => x.Имя).ThenBy(x => x.Отчество).ToList();
            }
            return View(students);
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult AgeDisplay(Guid studentID)
        {
            string ageString = " (";
            using (var db = new WEBEntities())
            {
                TimeSpan substractedDate = DateTime.Now.Subtract(db.Студент.Find(studentID).Дата_рождения);
                if (substractedDate.Days > 365)
                {
                    int age = substractedDate.Days / 365;
                    ageString += age.ToString() + ")";
                }
            }
            return PartialView("AgeDisplay", ageString);
        }
        [Authorize]
        public ActionResult StudentDetails(Guid studentID)
        {
            Студент model = new Студент();
            using (var db = new WEBEntities())
            {
                model = db.Студент.Find(studentID);
            }
            return View(model);
        }

        //Изменение
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditStudent(Guid studentID)
        {
            StudentVM model;
            using (var db = new WEBEntities())
            {
                Студент editedStudent = db.Студент.Find(studentID);
                model = new StudentVM
                {
                    Фамилия = editedStudent.Фамилия,
                    ID_студента = editedStudent.ID_студента,
                    Имя = editedStudent.Имя,
                    Отчество = editedStudent.Отчество,
                    Дата_рождения = editedStudent.Дата_рождения,
                    Пол = editedStudent.Пол,
                    Адрес_проживания = editedStudent.Адрес_проживания,
                    Уровень_владения_ИЯ = editedStudent.Уровень_владения_ИЯ,

                };
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditStudent(StudentVM model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new WEBEntities())
                {
                    Студент editedStudent = new Студент
                    {
                        ID_студента = model.ID_студента,
                        Фамилия = model.Фамилия,
                        Имя = model.Имя,
                        Отчество = model.Отчество,
                        Адрес_проживания = model.Адрес_проживания,
                        Дата_рождения = model.Дата_рождения,
                        Уровень_владения_ИЯ = model.Уровень_владения_ИЯ,
                        Пол = model.Пол,
                    };
                    db.Студент.Attach(editedStudent);
                    db.Entry(editedStudent).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("StudentsList");
            }
            return View(model);
        }

        //Добавление
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateStudent(StudentVM newStudent)
        {
            if (ModelState.IsValid)
            {
                using (var db = new WEBEntities())
                {
                    Студент studentToCreate = new Студент
                    {
                        ID_студента = Guid.NewGuid(),
                        Фамилия = newStudent.Фамилия,
                        Имя = newStudent.Имя,
                        Отчество = newStudent.Отчество,
                        Пол = newStudent.Пол,
                        Дата_рождения = newStudent.Дата_рождения,
                        Адрес_проживания = newStudent.Адрес_проживания,
                        Уровень_владения_ИЯ = newStudent.Уровень_владения_ИЯ,

                    };
                    db.Студент.Add(studentToCreate);
                    db.SaveChanges();
                }
                return RedirectToAction("StudentsList");
            }

            return View(newStudent);
        }

        //Удаление
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteStudent(Guid studentid)
        {
            Студент studentToDelete;
            using (var db = new WEBEntities())
            {
                studentToDelete = db.Студент.Find(studentid);
            }
            return View(studentToDelete);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult DeleteConfirmed(Guid studentID)
        {
            using (var db = new WEBEntities())
            {
                Студент studentToDelete = new Студент
                {
                    ID_студента = studentID,
                };
                db.Entry(studentToDelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("StudentsList");
        }

        //Авторизация User1 123  admin admin123
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM webUser)
        {
            if (ModelState.IsValid)
            {
                using (WEBEntities context = new WEBEntities())
                {
                    Пользователь user = null;
                    user = context.Пользователь.Where(u => u.Login == webUser.Login).FirstOrDefault();
                    if (user != null)
                    {
                        string passwordHash = ReturnHashCode(webUser.Password + user.Salt.ToString().ToUpper());
                        if (passwordHash == user.PasswordHash)
                        {
                            string userRole = "";

                            switch (user.UserRole)
                            {
                                case 2:
                                    userRole = "Admin";
                                    break;
                                case 1:
                                    userRole = "Student";
                                    break;
                            }
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                        1,
                                                        user.Login,
                                                        DateTime.Now,
                                                        DateTime.Now.AddDays(1),
                                                        false,
                                                        userRole
                                                        );
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
                            return RedirectToAction("StudentsList", "Lab2");
                        }
                    }
                }
            }
            ViewBag.Error = "Пользователя с таким логином и паролем не существует, попробуйте ещё.";
            return View(webUser);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        string ReturnHashCode(string loginAndSalt)
        {
            string hash = "";
            using (SHA1 sha1hash = SHA1.Create())
            {
                byte[] data = sha1hash.ComputeHash(Encoding.UTF8.GetBytes(loginAndSalt));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                hash = sBuilder.ToString().ToUpper();
            }
            return hash;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("StudentsList", "Lab2");
        }
    }
}