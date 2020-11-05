using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.DataAccess;

namespace WebApi.Models
{
    public class CustomerModel
    {
        public string ID { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

    }

    public class OrderModel
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipAddress { get; set; }
    }


    public class CustomerDemographicModel
    {
        public string TypeID { get; set; }
        public string Description { get; set; }

        public ICollection<CustomerModel> Customers { get; set; }

    }
}