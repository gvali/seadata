﻿using System;
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
using Microsoft.AspNet.Identity;
using PagedList;
using Web.ViewModels;


namespace Web.Controllers
{
    [Authorize]
    public class PersonsController : BaseController
    {
        private readonly IUOW _uow;

        public PersonsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Persons
        public ActionResult Index(PersonIndexViewModel vm)
        {
            int totalUserCount;
            string realSortProperty;

            // if not set, set base values
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            List<Person> res;
            if (vm.FilterByDTBoolean)
            {
                res = _uow.Persons.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.FilterFromDT,
                    vm.FilterToDT, vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalUserCount,
                    out realSortProperty);
            }
            else
            {
                res = _uow.Persons.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.SortProperty,
                    vm.PageNumber.Value - 1, vm.PageSize.Value, out totalUserCount, out realSortProperty);

                vm.FilterFromDT = vm.FilterFromDT ?? DateTime.Now.Subtract(TimeSpan.FromDays(5 * 365));
                vm.FilterToDT = vm.FilterToDT ?? DateTime.Now;
            }

            vm.SortProperty = realSortProperty;

            // https://github.com/kpi-ua/X.PagedList
            vm.Persons = new StaticPagedList<Person>(res, vm.PageNumber.Value, vm.PageSize.Value, totalUserCount);

            return View(vm);
        }

        // GET: Persons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _uow.Persons.GetById(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                // do not get user id from html get/post!!!!
                person.UserId = User.Identity.GetUserId<int>();

                _uow.Persons.Add(person);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        public ActionResult AddContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = _uow.Contacts.All.Where(b => b.PersonId == id.Value).FirstOrDefault();
            //var contact = _uow.Contacts.GetForUser(id.Value, User.Identity.GetUserId<int>());
            if (contact == null)
            {
                return HttpNotFound();
            }
            var vm = new ContactCreateEditViewModel();
            vm.Contact = contact;
            vm.ContactTypeSelectList = new SelectList(_uow.ContactTypes.All.Select(t => new { t.ContactTypeId, ContactTypeName = t.ContactTypeName.Translate() }).ToList(), nameof(ContactType.ContactTypeId), nameof(ContactType.ContactTypeName));
            vm.PersonSelectList = new SelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContact(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //vm.Comment.CreatedAt = DateTime.Now;
                //vm.Comment.AuthorId = vm.Author.AuthorId;
                //db.Comments.Add(vm.Comment);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            //vm.Person = db.Authors.Find(vm.Author.AuthorId);
            return View(vm);
        }


        // GET: Persons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // check user id!!!!
            Person person = _uow.Persons.GetForUser(id.Value, User.Identity.GetUserId<int>());
            if (person == null)
            {
                return HttpNotFound();
            }



            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                // do not get user id from html get/post!!!!
                person.UserId = User.Identity.GetUserId<int>();

                _uow.Persons.Update(person);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _uow.Persons.GetForUser(id.Value, User.Identity.GetUserId<int>());
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Persons.Delete(id);
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

