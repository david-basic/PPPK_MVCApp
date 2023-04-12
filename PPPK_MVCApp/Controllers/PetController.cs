using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PPPK_MVCApp.Controllers
{
    public class PetController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        ~PetController()
        {
            db?.Dispose();
        }

        // GET: Pet
        public ActionResult ListPet()
        {
            return View(db.Pets);
        }

        // GET: Pet/Details/5
        public ActionResult DetailsPet(int? id)
        {
            return CommonAction(id);
        }

        // GET: Pet/Create
        public ActionResult CreatePet()
        {
            return View();
        }

        // POST: Pet/Create
        [HttpPost]
        public ActionResult CreatePet(Pet pet, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                pet.UploadedFiles = new List<UploadedFile>();
                AddFiles(pet, files);

                db.Pets.Add(pet);
                db.SaveChanges();
            }

            return RedirectToAction("ListPet");
        }

        // GET: Pet/Edit/5
        public ActionResult EditPet(int? id)
        {
            return CommonAction(id);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        public ActionResult EditPet(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Pet pet = db.Pets.Find(id);

            if (TryUpdateModel(pet, "", new string[] {
                nameof(Pet.PetName),
                nameof(Pet.Species),
                nameof(Pet.Age),
                nameof(Pet.VeterinarianIDVeterinarian),
                nameof(Pet.OwnerIDOwner),
            }))
            {
                AddFiles(pet, files);

                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListPet");
            }

            return View(pet);
        }

        // GET: Pet/Delete/5
        public ActionResult DeletePet(int? id)
        {
            return CommonAction(id);
        }

        // POST: Pet/Delete/5
        [HttpPost]
        public ActionResult DeletePet(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.PetIDPet == id));
            db.Pets.Remove(db.Pets.Find(id));
            db.SaveChanges();

            return RedirectToAction("ListPet");
        }

        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pet pet = db.Pets
                .Include(p => p.UploadedFiles)
                .SingleOrDefault(p => p.IDPet == id);

            if (pet == null)
            {
                return HttpNotFound();
            }

            return View(pet);
        }

        private void AddFiles(Pet pet, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var picture = new UploadedFile
                    {
                        Name = file.FileName,
                        ContentType = file.ContentType
                    };
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        picture.Content = reader.ReadBytes(file.ContentLength);
                    }
                    pet.UploadedFiles.Add(picture);
                }
            }
        }
    }
}
