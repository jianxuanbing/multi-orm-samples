using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.NHibernate;
using Service.NH3.Repositories;
using Domain;
using Infrastructure;

namespace Service.NH3
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

        public IList<PersonN> GetAll()
        {
            return _peopleRepository.GetAll().ToList();
        }

        public void CreatePerson(PersonN person)
        {
            _peopleRepository.Add(person);
            _unitOfWork.Commit();
        }

        public void EditPerson(PersonN person)
        {
            _peopleRepository.Update(person);
            _unitOfWork.Commit();
        }

        public PersonN Find(long id)
        {
            var entity = _peopleRepository.GetById(id);
            return entity;
        }

        public void DeletePerson(PersonN person)
        {
            _peopleRepository.Delete(person);
            _unitOfWork.Commit();
        }
    }
}
