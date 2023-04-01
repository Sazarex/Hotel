using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFormsService.Domain
{
    public class Payment : BaseEntity
    {
        public Client Client { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Room Room{ get; set; }
    }
}
