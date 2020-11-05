using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<CustomerModel> customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2499/api/");
                //HTTP GET
                var responseTask = client.GetAsync("customer");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerModel>>();
                    readTask.Wait();

                    customer = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    customer = Enumerable.Empty<CustomerModel>();

                    ModelState.AddModelError(string.Empty, "Sunucu Hatası.");
                }
            }
            return View(customer);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            CustomerModel customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2499/api/");
                //HTTP GET
                var responseTask = client.GetAsync("customer?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerModel>();
                    readTask.Wait();

                    customer = readTask.Result;
                }
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(CustomerModel customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2499/api/customer");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<CustomerModel>("customer", customer);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2499/api/customer");
                //HTTP GET
                var responseTask = client.PutAsJsonAsync<CustomerModel>("customer", customer);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }
        
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2499/api/");

                var deleteTask = client.DeleteAsync("customer/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }
    }
}