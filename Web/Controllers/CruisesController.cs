using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;
using PagedList;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CruisesController : Controller
    {
        //        private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;

        public CruisesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Cruises
        public ActionResult Index(CruiseIndexViewModel vm)
        {
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            //            var cruises = db.Cruises.Include(c => c.CruiseLeaders.);
            //            return System.Web.UI.WebControls.View(db.Cruises.ToList());
            // https://github.com/kpi-ua/X.PagedList
            //vm.Cruises = new StaticPagedList<Cruise>(res, vm.PageNumber.Value, vm.PageSize.Value, totalUserCount);
            List<Cruise> res;
            res = _uow.Cruises.All;
            int totalCruiseCount = res.Count;
            vm.Cruises  = new StaticPagedList<Cruise>(res, vm.PageNumber.Value, vm.PageSize.Value, totalCruiseCount);
            return View(vm);
        }

        // GET: Cruises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Cruise cruise = db.Cruises.Find(id);
            Cruise cruise = _uow.Cruises.GetById(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            var vm = new CruiseCreateEditViewModel()
            {
                Cruise = cruise,
                LeadersMultiSelectList = new MultiSelectList(
                    _uow.Persons.All,
                    nameof(Person.PersonId), nameof(Person.FirstLastname),
                    cruise.CruiseLeaders.Select(person => person.PersonId)),
                PersonsMultiSelectList = new MultiSelectList(
                    _uow.Persons.All,
                    nameof(Person.PersonId), nameof(Person.FirstLastname),
                    cruise.CruisePersons.Select(person => person.PersonId)),
            };

            return View(vm);
        }

        // GET: Cruises/Create
        public ActionResult Create()
        {
            var vm = new CruiseCreateEditViewModel()
            {
//                PublisherSelectList = new SelectList(db.Publishers, nameof(Publisher.PublisherId), nameof(Publisher.PublisherName)),
                LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname)),
                PersonsMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname))
            };

            return View(vm);
        }

        // POST: Cruises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CruiseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {

                _uow.Cruises.Add(vm.Cruise);
            
                foreach (var personId in vm.LeaderIds)
                {
                    _uow.CruiseLeaders.Add(new CruiseLeader()
                    {
                        Cruise = vm.Cruise,
                        PersonId = personId
                    });
                }

                foreach (var personId in vm.PersonIds)
                {
                    _uow.CruisePersons.Add(new CruisePerson()
                    {
                        Cruise = vm.Cruise,
                        PersonId = personId
                    });
                }

                _uow.Commit();
                return RedirectToAction(nameof(Index));

                //vm.LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId),
                //    nameof(Person.FirstLastname), vm.LeaderIds);

                //db.Cruises.Add(cruise);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Cruises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Cruise cruise = db.Cruises.Find(id);
            var cruise = _uow.Cruises.GetById(id);

            if (cruise == null)
            {
                return HttpNotFound();
            }
            var vm = new CruiseCreateEditViewModel()
            {
                Cruise = cruise,
                LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All,nameof(Person.PersonId),nameof(Person.FirstLastname),cruise.CruiseLeaders.Select(person => person.PersonId)),
                PersonsMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname), cruise.CruiseLeaders.Select(person => person.PersonId))
            };

            return View(vm);
        }

        // POST: Cruises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CruiseCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {

                _uow.Cruises.Update(vm.Cruise);
                _uow.Commit();

                var blist = _uow.CruiseLeaders.All.Where(b => b.CruiseId == vm.Cruise.CruiseId).ToList();
                foreach (var cruiseLeader in blist)
                {
                    _uow.CruiseLeaders.Delete(cruiseLeader);
                }
                _uow.Commit();

                foreach (var leaderId in vm.LeaderIds)
                {
                    _uow.CruiseLeaders.Add(new CruiseLeader()
                    {
                        Cruise = vm.Cruise,
                        PersonId = leaderId
                    });
                }
                _uow.Commit();
                var personlist = _uow.CruisePersons.All.Where(b => b.CruiseId == vm.Cruise.CruiseId).ToList();

                foreach (var cruisePerson in personlist)
                {
                    _uow.CruisePersons.Delete(cruisePerson);
                }
                _uow.Commit();

                foreach (var personId in vm.PersonIds)
                {
                    _uow.CruisePersons.Add(new CruisePerson()
                    {
                        Cruise = vm.Cruise,
                        PersonId = personId
                    });

                }
                _uow.Commit();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Cruises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Cruise cruise = db.Cruises.Find(id);
            Cruise cruise = _uow.Cruises.GetById(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: Cruises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Cruise cruise = db.Cruises.Find(id);

            //db.Cruises.Remove(cruise);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            _uow.Cruises.Delete(id);
            _uow.Commit();
            return RedirectToAction(nameof(Index));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
