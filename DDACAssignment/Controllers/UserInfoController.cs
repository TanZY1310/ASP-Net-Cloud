using DDACAssignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DDACAssignment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DDACAssignment.Controllers
{
    public class UserInfoController : Controller
    {
        private UserManager<DDACAssignmentUser> userManager;

        public UserInfoController(UserManager<DDACAssignmentUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public IActionResult Index(string msg="")
        {
            ViewBag.msg = msg;
            return View(userManager.Users);
        }

        public IActionResult registerUser()
        {
            //ViewBag.msg = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registerUser(register user)
        {
            if (ModelState.IsValid)
            {
                DDACAssignmentUser webUser = new DDACAssignmentUser { Name = user.Name, UserName= user.Email, Email = user.Email, Role = user.Role, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(webUser, user.Password);

                if (result.Succeeded)
                {
                    //ViewBag.msg = "New User Added!";   //User Validation not completed yet
                    return RedirectToAction("Index", "UserInfo", new { msg = "User Created!" });
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }  
            }

            return View(user);
        }

        public async Task<IActionResult> UpdateUser (String id)
        {
            DDACAssignmentUser user = await userManager.FindByIdAsync(id);

            Boolean a = false; Boolean b = false; Boolean c = false; Boolean d = false; Boolean e = false;
            if (user.Role == "Admin")
            {
                a = true;
            }
            else if (user.Role == "Artist Manager")
            {
                b = true;
            }
            else if (user.Role == "Composer")
            {
                c = true;
            }
            else if (user.Role == "Produer")
            {
                d = true;
            }
            else
            {
                e = true;
            }
            ViewBag.users = new List<SelectListItem>
            {
                new SelectListItem {Selected=e, Text="Select Option", Value=""},
                new SelectListItem {Selected=a, Text="Admin", Value="Admin"},
                new SelectListItem {Selected=b, Text="Artist Manager", Value="Artist Manager"},
                new SelectListItem {Selected=c, Text="Composer", Value="Composer"},
                new SelectListItem {Selected=d, Text="Producer", Value="Producer"}
            };
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");  //To add in validation
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser (String id, string name, string email, String address, string role)
        {
            DDACAssignmentUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                user.Address = address;
                user.Role = role;

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", new { msg = "User Updated!" });  //To add in validation
                else
                    return View(user);
            }
            else
                ModelState.AddModelError("", "User Not Found!");

            return View(user);
        }

        public async Task <IActionResult> DeleteUser (string id)
        {
            DDACAssignmentUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            else
            {
                return RedirectToAction("Index", "UserInfo", new { msg = "Unable to delete user!" });
            }
            return RedirectToAction("Index", "UserInfo", new { msg = "User Deleted!" });
        }

    }
}
