using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Service.NH3
{
    public interface IPeopleService
    {
        IList<PersonN> GetAll();
        void CreatePerson(PersonN person);
        void EditPerson(PersonN person);
        PersonN Find(long id);
        void DeletePerson(PersonN person);
    }
}
