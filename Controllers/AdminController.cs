using Microsoft.AspNetCore.Mvc;
using MyCRM.Models;
namespace MyCRM.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("AdminId");
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

    public AdminController(MyCRMContext context)
    {
        db = context;
    }

    [HttpGet("/admin")]
    public IActionResult Admin()
    {
        if(loggedIn)
        {
            return AdminDashboard();
        }
        return View("AdminLoginOrRegister");
    }

    [HttpPost("/admin/register")]
    public IActionResult AdminRegister(Admin newAdmin)
    {
        if(db.Administrators.Any(a => a.Email == newAdmin.Email))
        {
            ModelState.AddModelError("Email", "is taken");
        }

        if(ModelState.IsValid == false)
        {
            return Admin();
        }

        PasswordHasher<Admin> hashBrowns = new PasswordHasher<Admin>();
        newAdmin.Password = hashBrowns.HashPassword(newAdmin, newAdmin.Password);

        db.Administrators.Add(newAdmin);
        db.SaveChanges();
        HttpContext.Session.SetInt32("AdminId", newAdmin.AdminId);
        HttpContext.Session.SetString("Name", newAdmin.FirstName);
        return RedirectToAction("AdminDashboard");
    }

    [HttpPost("/admin/login")]
    public IActionResult AdminLogin(AdminLogin admin)
    {
        if(ModelState.IsValid == false)
        {
            return Admin();
        }

        Admin? dbAdmin = db.Administrators.FirstOrDefault(a => a.Email == admin.LoginEmail);

        if(dbAdmin == null)
        {
            ModelState.AddModelError("LoginEmail", "not found");
            return Admin();
        } 

        PasswordHasher<AdminLogin> hashBrowns = new PasswordHasher<AdminLogin>();
        PasswordVerificationResult pwCheck = hashBrowns.VerifyHashedPassword(admin, dbAdmin.Password, admin.LoginPassword);

        if(pwCheck == 0)
        {
            ModelState.AddModelError("LoginPassword", "is invalid");
            return Admin();
        }

        HttpContext.Session.SetInt32("AdminId", dbAdmin.AdminId);
        HttpContext.Session.SetString("Name", dbAdmin.FirstName);
        return RedirectToAction("AdminDashboard");
    }

    [HttpGet("/admin/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Admin");
    }

    [HttpGet("/admin/dashboard")]
    public IActionResult AdminDashboard()
    {
        if(!loggedIn)
        {
            return Admin();
        }
        return View("AdminDashboard");
    }    

    [HttpPost("/product/new")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if(ModelState.IsValid == false)
        {
            return AdminDashboard();
        }
        db.Products.Add(newProduct);
        db.SaveChanges();
        return AdminDashboard();
    }
}