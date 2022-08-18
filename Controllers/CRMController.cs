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
        return View("NewBusinessForm");
    }


    [HttpPost("/business/create")]
    public IActionResult CreateBusiness(NewBusinessForm newBusiness)
    {
        if(ModelState.IsValid == false)
        {
            return NewBusiness();
        }

        Business business = new Business()
        {
            BusinessName = newBusiness.BusinessName,
            BusinessOwner = newBusiness.BusinessOwner,
            StartDate = newBusiness.StartDate,
            Industry = newBusiness.Industry
        };
        db.Businesses.Add(business);
        db.SaveChanges();
        Address address = new Address()
        {
            Street = newBusiness.Street,
            City = newBusiness.City,
            State = newBusiness.State,
            ZipCode = newBusiness.ZipCode,
            BusinessId = business.BusinessId
        };
        db.Addresses.Add(address);
        db.SaveChanges();
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