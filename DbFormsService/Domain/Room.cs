using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFormsService.Domain
{
    public class Room
    {
        public int Id{ get; set; }
        public bool? IsTaken { get; set; }
        public int Number { get; set; }
        public bool? IsReserved { get; set; }
    }
}
