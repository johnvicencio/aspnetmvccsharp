using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GreatCRM.Models.Repositories
{
    public class RecordRepository<T> : IRecordRepository<T> where T : class
    {
        private ApplicationDbContext db = null;
        private DbSet<T> table = null;

        public RecordRepository()
        {
            this.db = new ApplicationDbContext();
            table = db.Set<T>();
        }
        public RecordRepository(ApplicationDbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }


        public ICollection<T> GetAll()
        {
            return table.ToList();
        }

        public T Search(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        //Other Model Specific Functions


        //ALL INCLUDES
        public ICollection<Client> GetAllIncludeBroker()
        {
            return db.Clients.Include(x => x.Broker).ToList();
        }
        public ICollection<Proposal> GetAllIncludeClient()
        {
            return db.Proposals.Include(x => x.Client).ToList();
        }
        //public ICollection<Proposal> GetAllIncludeRider()
        //{
        //    return db.Proposals.Include(y => y.Riders).ToList();
        //}
        //END ALL INCLUDES

        //ALL INSERT OTHER ENTITIES 
        public void InsertBroker(Client obj, int id)
        {
            var broker = db.Brokers.Find(id);
            broker.Clients.Add(obj);
        }

        public void InsertClient(Proposal obj, int id)
        {
            var client = db.Clients.Find(id);
            client.Proposals.Add(obj);
        }

        public void InsertRider(Proposal obj, int id)
        {
            var rider = db.Riders.Find(id);
            rider.Proposals.Add(obj);
        }

        // END



    }
}