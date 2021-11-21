using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidationApp.Web.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.Controllers
{
    [Route("api/[controller]")] // Buranin anlami https://localhost:44330/api/CustomersApi olarak http isteklerine göre calisir. Metot isimlerine göre tepki vermez. Best Practise olarak kullanimi bu sekildedir. Ancak metoda göre islem yapsin istersek 2 yol var. 1. yol [Route("api/[controller]/[action]")] yapilmasi gerekir ve tavsiye edilmez. Bunun yerine ilgli metot üzerine [Route("MappingExample")] [HttpGet] attribute'ler eklenerek islem yapilabilir.
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _customerValidator; //Validator'imizi class olarak gecmek icin DI (Dependency Injection) yapiyoruz
        private readonly IMapper _mapper;

        public CustomersApiController(AppDbContext context, IValidator<Customer> customerValidator, IMapper mapper)
        {
            _context = context;
            _customerValidator = customerValidator;
            _mapper = mapper;
        }


        [Route("MappingExample")] // Controller bazindaki route, buradaki Route attribute ile ezmis oluyoruz.
        [HttpGet]
        public IActionResult MappingExample()
        {
            Customer customer = new Customer { Id = 1, Name = "Max", Email = "test@gmail.com", Age = 25, CreditCard = new CreditCard { Number = "123456789", ValidDate = DateTime.Now } };

            return Ok(_mapper.Map<CustomerDto>(customer));
        }


        // GET: api/CustomersApi
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            //_mapper.Map<Customer>(new CustomerDto()); // Eger CustomerDto nesnesini Customer nesnesine cevirmek isteseydik bu sekilde yazabilirdik.

            return _mapper.Map<List<CustomerDto>>(customers); //IMapper sayesinde Customer nesnesini CustomerDto tek adim ile burada dönüstürüyoruz. Generic icerisine dönüs tipi,parantez icerisine kaynak nesne yazilir.
        }

        // GET: api/CustomersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/CustomersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var result = _customerValidator.Validate(customer);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(x => new
                {
                    property = x.PropertyName,
                    error = x.ErrorMessage
                }));
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/CustomersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
