using LeaseFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LeaseFilms.ViewModels;
using System.Threading.Tasks;

namespace LeaseFilms.Controllers
{
    public class CustomersController : Controller
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = await _context.MembershipTypes.ToListAsync()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = await _context.Customers.SingleAsync(c => c.Id == customer.Id);

                //autoMapper.Map(Customer, customerInDb); DTO; potential security hole
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Birthdate = customer.Birthdate;
            }
            await _context.SaveChangesAsync();// Transaction

            return RedirectToAction("Index", "Customers");
        }

        public async Task<ActionResult> New()
        {
            var membershipTypes = await _context.MembershipTypes.ToListAsync();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), // added for hidden value
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            var customer = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            // It serves the View below
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };

            //redirect to CustomerForm.cshtml rather than (the "default" Edit.cshtml if we don't designate the arguments)
            return View("CustomerForm", viewModel);
        }
    }
}