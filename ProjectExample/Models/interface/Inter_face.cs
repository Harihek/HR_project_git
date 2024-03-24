using ProjectExample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExample.Models
{
    public interface Inter_face<T>
    {
        IEnumerable<T> GetAllValue();

        void AddInfo(T entity);

        void Delete(int id);

        void Update(T entity, object id);

        //T GetById(int id);
    }
}
