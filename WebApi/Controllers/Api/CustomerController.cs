using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using System.Data.Entity;
using WebApi.DataAccess;

namespace WebApi.Controllers.Api
{
    public class CustomerController : ApiController
    {
        //routeTemplate: "api/{controller}/{action}/{id}",

        //api/customer/
        public IHttpActionResult GetCustomers()
        {
            IList<CustomerModel> customers = null;

            using (var ctx = new NorthwindEntities())
            {
                customers = ctx.Customers.Select(s => new CustomerModel()
                {
                    ID = s.CustomerID,
                    Name = s.ContactName,
                    Phone = s.Phone,
                    Adress = s.Address,
                    City = s.City,
                    CompanyName = s.CompanyName,
                    Title = s.ContactTitle,
                    
                }).ToList();
            }

            if (customers.Count == 0)
            {
                return NotFound();
            }

            return Ok(customers);
        }
        // api/customer?id=
        public IHttpActionResult GetCustomerById(string id)
        {
            CustomerModel customer = null;
            using (var ctx = new NorthwindEntities())
            {
                customer = ctx.Customers.Where(s => s.CustomerID == id).Select(s => new CustomerModel()
                {
                    ID = s.CustomerID,
                    Name = s.ContactName,
                    Phone = s.Phone,
                    Adress = s.Address,
                    City = s.City,
                    CompanyName = s.CompanyName,
                    Title = s.ContactTitle,
                }).FirstOrDefault<CustomerModel>();
            }

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        // api/customer?name=
        public IHttpActionResult GetCustomerByName(string name)
        {
            CustomerModel customer = null;
            using (var ctx = new NorthwindEntities())
            {
                customer = ctx.Customers.Where(s => s.ContactName == name).Select(s => new CustomerModel()
                {
                    ID = s.CustomerID,
                    Name = s.ContactName,
                    Phone = s.Phone,
                    Adress = s.Address,
                    CompanyName = s.CompanyName,
                }).FirstOrDefault<CustomerModel>();
            }
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        public IHttpActionResult PostNewCustomer(CustomerModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model.");

            using (var ctx = new NorthwindEntities())
            {
                ctx.Customers.Add(new Customer()
                {
                    CustomerID = customer.ID,
                    ContactName = customer.Name,
                    Address = customer.Adress,
                    Phone = customer.Phone,
                    City = customer.City,
                    ContactTitle = customer.Title,
                    CompanyName = customer.CompanyName,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult PutUpdateCustomer(CustomerModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new NorthwindEntities())
            {
                var customerUpdate = ctx.Customers.Where(s => s.CustomerID == customer.ID)
                                                        .FirstOrDefault<Customer>();

                if (customerUpdate != null)
                {
                    customerUpdate.ContactName = customer.Name;
                    customerUpdate.Address = customer.Adress;
                    customerUpdate.Phone = customer.Phone;
                    customerUpdate.City = customer.City;
                    customerUpdate.ContactTitle = customer.Title;
                    customerUpdate.CompanyName = customer.CompanyName;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
    
        public IHttpActionResult DeleteCustomer(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");
            using (var ctx = new NorthwindEntities())
            {
                var customer = ctx.Customers.Where(s => s.CustomerID == id).FirstOrDefault<Customer>();
                ctx.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
