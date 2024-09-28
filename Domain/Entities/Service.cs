using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? PriceByHour { get; set; }
        public virtual ICollection<Country>? Countries { get; set; }
    }
}
