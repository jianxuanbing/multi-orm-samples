using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PersonN:IEntity
    {
        public virtual long Id
        {
            get;
            set;
        }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual int Age { get; set; }
    }
}
