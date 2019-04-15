using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneApi.DAO;
using PhoneApi.Model;

namespace PhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        // GET api/phones
        [HttpGet]
        public ActionResult<CatalogPhone> Get(int id)
        {
            return PhoneDao.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Phone> Get(string id)
        {
            return PhoneDao.GetById(id);
        }
    }
}