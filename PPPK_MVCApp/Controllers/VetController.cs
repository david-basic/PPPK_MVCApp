using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace PPPK_MVCApp.Controllers
{
    public class VetController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        ~VetController()
        {
            db?.Dispose();
        }

        // GET: Vet
        public ActionResult ListVet()
        {
            return View(db.Veterinarians);
        }

        // GET: Vet/Details/5
        public ActionResult DetailsVet(int? id)
        {
            return CommonAction(id);
        }

        // GET: Vet/Create
        public ActionResult CreateVet()
        {
            return View();
        }

        // POST: Vet/Create
        [HttpPost]
        public ActionResult CreateVet(Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                db.Veterinarians.Add(veterinarian);
                db.SaveChanges();
            }

            return RedirectToAction("ListVet");
        }

        // GET: Vet/Edit/5
        public ActionResult EditVet(int? id)
        {
            return CommonAction(id);
        }

        // POST: Vet/Edit/5
        [HttpPost]
        public ActionResult EditVet(int id)
        {
            Veterinarian veterinarian = db.Veterinarians.Find(id);

            if (TryUpdateModel(veterinarian, "", new string[] {
                nameof(Veterinarian.FirstName),
                nameof(Veterinarian.LastName),
                nameof(Veterinarian.Email)
            }))
            {
                db.Entry(veterinarian).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListVet");
            }

            return View(veterinarian);
        }

        // GET: Vet/Delete/5
        public ActionResult DeleteVet(int? id)
        {
            return CommonAction(id);
        }

        // POST: Vet/Delete/5
        [HttpPost]
        public ActionResult DeleteVet(int id)
        {
            db.Veterinarians.Remove(db.Veterinarians.Find(id));
            db.SaveChanges();

            return RedirectToAction("ListVet");
        }

        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Veterinarian veterinarian = db.Veterinarians
                .SingleOrDefault(vet => vet.IDVeterinarian == id);

            if (veterinarian == null)
            {
                return HttpNotFound();
            }

            return View(veterinarian);
        }
    }
}
