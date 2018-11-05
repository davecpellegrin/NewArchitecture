using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SandboxCustomers.Api.Messages;
using SandboxCustomers.Api.Models;
using SandboxCustomers.Api.Services;

namespace SandboxCustomers.Api.Controllers.v1
{
    //put this in if versioning is used (switch out Route attribute)
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
 
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("{id:int}", Name = nameof(Get))]
        public ActionResult Get(int id)
        {
            var dataModel = _repository.Get(id);
            if (dataModel == null)
            {
                return NotFound();
            }

            var item = Mapper.Map<CustomerItem>(dataModel);
            return Ok(item);
        }
 
        [HttpGet(Name = nameof(GetAll))]
        public ActionResult GetAll()
        {
            var dataModel = _repository.GetAll().ToList();
            var items = Mapper.Map<List<CustomerItem>>(dataModel);
            return Ok(items);
        }

        // add item
        [HttpPost(Name = nameof(Post))]
        public ActionResult<CustomerItem> Post([FromBody] CustomerItemAdd newItem)
        {
            if (newItem == null)
            {
                return BadRequest();
            }
 
            var newDataModel = Mapper.Map<Customer>(newItem);
            _repository.Add(newDataModel);
 
            if (!_repository.Save())
            {
                throw new Exception("Adding a Customer failed on save");
            }
 
            var newDataModelRefreshed = _repository.Get(newDataModel.Id);
            var newItemRefreshed = Mapper.Map<CustomerItem>(newDataModelRefreshed);
            return CreatedAtRoute(nameof(Get), new { id = newDataModelRefreshed.Id }, newDataModelRefreshed);
        }

        // update item
        [HttpPut]
        [Route("{id:int}", Name = nameof(Put))]
        public ActionResult<CustomerItem> Put(int id, [FromBody] CustomerItemUpdate updatedItem)
        {
            if (updatedItem == null)
            {
                return BadRequest();
            }
 
            var existingDataModel = _repository.Get(id);
            if (existingDataModel == null)
            {
                return NotFound();
            }
 
            Mapper.Map(updatedItem, existingDataModel);
            _repository.Update(existingDataModel);
 
            if (!_repository.Save())
            {
                throw new Exception("Updating a Customer failed on save");
            }

            var updatedExistingItem = Mapper.Map<CustomerItem>(existingDataModel);
            return Ok(updatedExistingItem);
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(Delete))]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            if (!_repository.Save())
            {
                throw new Exception("Deleting a Customer failed on save");
            }
 
            return Ok();
        }

    }
}