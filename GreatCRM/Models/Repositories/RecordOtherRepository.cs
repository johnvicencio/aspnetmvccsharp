using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatCRM.Models.Repositories
{
    public partial class RecordRepository<T> : IRecordRepository<T> where T : class
    {
        public ICollection<Client> GetAllIncludeBroker()
        {
            return db.Clients.Include(x => x.Broker).ToList();
        }

        public void InsertBroker(Client obj, int id)
        {
            var broker = db.Brokers.Find(id);
            broker.Clients.Add(obj);
        }
    }
}