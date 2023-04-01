using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFormsService.Domain
{
    public abstract class BaseEntity
    {

        public virtual int Id { get; set; }
    }
}
