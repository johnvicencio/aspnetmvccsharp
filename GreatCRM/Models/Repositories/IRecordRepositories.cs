using GreatCRM.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace GreatCRM.Models.Repositories
{
    public interface IRecordRepository<T> where T : class
    {
        ICollection<T> GetAll();
        T Search(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        void Dispose();


        //MODEL SPECIFIC
        ICollection<Client> GetAllIncludeBroker(); // for client
        ICollection<Proposal> GetAllIncludeClient(); // for proposal
        //ICollection<Proposal> GetAllIncludeRider(); // for proposal with riders

        void InsertBroker(Client obj, int id); // for client broker key
        void InsertClient(Proposal obj, int id); // for proposal client key
    }
}