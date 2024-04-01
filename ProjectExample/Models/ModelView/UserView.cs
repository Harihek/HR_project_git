using ProjectExample.Models.Entities;
using ProjectExample.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.ModelView
{
    public class UserView
    {
        private readonly UserRepository<infoUser> repository;

        public UserView()
        {
            repository = new UserRepository<infoUser>();
        }
        public IEnumerable<infoUser> GetAllVanCy()
        {
            return repository.GetAllValue();
        }
        public void InsertUser(infoUser user)
        {
            try
            {
                if (user != null)
                {
                    repository.AddInfo(user);
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
    }
}