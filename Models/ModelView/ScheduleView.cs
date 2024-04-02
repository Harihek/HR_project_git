using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class ScheduleView
    {
        private readonly Schedule_Interview_Repository<Candidate_Employe> repository;
        public ScheduleView()
        {
            repository = new Schedule_Interview_Repository<Candidate_Employe>();
        }
        public void InsertSchedule(int? id1, int? id2, int? id3, string code_ca, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            try
            {
                if (code_ca != null && dateTimeStart != null && dateTimeEnd != null)
                {
                    repository.AddInfo(id1,id2,id3,code_ca,dateTimeStart,dateTimeEnd);
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
        public IEnumerable<ShowInterView> GetInterViewAccount(string name)
        {
            try
            {
                List<ShowInterView> candidate = repository.GetInterViewByName(name);

                if (candidate != null)
                {
                    return candidate;
                }
                else
                {
                    return Enumerable.Empty<ShowInterView>();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ShowInterView>();
            }
        }
        public IEnumerable<ShowInterView> GetResult(int id_cadi, int id_emp)
        {
            try
            {
                List<ShowInterView> candidate = repository.GetResults(id_cadi,id_emp);

                if (candidate != null)
                {
                    return candidate;
                }
                else
                {
                    return Enumerable.Empty<ShowInterView>();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ShowInterView>();
            }
        }
        public void UpdateResultsView(int id_CadiEmp, int status)
        {
            try
            {
                if (id_CadiEmp != null && status != null)
                {
                    repository.UpdateResult(id_CadiEmp, status);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}