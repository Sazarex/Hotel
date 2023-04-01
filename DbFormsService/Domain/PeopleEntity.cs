using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFormsService.Domain
{
    public abstract class PeopleEntity: BaseEntity
    {
        public string Name { get; set; }
    }
}
