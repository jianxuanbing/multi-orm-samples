using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using Domain;

namespace Infrastructure.NHibernate.Mapping
{
    public class PersonMap : ClassMap<PersonN>
    {
        public PersonMap()
        {
            Table("[PersonN]");
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.FirstName).Not.Nullable().Length(256);
            Map(p => p.LastName).Not.Nullable().Length(256);
            Map(p => p.Age).Not.Nullable();
        }
    }
}
