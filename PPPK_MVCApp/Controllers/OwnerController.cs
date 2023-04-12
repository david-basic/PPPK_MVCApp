using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PPPK_MVCApp.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        ~OwnerController()
        {
            db?.Dispose();
        }

        // GET: Owner
        public ActionResult ListOwner()
        {
            return View(db.Owners);
        }

        // GET: Owner/Details/5
        public ActionResult DetailsOwner(int? id)
        {
            return CommonAction(id);
        }

        // GET: Owner/Create
        public ActionResult CreateOwner()
        {
            return View();
        }

        // POST: Owner/Create
        [HttpPost]
        public ActionResult CreateOwner(Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Owners.Add(owner);
                db.SaveChanges();
            }

            return RedirectToAction("ListOwner");
        }

        // GET: Owner/Edit/5
        public ActionResult EditOwner(int? id)
        {
            return CommonAction(id);
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult EditOwner(int id)
        {
            Owner owner = db.Owners.Find(id);

            if (TryUpdateModel(owner, "", new string[] {
                nameof(Owner.FirstName),
                nameof(Owner.LastName),
                nameof(Owner.Email)
            }))
            {
                db.Entry(owner).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListOwner");
            }

            return View(owner);
        }

        // GET: Owner/Delete/5
        public ActionResult DeleteOwner(int? id)
        {
            return CommonAction(id);
        }

        // POST: Owner/Delete/5
        [HttpPost]
        public ActionResult DeleteOwner(int id)
        {
            db.Owners.Remove(db.Owners.Find(id));
            db.SaveChanges();

            return RedirectToAction("ListOwner");
        }

        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Owner owner = db.Owners
                .SingleOrDefault(o => o.IDOwner == id);

            if (owner == null)
            {
                return HttpNotFound();
            }

            return View(owner);
        }
    }
}
