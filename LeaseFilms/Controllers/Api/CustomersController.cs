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

        // GET /api/customers
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            var customersToReturn = Mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersToReturn;
        }

        // GET /api/customers/1
        public async Task<CustomerDto> GetCustomer(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var customerToReturn = Mapper.Map<CustomerDto>(customer);
            return customerToReturn;
        }

        // POST /api/customers
        [HttpPost]
        public async Task<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDto.Id = customer.Id;//this Id is created by the DB
            return customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        public async Task UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);//<src,dst>(src,dst);
            //don't need the template parameters since the compiler can infer from the arguments
            Mapper.Map(customerDto, customerInDb);
            await _context.SaveChangesAsync();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.SingleAsync(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
