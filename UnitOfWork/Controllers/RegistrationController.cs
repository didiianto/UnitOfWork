using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Registration>> Get()
        {
            return await unitOfWork.RegistrationRepository.GetAll();
            //return await unitOfWork.RegistrationRepository.GetWhere(x => x.SerialNo == "3311");
        }

        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public async Task<Registration> Get(Guid id)
        {
            return await unitOfWork.RegistrationRepository.Get(id);
        }

        // POST api/<RegistrationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Registration registration)
        {
            registration.Id = Guid.NewGuid();
            registration.DateCreated = DateTime.Now;    
            unitOfWork.RegistrationRepository.Add(registration);
            await unitOfWork.SaveAsync();

            return Ok(registration.Id);
        }

        // PUT api/<RegistrationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Registration registration)
        {
            var updated = unitOfWork.RegistrationRepository.Get(id).Result;
            updated.DateModified = DateTime.Now;
            updated.Name = registration.Name;
            unitOfWork.RegistrationRepository.Update(updated);
            await unitOfWork.SaveAsync();

            return Ok(updated.Id);
        }

        // DELETE api/<RegistrationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var toDelete = unitOfWork.RegistrationRepository.Get(id).Result;
            unitOfWork.RegistrationRepository.Delete(toDelete);
            await unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
