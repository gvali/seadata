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
    public class ProjectsController : Controller
    {
        //private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;

        public ProjectsController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Projects
        public ActionResult Index(ProjectIndexViewModel vm)
        {
            //var projects = db.Projects.Include(p => p.ProjectLeader);
            //return System.Web.UI.WebControls.View(projects.ToList());
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;
            int totalProjectCount;

            //            var cruises = db.Cruises.Include(c => c.CruiseLeaders.);
            //            return System.Web.UI.WebControls.View(db.Cruises.ToList());
            // https://github.com/kpi-ua/X.PagedList
            //vm.Cruises = new StaticPagedList<Cruise>(res, vm.PageNumber.Value, vm.PageSize.Value, totalUserCount);
            List<Project> res;
            res = _uow.Projects.All;
            totalProjectCount = res.Count;
            vm.Projects = new StaticPagedList<Project>(res, vm.PageNumber.Value, vm.PageSize.Value, totalProjectCount);
            return View(vm);

        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Projects.Find(id);
            Project project = _uow.Projects.GetById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var vm = new ProjectCreateEditViewModel()
            {
                Project = project,
                LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All,nameof(Person.PersonId), nameof(Person.FirstLastname),project.ProjectLeaders.Select(person => person.PersonId)),
                PersonsMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname),project.ProjectPersons.Select(person => person.PersonId))
            };

            return View(vm);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            var vm = new ProjectCreateEditViewModel()
            {
                //                PublisherSelectList = new SelectList(db.Publishers, nameof(Publisher.PublisherId), nameof(Publisher.PublisherName)),
                LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname)),
                PersonsMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname))

            };
            return View(vm);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //db.Projects.Add(project);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                _uow.Projects.Add(vm.Project);
                foreach (var personId in vm.LeaderIds)
                {
                    _uow.ProjectLeaders.Add(new ProjectLeader()
                    {
                        Project = vm.Project,
                        PersonId = personId
                    });
                }
                foreach (var personId in vm.PersonIds)
                {
                    _uow.ProjectPersons.Add(new ProjectPerson()
                    {
                        Project = vm.Project,
                        PersonId = personId
                    });
                }
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            //vm.LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId),
            //    nameof(Person.FirstLastname), vm.LeaderIds);

            //            ViewBag.PersonId = new SelectList(db.Persons, "PersonId", "Firstname", project.PersonId);
            return View(vm);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _uow.Projects.GetById(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var vm = new ProjectCreateEditViewModel()
            {
                Project = project,
                LeadersMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname)),
                PersonsMultiSelectList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.FirstLastname))
            };


            //ViewBag.PersonId = new SelectList(db.Persons, "PersonId", "Firstname", project.PersonId);
            return View(vm);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProjectCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Projects.Update(vm.Project);
                var blist = _uow.ProjectLeaders.All.Where(b => b.ProjectId == vm.Project.ProjectId).ToList();
                foreach (var projectLeader in blist)
                {
                    _uow.ProjectLeaders.Delete(projectLeader);
                }
                _uow.Commit();

                foreach (var leaderId in vm.LeaderIds)
                {
                    _uow.ProjectLeaders.Add(new ProjectLeader()
                    {
                        Project = vm.Project,
                        PersonId = leaderId
                    });
                }
                _uow.Commit();
                var personlist = _uow.ProjectPersons.All.Where(b => b.ProjectId == vm.Project.ProjectId).ToList();

                foreach (var projectPerson in personlist)
                    {
                        _uow.ProjectPersons.Delete(projectPerson);
                    }
                _uow.Commit();

                foreach (var personId in vm.PersonIds)
                    {
                        _uow.ProjectPersons.Add(new ProjectPerson()
                        {
                            Project = vm.Project,
                            PersonId = personId
                        });

                    }
                _uow.Commit();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Projects.Find(id);
            Project project = _uow.Projects.GetById(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Project project = db.Projects.Find(id);
            //db.Projects.Remove(project);
            //db.SaveChanges();

            _uow.Cruises.Delete(id);
            _uow.Commit();
            return RedirectToAction(nameof(Index));
            return RedirectToAction("Index");
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
