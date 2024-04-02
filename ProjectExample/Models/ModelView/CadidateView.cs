using Antlr.Runtime.Tree;
using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class CadidateView
    {
        private readonly CadidateRepository<Cadidate> repository;
        public CadidateView()
        {
            repository = new CadidateRepository<Cadidate>();
            
        }
        public IEnumerable<Cadidate> GetAllCandidate()
        {
            return repository.GetAllValue();
        }
        public void InsertCadidate(Cadidate cadidate)
        {
            try
            {
                if(cadidate != null)
                {
                    repository.AddInfo(cadidate);
                }
                else
                {
                    throw new ArgumentException("Null Data!!");
                }
            }
            catch (Exception e)
            {

            }
        }
        public IEnumerable<Cadidate> GetByIDCadidate(int id_vanacys)
        {
            try
            {
                List<Cadidate> candidate = repository.GetById(id_vanacys);

                if (candidate != null)
                {
                    return candidate;
                }
                else
                {
                    return Enumerable.Empty<Cadidate>();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Cadidate>();
            }
        }
     
        public Cadidate GetByCodeCadidate(string code)
        {
            try
            {
                if (code != null)
                {
                    return repository.GetByIdCode(code);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public Cadidate GetByIDCadidatestatus(int id)
        {
            try
            {
                if (id != null)
                {
                    return repository.GetByIdStatus(id);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public void UpdateStatusCadi(Cadidate cadidate,int id_cadi)
        {
            try
            {
                if (id_cadi != null)
                {
                    repository.Update(cadidate,id_cadi);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}