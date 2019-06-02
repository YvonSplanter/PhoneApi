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

        // GET api/phone/{id}
        [HttpGet("{id}")]
        public ActionResult<Phone> Get(string id)
        {
            return PhoneDao.GetById(id);
        }

        //Post api/phone
        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            var result = PhoneDao.Create(phone);
            if (result == 1)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public ActionResult Update(Phone phone) {
            var result = PhoneDao.Update(phone);
            if (result == 1)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var result = PhoneDao.Delete(id);
            if (result == 1)
                return Ok();
            return BadRequest();
        }
    }
}