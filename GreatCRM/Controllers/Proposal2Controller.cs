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
    public class Proposal2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proposal2
        public ActionResult Index()
        {
            var proposals = db.Proposals.Include(p => p.Client);
            return View(proposals.ToList());
        }

        // GET: Proposal2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposal proposal = db.Proposals.Find(id);
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        // GET: Proposal2/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            ViewBag.RiderId = new MultiSelectList(db.Riders, "RiderId", "RiderName");
            return View();
        }

        // POST: Proposal2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProposalId,Insurance,ClientId")] Proposal proposal, int[] Riders)
        {
            if (ModelState.IsValid)
            {
                if (Riders != null)
                {
                    foreach (var RiderId in Riders)
                    {
                        Rider rider = db.Riders.Find(RiderId);
                        proposal.Riders.Add(rider);
                    }
                }
                db.Proposals.Add(proposal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", proposal.ClientId);
            
            return View(proposal);
        }

        // GET: Proposal2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposal proposal = db.Proposals.Find(id);
            if (proposal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", proposal.ClientId );
            ViewBag.RiderId = new MultiSelectList(db.Riders, "RiderId", "RiderName", proposal.Riders);
            return View(proposal);
        }

        // POST: Proposal2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProposalId,Insurance,ClientId")] Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proposal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", proposal.ClientId);
            return View(proposal);
        }

        // GET: Proposal2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposal proposal = db.Proposals.Find(id);
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        // POST: Proposal2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proposal proposal = db.Proposals.Find(id);
            db.Proposals.Remove(proposal);
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
