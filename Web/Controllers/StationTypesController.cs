using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

using DAL;
using DAL.Interfaces;
using Domain;

namespace Web.Controllers
{
    public class StationTypesController : Controller
    {
        private readonly IUOW _uow;

        public StationTypesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Stations

        // GET: StationTypes
        public ActionResult Index(StationTypeIndexViewModel vm)
        {
            //var stationTypes = db.StationTypes.Include(s => s.StationTypeName);
            //return System.Web.UI.WebControls.View(stationTypes.ToList());
            vm.StationTypes = _uow.StationTypes.All;
            return View(vm);
        }

        // GET: StationTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationType stationType = _uow.StationTypes.GetById(id);
            if (stationType == null)
            {
                return HttpNotFound();
            }
            return View(stationType);
        }

        // GET: StationTypes/Create
        public ActionResult Create()
        {
            var vm = new StationTypeCreateEditViewModel();

            return View(vm);
        }

        // POST: StationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StationTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.StationTypes.Add(vm.StationType);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            //ViewBag.StationTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", stationType.StationTypeNameId);
            return View(vm);
        }

        // GET: StationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationType stationType = _uow.StationTypes.GetById(id);
            if (stationType == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StationTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", stationType.StationTypeNameId);
            return View(stationType);
        }

        // POST: StationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StationType stationType)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(stationType).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.StationTypeNameId = new SelectList(db.MultiLangStrings, "MultiLangStringId", "Value", stationType.StationTypeNameId);
            return View(stationType);
        }

        // GET: StationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationType stationType = _uow.StationTypes.GetById(id);
            if (stationType == null)
            {
                return HttpNotFound();
            }
            return View(stationType);
        }

        // POST: StationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StationType stationType = _uow.StationTypes.GetById(id);
            _uow.StationTypes.Delete(stationType);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.StationTypes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
