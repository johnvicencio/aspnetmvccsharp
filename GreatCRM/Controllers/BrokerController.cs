using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatCRM.Models;
using GreatCRM.Models.ViewModels;
using GreatCRM.Models.Repositories;

namespace GreatCRM.Controllers
{
    public class BrokerController : Controller
    {
        private IRecordRepository<Broker> repositoryBroker = null;

        public BrokerController()
        {
            this.repositoryBroker = new RecordRepository<Broker>();
        }

        public BrokerController(IRecordRepository<Broker> repositoryBroker)
        {
            this.repositoryBroker = repositoryBroker;
        }



        // GET: Broker
        public ActionResult Index()
        {
            var viewModel = new RecordViewModels();
            viewModel.Brokers = repositoryBroker.GetAll();
            return View(viewModel);
        }
        // GET: Broker/Details/5
        public ActionResult Details(int? id)
        {
            var viewModel = new RecordViewModels();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModel.Broker = repositoryBroker.Search(id.GetValueOrDefault());
            if (viewModel.Broker == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Broker/Create
        public ActionResult Create()
        {
            var viewModel = new RecordViewModels();
            viewModel.Brokers = repositoryBroker.GetAll();
            return View(viewModel);
        }

        // POST: Broker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrokerId,Name,Age")] Broker broker)
        {
            var viewModel = new RecordViewModels();
            viewModel.Brokers = repositoryBroker.GetAll();
            if (ModelState.IsValid)
            {
                //db.Brokers.Add(broker);
                //db.SaveChanges();
                repositoryBroker.Insert(broker);
                repositoryBroker.Save();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Broker/Edit/5
        public ActionResult Edit(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Broker broker = db.Brokers.Find(id);
            viewModel.Broker = repositoryBroker.Search(id);
            if (viewModel.Broker == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Broker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrokerId,Name,Age")] Broker broker)
        {
            var viewModel = new RecordViewModels();
            viewModel.Broker = broker;
            if (ModelState.IsValid)
            {
                //db.Entry(broker).State = EntityState.Modified;
                //db.SaveChanges();
                repositoryBroker.Update(broker);
                repositoryBroker.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Broker/Delete/5
        public ActionResult Delete(int? id)
        {
            var viewModel = new RecordViewModels();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            viewModel.Broker = repositoryBroker.Search(id);
            if (viewModel.Broker == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Broker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var viewModel = new RecordViewModels();
            //Broker broker = db.Brokers.Find(id);
            viewModel.Broker = repositoryBroker.Search(id);
            //db.Brokers.Remove(broker);
            //db.SaveChanges();
            repositoryBroker.Delete(id);
            repositoryBroker.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repositoryBroker.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
