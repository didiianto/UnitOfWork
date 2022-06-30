using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;
using UnitOfWork.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/<RegistrationController>
        [HttpGet]
        public async Task<IEnumerable<Registration>> Get()
        {
            return await unitOfWork.RegistrationRepository.GetAll();
        }

        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public async Task<Registration> Get(Guid id)
        {
            return await unitOfWork.RegistrationRepository.Get(id);
        }

        // POST api/<RegistrationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegistrationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegistrationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
