using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GreatCRM.Models.Repositories
{
    public partial class RecordRepositories : IRecordRepositories, IBrokerRepository
    {
        public ICollection<Broker> GetBrokers()
        {
            return db.Brokers.ToList();
        }

        public Broker FindBroker(int id)
        {
            return db.Brokers.Find(id);
        }

        public void Insert(Broker obj)
        {
            db.Brokers.Add(obj);
        }

        public void Update(Broker obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Broker broker = db.Brokers.Find(id);
            db.Brokers.Remove(broker);

        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}