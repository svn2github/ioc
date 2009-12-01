using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.DAL.Unit_of_Work
{
    public interface IUnitOfWork<TObject>
    {
        void Insert(TObject item);
        void Update(TObject item);
        void Delete(TObject item);
        void Commit();
        void Rollback();
    }
}
