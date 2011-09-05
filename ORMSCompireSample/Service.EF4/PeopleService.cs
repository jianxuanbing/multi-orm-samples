using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.EF4;
using Service.EF4.Repositories;
using Domain;
using Infrastructure;

namespace Service.EF4
{
    public class PeopleService:IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PeopleService(IPeopleRepository peopleRepository,IUnitOfWork unitOfWork)
        {
            _peopleRepository = peopleRepository;
            _unitOfWork = unitOfWork;
        }

        public IList<Person> GetAll()
        {
            return _peopleRepository.GetAll().ToList();
        }

        public void CreatePerson(Person person)
        {
            _peopleRepository.Add(person);
            _unitOfWork.Commit();
        }

        public Person Find(long id)
        {
           var entity= _peopleRepository.GetById(id);
           return entity;
        }

        public void EditPerson(Person person)
        {
            _peopleRepository.Update(person);
            _unitOfWork.Commit();
        }

        public void DeletePerson(Person person)
        {
            _peopleRepository.Delete(p => p.Id == person.Id);
            _unitOfWork.Commit();
        }


    }
}
