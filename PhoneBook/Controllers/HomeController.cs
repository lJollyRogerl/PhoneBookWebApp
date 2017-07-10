using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        //showing the list of the contacts
        [HttpGet]
        public ActionResult Index(int page = 1, string successMessage = "")
        {
            if (!string.IsNullOrEmpty(successMessage))
                ViewData["successMessage"] = successMessage;
            PhoneBookDbContext db = new PhoneBookDbContext();
            return View(db.spGetContacts(page).ToList());
        }

        //verificating and posting a new contact details to the db
        [HttpPost]
        public ActionResult Index()
        {
            PhoneBookDbContext db = new PhoneBookDbContext();
            Citizen userAddingCitizen = new Citizen();
            TryUpdateModel(userAddingCitizen);
            //If user entered the both values and they are valid
            if (ModelState.IsValid)
            {
                //pulling entity from db with parameters gaved by user
                Citizen citizenFromDbByName = db.tblCitizens.FirstOrDefault(o => o.Name == userAddingCitizen.Name);
                Citizen citizenFromDbByPhone = db.tblCitizens.FirstOrDefault(o => o.PhoneNumber == userAddingCitizen.PhoneNumber);
                //if the same details already exists in db - adding relative messages
                //and returning to the form
                if (citizenFromDbByName != null)
                {
                    ModelState.AddModelError(string.Empty, citizenFromDbByName.Name + " уже есть в справочнике.");
                }
                if (citizenFromDbByPhone != null)
                { 
                    ModelState.AddModelError(string.Empty, "Введенный номер уже назначен контакту с именем: " + citizenFromDbByPhone.Name);
                }
                if ((citizenFromDbByName != null) || (citizenFromDbByPhone != null))
                    return View(db.spGetContacts(1).ToList());

                //if all entered data is ok - push it to the db
                else
                {
                    db.tblCitizens.Add(userAddingCitizen);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", 
                        new { successMessage = userAddingCitizen.Name + " был добавлен в список" });
                }
            }
            //if something wrong with entered data - return to the same form and throw the error message
            else
            {
                return View(db.spGetContacts(1).ToList());
            }


        }

        public PartialViewResult GetCitizensByString(string txtSearch)
        {
            txtSearch = txtSearch.Trim();
            PhoneBookDbContext db = new PhoneBookDbContext();
            //if string is null or empty - show the first page of the contacts from db
            if (string.IsNullOrEmpty(txtSearch))
            {
                return PartialView("_Citizen", db.spGetContacts(1).ToList());
            }
            else
            {
                List<Citizen> citizens = new List<Citizen>();
                citizens = db.tblCitizens.Where(c => c.Name.Contains(txtSearch)).OrderBy(o=>o.Name).ToList();
                if (citizens.Count > 0)
                    //if any contact mathes - show it through the partial view
                    return PartialView("_Citizen", citizens);
                else
                    //Show a message to user that there isn't any contacts match through a partial view
                    return PartialView("_SomethingWrong", txtSearch);
            }
    }

        //gets a page of contact details from db by number
        public PartialViewResult GetPageOfCitizens(int page = 1)
        {
            PhoneBookDbContext db = new PhoneBookDbContext();
            return PartialView("_Citizen", db.spGetContacts(page).ToList());
        }

        //delete a contact with id, given as a parameter
        public ActionResult DeleteContact(int citizenToDelId)
        {
            PhoneBookDbContext db = new PhoneBookDbContext();
            Citizen citizenToDelete = db.tblCitizens.Single(c => c.CitizenId == citizenToDelId);
            if (citizenToDelete != null)
            {
                db.tblCitizens.Remove(citizenToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}