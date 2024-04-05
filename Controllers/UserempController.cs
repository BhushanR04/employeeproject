using employeeproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace employeeproject.Controllers
{
    public class UserempController : Controller
    {
        employeeprojectContext emp;

        UserManager<Useremp> userMan;
        SignInManager<Useremp> signInMan;

        public UserempController(employeeprojectContext c, UserManager<Useremp> um, SignInManager<Useremp> signInMan)
        {
            emp = c;
            userMan = um;
            this.signInMan = signInMan;
        }
        [HttpGet]
        // GET: Useremp/Create
        public IActionResult Create()
        {
            return View(new Useremp());
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserId,Utype,UserName,DOB,Gender,HiredDate,Department,Salary,PasswordHash,Email")] Useremp u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            var result = await userMan.CreateAsync(u, u.PasswordHash);
            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }                
            }
            return View(u);
        }
        [HttpGet]
        public IActionResult Indexer()
        {
            return View(emp.Users.ToList());
        }
        [HttpGet]
        public IActionResult Details(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Useremp? e = (from f in emp.Users
                         where f.Id == Id
                         select f).FirstOrDefault();
            if (e == null)
            {
                return NotFound();
            }
            return View(e);
        }
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound("No id found");
            }
            Useremp? e = emp.Users.Find(Id);
            if (e == null)
            {
                throw new ArgumentException("No User found");
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Useremp e)
        {
            Useremp? eo = emp.Users.Find(e.Id);
            if (e == null)
            {
                return NotFound();
            }
            emp.Entry(eo).CurrentValues.SetValues(e);
            emp.SaveChanges();
            return RedirectToAction("Indexer");
        }
        [HttpGet]
        public IActionResult Delete(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Useremp e = emp.Users.Find(Id);
            if (e == null)
            {
                return NotFound();
            }
            emp.Users.Remove(e);
            emp.SaveChanges();
            return RedirectToAction("Indexer");
        }
        [HttpGet]
        public IActionResult ChangePw()
        {
            Useremp w = emp.Users.FirstOrDefault();
            if (w == null)
            {
                return NotFound("No User");
            }
            ChangePw cpw = new ChangePw();
            cpw.Id= w.Id;
            return View(cpw);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePW(ChangePw cpw)
        {
            if (!ModelState.IsValid)
            {
                return View(cpw);
            }
            //check if new password and confirm password match
            if (cpw.ConfirmPassword != cpw.NewPassword)
            {
                ModelState.AddModelError("NewPassword", "New Password and confirm password do not match");
                return View(cpw);
            }
            Useremp w = emp.Users.Where(x => x.Id == cpw.Id.ToString()).FirstOrDefault();
            if(w == null)
            {
                return NotFound("User does not exist");
            }

            //Check if old password matches
            var t = await userMan.CheckPasswordAsync(w, cpw.OldPassword);
            if ( t)
            {
                ModelState.AddModelError("OldPassword", "Incorrect Old Password");
                return View(cpw);
            }
            //update the password
            w.PasswordHash = cpw.NewPassword;
            //save the changes
            emp.Users.Update(w);
            emp.SaveChanges();
            return RedirectToAction("Indexer");
        }
        public IActionResult ForgotPass()
        {
            return View(new ForgotPassVM());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPass(ForgotPassVM fp)
        {
            if (!ModelState.IsValid)
            {
                return View(fp);
            }
            var user = await userMan.FindByEmailAsync(fp.Email);
            if (user == null)
            {
                return NotFound();
            }
            var token = await userMan.GeneratePasswordResetTokenAsync(user);
            var passWordResetLink = Url.Action("ResetPassword", "Userss", new { token, email = user.Email });
            ViewBag.PLink = passWordResetLink;
            //send email to user
            //show mail sent page
            return View("EmailLink");
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Indexer");
            }
             return View(new LoginVM());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM lvm)
        {
            if (!ModelState.IsValid)
            {
                return View(lvm);
            }
            var result = await signInMan.PasswordSignInAsync(lvm.UserName, lvm.Password, lvm.RememberMe, false);
            if (result.Succeeded)
            {
                TempData["cartitemms"] = 0;
                //some code here
                return RedirectToAction("Indexer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Credentials");
                return View(lvm);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await signInMan.SignOutAsync();
            return RedirectToAction("Create");
        }
    }
}


