using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbFormsService.GlobalVariables;

namespace DbFormsService.Domain
{
    public class Employee: PeopleEntity
    {
        public Post Post { get; set; }
    }
}
