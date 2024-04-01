
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

        public void InsertVacancy(Vancacy vacancy)
        {
                    repository.AddVancacy(vacancy);

        }
        public void InsertCadidate(Vancacy vacancy)
        {
            repository.AddInfo(vacancy);

        }
        public IEnumerable<string> GetAllStatusNames()
        {
            return new List<string> { "Open", "Temporarily Suspended", "Closed" };
        }

        public Vancacy GetVacancyByID(int id)
        {
            return repository.GetByIdVancacy(id);
        }

        public void UpdateVacancy(Vancacy vacancy)
        {
            repository.Update(vacancy, vacancy.id);
        }
    }
}
