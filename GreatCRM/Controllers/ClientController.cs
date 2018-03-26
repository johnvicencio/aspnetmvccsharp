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
    public class ClientController : Controller
    {
     
        private IRecordRepository<Client> repositoryClient = null;
        private IRecordRepository<Broker> repositoryBroker = null;
        public ClientController()
        {
            this.repositoryClient = new RecordRepository<Client>();
            this.repositoryBroker = new RecordRepository<Broker>();
        }

        public ClientController(IRecordRepository<Client> repositoryClient, IRecordRepository<Broker> repositoryBroker)
        {
            this.repositoryClient = repositoryClient;
            this.repositoryBroker = repositoryBroker;
        }

        // GET: Client
        public ActionResult Index()
        {
            var viewModel = new RecordViewModels();
            viewModel.Clients = repositoryClient.GetAllIncludeBroker();
            //var clients = db.Clients.Include(c => c.Broker);
            return View(viewModel);
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Client client = db.Clients.Find(id);
            viewModel.Client = repositoryClient.Search(id);
            if (viewModel.Client == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Client/Create
        public ActionResult Create(int? brokerId)
        {
            var viewModel = new RecordViewModels();
            viewModel.Clients = repositoryClient.GetAll();
            ViewBag.BrokerId = new SelectList(repositoryBroker.GetAll().Where(x => x.BrokerId == brokerId), "BrokerId", "Name");
            return View(viewModel);
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Name,BrokerId")] Client client, int? brokerId)
        {
            var viewModel = new RecordViewModels();
            viewModel.Client = client;
            //var broker = repositoryBroker.Search(brokerId);
            if (ModelState.IsValid)
            {
                //broker.Clients.Add(client);
                repositoryClient.InsertBroker(client, (int)brokerId);
                //repositoryClient.Insert(client);
                repositoryClient.Save();
                return RedirectToAction("Index");
            }

            ViewBag.BrokerId = new SelectList(repositoryBroker.GetAll(), "BrokerId", "Name", client.BrokerId);
            return View(viewModel);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id, int? brokerId)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Clients.Find(id);
            viewModel.Client = repositoryClient.Search(id);
            if (viewModel.Client == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrokerId = new SelectList(repositoryBroker.GetAll().Where(x => x.BrokerId == brokerId), "BrokerId", "Name", viewModel.Client.BrokerId);
            return View(viewModel);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Name,BrokerId")] Client client, int? brokerId)
        {
            var viewModel = new RecordViewModels();
            viewModel.Client = client;
            if (ModelState.IsValid)
            {
                //repositoryClient.InsertBroker(client, (int)brokerId);
                viewModel.Client.BrokerId = (int)brokerId;
                repositoryClient.Update(client);
                //db.Entry(client).State = EntityState.Modified;
                //db.SaveChanges();
                repositoryClient.Save();
                return RedirectToAction("Index");
            }
            ViewBag.BrokerId = new SelectList(repositoryBroker.GetAll(), "BrokerId", "Name", client.BrokerId);
            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Clients.Find(id);
            viewModel.Client = repositoryClient.Search(id);
            if (viewModel.Client == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var viewModel = new RecordViewModels();
            viewModel.Client = repositoryClient.Search(id);
            repositoryClient.Delete(id);
            repositoryClient.Save();
            //Client client = db.Clients.Find(id);

            //db.Clients.Remove(client);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repositoryClient.Dispose();
               // db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
