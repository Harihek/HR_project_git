using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class VanacyView
    {
        private readonly VanacyRepository<Vancacy> repository;
        public VanacyView()
        {
            repository = new VanacyRepository<Vancacy>();
        }
        public IEnumerable<Vancacy> GetAllVanCy()
        {
            return repository.GetAllValue();
        }
        public void InsertVancy(Vancacy vancacys)
        {
            try
            {
                if (vancacys != null)
                {
                    repository.AddInfo(vancacys);
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
        public void UpdateStatus(Vancacy vancacys, int id)
        {
            try
            {
                if (vancacys != null && id != null)
                {
                    repository.Update(vancacys, id);
                }
            }
            catch (Exception e)
            {

            }
        }
        public void UpdateVancacy(Vancacy vancacys, int id)
        {
            try
            {
                if (vancacys != null && id != null)
                {
                    repository.UpdateCancacy(vancacys, id);
                }
            }
            catch (Exception e)
            {

            }
        }
        public Vancacy GetValueIDView(string name)
        {
            try
            {
                if (name != null)
                {
                    return repository.GetById(name);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public Vancacy GetIDVancyView(int id)
        {
            try
            {
                if (id != null)
                {
                    return repository.GetByIdVancacy(id);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}