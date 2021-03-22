using TownerTown.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownerTown.Web.Models
{
    public class BusinessViewModel
    {
        public List<Business> Businesses { get; set; }
        public SearchViewModel SearchViewModel { get; set; }

    }
}
