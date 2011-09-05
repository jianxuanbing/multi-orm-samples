using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Service.SD1
{
    public interface IPeopleService
    {
        IList<Person> GetAll();
        void CreatePerson(Person person);
        void EditPerson(Person person);
        Person Find(long id);
        void DeletePerson(Person person);
    }
}
