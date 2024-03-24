using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class Cadidate_Vancies_View
    {
        private readonly Candidate_Vanacies_Repository<Cadidate_Vanacies> repository;
        public Cadidate_Vancies_View()
        {
            repository = new Candidate_Vanacies_Repository<Cadidate_Vanacies>();

        }
        public IEnumerable<Cadidate_Vanacies> GetAllValues()
        {
            return repository.GetCadidate_Vanacy();
        }

    }
}