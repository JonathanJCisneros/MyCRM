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
            .Include(s => s.StaffList)
            .Include(u => u.UsersWorkedWith.OrderBy(e => e.CreatedAt))
            .ThenInclude(u => u.User)
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
    public IActionResult CreateBusiness(NewBusinessForm newBusiness)
    {
        if(ModelState.IsValid == false)
        {
            return NewBusiness();
        }

        Business business = new Business()
        {
            BusinessName = newBusiness.BusinessName,
            Website = newBusiness.Website,
            StartDate = newBusiness.StartDate,
            Industry = newBusiness.Industry
        };
        db.Businesses.Add(business);
        db.SaveChanges();

        Staff contact = new Staff()
        {
            StaffType = newBusiness.StaffType,
            FirstName = newBusiness.FirstName,
            LastName = newBusiness.LastName,
            PhoneNumber = newBusiness.PhoneNumber,
            Email = newBusiness.Email,
            BusinessId = business.BusinessId
        };
        db.Staff.Add(contact);
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
        return RedirectToAction("ViewOne", new {businessId = business.BusinessId});
    }

    [HttpGet("/client/{businessId}")]
    public IActionResult ViewOne(int businessId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }
        Business? OneBusiness = db.Businesses
            .Include(s => s.StaffList)
            .Include(a => a.AddressList)
            .Include(a => a.PurchaseList)
                .ThenInclude(a => a.Product)
            .Include(n => n.SpecialNotes)
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

    [HttpPost("/activity/new/{businessId}")]
    public IActionResult AddActivity(int businessId, Activity newActivity)
    {
        if(ModelState.IsValid == false)
        {
            return ViewOne(businessId);
        }
        newActivity.BusinessId = businessId;
        newActivity.UserId = (int)uid;
        db.Activities.Add(newActivity);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }

    [HttpPost("/specialnote/new/{businessId}")]
    public IActionResult AddSpecialNote(int businessId, Note newNote)
    {
        if(ModelState.IsValid == false)
        {
            return ViewOne(businessId);
        }
        newNote.BusinessId = businessId;
        newNote.UserId = (int)uid;
        db.Notes.Add(newNote);
        db.SaveChanges();
        return RedirectToAction("ViewOne",new {businessId = businessId});
    }

    [HttpGet("/client/{businessId}/present")]
    public IActionResult Presentation(int businessId)
    {
        Business? oneBusiness = db.Businesses
            .Include(a => a.AddressList)
            .Include(a => a.PurchaseList)
                .ThenInclude(a => a.Product)
            .FirstOrDefault(b => b.BusinessId == businessId);
        ViewBag.oneBusiness = oneBusiness;

        List<Product> allProducts = db.Products.ToList();
        ViewBag.Products = allProducts;
        return View("Presentation");
    }

    [HttpPost("/purchase/{businessId}")]
    public IActionResult AddPurchase(int businessId, Purchase newPurchase)
    {
        if(ModelState.IsValid == false)
        {
            return Presentation(businessId);
        }
        newPurchase.BusinessId = businessId;
        db.Purchases.Add(newPurchase);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }

    [HttpPost("/client/bname/update/{businessId}")]
    public IActionResult UpdateBusinessName(int businessId, Business updateBusiness)
    {
        Business? business = db.Businesses.FirstOrDefault(b => b.BusinessId == businessId);

        if(business == null)
        {
            RedirectToAction("Dashboard");
        }

        business.BusinessName = updateBusiness.BusinessName;

        db.Businesses.Update(business);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = business.BusinessId});
    }
    [HttpPost("/client/sd/update/{businessId}")]
    public IActionResult UpdateBusinessStartDate(int businessId, Business updateBusiness)
    {
        Business? business = db.Businesses.FirstOrDefault(b => b.BusinessId == businessId);

        if(business == null)
        {
            RedirectToAction("Dashboard");
        }

        business.StartDate = updateBusiness.StartDate;

        db.Businesses.Update(business);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = business.BusinessId});
    }

    [HttpPost("/client/i/update/{businessId}")]
    public IActionResult UpdateBusinessIndustry(int businessId, Business updateBusiness)
    {
        Business? business = db.Businesses.FirstOrDefault(b => b.BusinessId == businessId);

        if(business == null)
        {
            RedirectToAction("Dashboard");
        }

        business.Industry = updateBusiness.Industry;

        db.Businesses.Update(business);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = business.BusinessId});
    }

    [HttpPost("/client/{businessId}/address/add")]
    public IActionResult AddAddress(int businessId, Address newAddress)
    {
        newAddress.BusinessId = businessId;

        db.Addresses.Add(newAddress);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }

    [HttpGet("/client/{businessId}/address/{addressId}/delete")]
    public IActionResult DeleteLocation(int businessId, int addressId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Home", "User");
        }
        Address? oneLocation = db.Addresses.FirstOrDefault(e => e.AddressId == addressId);
        if(oneLocation == null)
        {
            return RedirectToAction("Dashboard");
        }

        db.Addresses.Remove(oneLocation);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }

    [HttpPost("/client/{businessId}/staff/add/")]
    public IActionResult NewStaff(int businessId, Staff newStaff)
    {
        newStaff.BusinessId = businessId;

        db.Staff.Add(newStaff);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }

    [HttpPost("/client/{businessId}/website/edit")]
    public IActionResult EditWebsite(int businessId, Business editWebsite)
    {
        Business? business = db.Businesses.FirstOrDefault(b => b.BusinessId == businessId);

        business.Website = editWebsite.Website;

        db.Businesses.Update(business);
        db.SaveChanges();
        return RedirectToAction("ViewOne", new {businessId = businessId});
    }
}