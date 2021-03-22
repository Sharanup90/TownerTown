using TownerTown.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownerTown.Web.Models
{
    public class RegisterViewModel
    {
        public Business Business { get; set; }
        public SearchViewModel SearchViewModel { get; set; }
        public List<IFormFile> uploadImagesFormFile { set; get; }
    }
}
