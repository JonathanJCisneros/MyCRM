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

    

    [HttpGet("/admin/dashboard")]
    public IActionResult AdminDashboard()
    {
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