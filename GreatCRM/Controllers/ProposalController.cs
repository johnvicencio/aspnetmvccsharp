using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatCRM.Models;
using GreatCRM.Models.Repositories;
using GreatCRM.Models.ViewModels;

namespace GreatCRM.Controllers
{
    public class ProposalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IRecordRepository<Proposal> repositoryProposal = null;
        private IRecordRepository<Client> repositoryClient = null;
        private IRecordRepository<Rider> repositoryRider = null;
        public ProposalController()
        {
            this.repositoryProposal = new RecordRepository<Proposal>();
            this.repositoryClient = new RecordRepository<Client>();
            this.repositoryRider = new RecordRepository<Rider>();
        }

        public ProposalController(IRecordRepository<Proposal> repositoryProposal, 
            IRecordRepository<Client> repositoryClient, IRecordRepository<Rider> repositoryRider
            )
        {
            this.repositoryProposal = repositoryProposal;
            this.repositoryClient = repositoryClient;
            this.repositoryRider = repositoryRider;
        }
        // GET: Proposal
        public ActionResult Index()
        {
            var viewModel = new RecordViewModels();
            viewModel.Proposals = repositoryProposal.GetAllIncludeClient();
            //var proposals = db.Proposals.Include(p => p.Client);
            return View(viewModel);
        }

        // GET: Proposal/Details/5
        public ActionResult Details(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Proposal proposal = db.Proposals.Find(id);
            viewModel.Proposal = repositoryProposal.Search(id);
            if (viewModel.Proposal == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Proposal/Create
        public ActionResult Create(int? clientId)
        {
            var viewModel = new RecordViewModels();
            viewModel.Proposals = repositoryProposal.GetAll();
            ViewBag.ClientId = new SelectList(repositoryClient.GetAll().Where(x => x.ClientId == clientId), "ClientId", "Name");
            ViewBag.RiderId = new MultiSelectList(db.Riders, "RiderId", "RiderName");
            return View(viewModel);
        }

        // POST: Proposal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProposalId,Insurance,ClientId")] Proposal proposal, int? clientId, int[] Riders)
        {
            var viewModel = new RecordViewModels();
            viewModel.Proposal = proposal;
            if (ModelState.IsValid)
            {
                if (Riders != null)
                {
                    foreach (var RiderId in Riders)
                    {
                        //var rider = repositoryRider.Search(RiderId);
                        // proposal.Riders.Add(rider);
                        repositoryProposal.InsertRider(proposal, RiderId);
                        //repositoryProposal.Save();
                    }
                    
                }
                repositoryProposal.InsertClient(proposal, (int)clientId);
                repositoryProposal.Save();
                //db.Proposals.Add(proposal);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(repositoryClient.GetAll(), "ClientId", "Name", proposal.ClientId);
            return View(viewModel);
        }

        // GET: Proposal/Edit/5
        public ActionResult Edit(int? id, int? clientId)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Proposal proposal = db.Proposals.Find(id);
            viewModel.Proposal = repositoryProposal.Search(id);
            if (viewModel.Proposal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(repositoryClient.GetAll(), "ClientId", "Name", viewModel.Proposal.ClientId);
            return View(viewModel);
        }

        // POST: Proposal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProposalId,Insurance,ClientId")] Proposal proposal, int? clientId)
        {
            var viewModel = new RecordViewModels();
            viewModel.Proposal = proposal;
            if (ModelState.IsValid)
            {
                viewModel.Proposal.ClientId = (int)clientId;
                repositoryProposal.Update(proposal);
                repositoryProposal.Save();
                //db.Entry(proposal).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(repositoryClient.GetAll(), "ClientId", "Name", proposal.ClientId);
            return View(viewModel);
        }

        // GET: Proposal/Delete/5
        public ActionResult Delete(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Proposal proposal = db.Proposals.Find(id);
            viewModel.Proposal = repositoryProposal.Search(id);
            if (viewModel.Proposal == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Proposal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var viewModel = new RecordViewModels();
            viewModel.Proposal = repositoryProposal.Search(id);
            repositoryProposal.Delete(id);
            repositoryProposal.Save();
            //Proposal proposal = db.Proposals.Find(id);
            //db.Proposals.Remove(proposal);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repositoryProposal.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
