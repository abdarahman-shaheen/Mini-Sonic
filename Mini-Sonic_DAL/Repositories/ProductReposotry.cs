using Microsoft.Extensions.Configuration;
using Mini_Sonic.Model;
using Mini_Sonic_DAL.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Sonic_DAL.Repositories
{
    public class ProductReposotry : GenericRepositry<Item>
    {
        public ProductReposotry(IConfiguration _configuration) : base(_configuration)
        {
        }


    }
}
