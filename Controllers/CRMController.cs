using Microsoft.AspNetCore.Mvc;
using MyCRM.Models;
namespace MyCRM.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class CRMController : Controller
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

    public CRMController(MyCRMContext context)
    {
        db = context;
    }

    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }

        List<Business> UsersLeads = db.Businesses
            .Include(u => u.UsersWorkedWith)
            .ThenInclude(u => u.User)
            .Where(u => u.UsersWorkedWith.Any(u => u.UserId == uid))
            .ToList();
        return View("Dashboard", UsersLeads);
    }

    [HttpGet("/business/new")]
    public IActionResult NewBusiness()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }
        return View("NewBusiness");
    }


    [HttpPost("/business/create")]
    public IActionResult CreateBusiness(Business business, Address address)
    {
        if(ModelState.IsValid == false)
        {
            return NewBusiness();
        }
        return ViewOne(business.BusinessId);
    }

    [HttpGet("/client/{businessId}")]
    public IActionResult ViewOne(int businessId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }
        Business? OneBusiness = db.Businesses
            .Include(g => g.UsersWorkedWith.OrderBy(a => a.CreatedAt))
            .ThenInclude(g => g.User)
            .FirstOrDefault(e => e.BusinessId == businessId);
        if(OneBusiness == null)
        {
            return RedirectToAction("Dashboard");
        }
        ViewBag.OneBusiness = OneBusiness;
        return View("ViewOne");
    }

    [HttpGet("/delete/{businessId}")]
    public IActionResult DeleteOne(int businessId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }
        Business? oneBusiness = db.Businesses.FirstOrDefault(e => e.BusinessId == businessId);
        if(oneBusiness == null)
        {
            return RedirectToAction("Dashboard");
        }

        db.Businesses.Remove(oneBusiness);
        db.SaveChanges();
        return RedirectToAction("Dashboard");
    }
}