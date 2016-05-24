using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain;
using Web.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Web.Controllers
{
    public class StationsController : Controller
    {
        private readonly IUOW _uow;

        public StationsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Stations
        public ActionResult Index(StationIndexViewModel vm)
        {
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;
            int totalStationsCount;

            List<Station> res;
            res = _uow.Stations.All;
            totalStationsCount = res.Count;
            vm.Stations = new StaticPagedList<Station>(res, vm.PageNumber.Value, vm.PageSize.Value, totalStationsCount);

            return View(vm);
        }

        // GET: Stations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _uow.Stations.GetById(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            var vm = new StationCreateEditViewModel();
            vm.StationTypeSelectList = new SelectList(_uow.StationTypes.All.Select(t => new { t.StationTypeId, StationTypeName = t.StationTypeName.Translate() }).ToList(), nameof(StationType.StationTypeId), nameof(StationType.StationTypeName));
            return View(vm);
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Stations.Add(vm.Station);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            vm.StationTypeSelectList = new SelectList(_uow.StationTypes.All.Select(t => new { t.StationTypeId, StationTypeName = t.StationTypeName.Translate() }).ToList(), nameof(StationType.StationTypeId), nameof(StationType.StationTypeName));
            return View(vm);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _uow.Stations.GetById(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            var vm = new StationCreateEditViewModel()
            {
                Station = station,
                StationTypeSelectList = new SelectList(_uow.StationTypes.All.Select(t => new { t.StationTypeId, StationTypeName = t.StationTypeName.Translate() }).ToList(), nameof(StationType.StationTypeId), nameof(StationType.StationTypeName))
            };

            return View(vm);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Stations.Update(vm.Station);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            vm.StationTypeSelectList = new SelectList(_uow.StationTypes.All.Select(t => new { t.StationTypeId, StationTypeName = t.StationTypeName.Translate() }).ToList(), nameof(StationType.StationTypeId), nameof(StationType.StationTypeName));
            return View(vm);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _uow.Stations.GetById(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Station station = _uow.Stations.GetById(id);
            _uow.Stations.Delete(station);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Stations.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
