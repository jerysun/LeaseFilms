using AutoMapper;
using LeaseFilms.Dtos;
using LeaseFilms.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LeaseFilms.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET /api/customers
        public async Task<IHttpActionResult> GetCustomers()
        {
            //We need the EAGER loading, thus here the Include method
            var customers = await _context.Customers.Include(c => c.MembershipType).ToListAsync();
            var customersToReturn = Mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customersToReturn);
        }

        // GET /api/customers/1
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var customerToReturn = Mapper.Map<CustomerDto>(customer);
            return Ok(customerToReturn);
        }

        // POST /api/customers
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer(CustomerDto customerDto)
        {
            //if (!ModelState.IsValid) //For MVC, NOT for Web API
            //    return BadRequest();

            var customer = Mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDto.Id = customer.Id;//this Id is created by the DB
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return BadRequest();

            //Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);//<src,dst>(src,dst);
            //don't need the template parameters since the compiler can infer from the arguments
            Mapper.Map(customerDto, customerInDb);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.SingleAsync(c => c.Id == id);
            if (customer == null)
                return BadRequest();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
