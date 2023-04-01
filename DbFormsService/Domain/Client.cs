using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFormsService.Domain
{
    public class Client: PeopleEntity
    {
        public List<Room> TakenRooms { get; set; }
        public List<Room> ReservedRooms { get; set; }
        public List<Payment> Payments { get; set; }
        public Employee Employee { get; set; }
    }
}
