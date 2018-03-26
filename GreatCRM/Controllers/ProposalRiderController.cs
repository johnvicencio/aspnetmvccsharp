using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatCRM.Models;

namespace GreatCRM.Controllers
{
    public class ProposalRiderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProposalRider
        public ActionResult Index()
        {
            var proposalRiders = db.ProposalRiders.Include(p => p.Proposal).Include(p => p.Rider);
            return View(proposalRiders.ToList());
        }

        // GET: ProposalRider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalRider proposalRider = db.ProposalRiders.Find(id);
            if (proposalRider == null)
            {
                return HttpNotFound();
            }
            return View(proposalRider);
        }

        // GET: ProposalRider/Create
        public ActionResult Create()
        {
            ViewBag.ProposalId = new SelectList(db.Proposals, "ProposalId", "Insurance");
            ViewBag.RiderId = new SelectList(db.Riders, "RiderId", "RiderName");
            return View();
        }

        // POST: ProposalRider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProposalId,RiderId,Note")] ProposalRider proposalRider)
        {
            if (ModelState.IsValid)
            {
                db.ProposalRiders.Add(proposalRider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProposalId = new SelectList(db.Proposals, "ProposalId", "Insurance", proposalRider.ProposalId);
            ViewBag.RiderId = new SelectList(db.Riders, "RiderId", "RiderName", proposalRider.RiderId);
            return View(proposalRider);
        }

        // GET: ProposalRider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalRider proposalRider = db.ProposalRiders.Find(id);
            if (proposalRider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProposalId = new SelectList(db.Proposals, "ProposalId", "Insurance", proposalRider.ProposalId);
            ViewBag.RiderId = new SelectList(db.Riders, "RiderId", "RiderName", proposalRider.RiderId);
            return View(proposalRider);
        }

        // POST: ProposalRider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProposalId,RiderId,Note")] ProposalRider proposalRider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proposalRider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProposalId = new SelectList(db.Proposals, "ProposalId", "Insurance", proposalRider.ProposalId);
            ViewBag.RiderId = new SelectList(db.Riders, "RiderId", "RiderName", proposalRider.RiderId);
            return View(proposalRider);
        }

        // GET: ProposalRider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProposalRider proposalRider = db.ProposalRiders.Find(id);
            if (proposalRider == null)
            {
                return HttpNotFound();
            }
            return View(proposalRider);
        }

        // POST: ProposalRider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProposalRider proposalRider = db.ProposalRiders.Find(id);
            db.ProposalRiders.Remove(proposalRider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
