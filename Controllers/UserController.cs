using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCRM.Models;
namespace MyCRM.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UserId");
        }
    }

    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }
    private MyCRMContext db;

    public UserController(MyCRMContext context)
    {
        db = context;
    }

    [HttpGet("/")]
    public IActionResult Home()
    {
        if(loggedIn)
        {
            return RedirectToAction("Dashboard", "CRM");
        }
        return View("Home");
    }

    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if(db.Users.Any(a => a.Email == newUser.Email))
        {
            ModelState.AddModelError("Email", "is taken");
        }

        if(ModelState.IsValid == false)
        {
            return Home();
        }

        PasswordHasher<User> hashBrowns = new PasswordHasher<User>();
        newUser.Password = hashBrowns.HashPassword(newUser, newUser.Password);

        db.Users.Add(newUser);
        db.SaveChanges();
        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        HttpContext.Session.SetString("Name", newUser.FirstName);
        return RedirectToAction("Dashboard", "CRM");
    }

    [HttpPost("/login")]
    public IActionResult Login(UserLogin user)
    {
        if(ModelState.IsValid == false)
        {
            return Home();
        }

        User? dbUser = db.Users.FirstOrDefault(a => a.Email == user.LoginEmail);

        if(dbUser == null)
        {
            ModelState.AddModelError("LoginEmail", "not found");
            return Home();
        } 

        PasswordHasher<UserLogin> hashBrowns = new PasswordHasher<UserLogin>();
        PasswordVerificationResult pwCheck = hashBrowns.VerifyHashedPassword(user, dbUser.Password, user.LoginPassword);

        if(pwCheck == 0)
        {
            ModelState.AddModelError("LoginPassword", "is invalid");
            return Home();
        }

        HttpContext.Session.SetInt32("UserId", dbUser.UserId);
        HttpContext.Session.SetString("Name", dbUser.FirstName);
        return RedirectToAction("Dashboard", "CRM");
    }

    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Home");
    }
}