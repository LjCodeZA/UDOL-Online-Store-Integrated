using Newtonsoft.Json;
using OnlineStore.DAL;
using OnlineStore.Models;
using OnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ProductDashboardController : Controller
    {
        // GET: ProductDashboard
        public ActionResult Index()
        {
            string productDashboardViewModelString = GetProducts();
            var reserializedProduct = JsonConvert.DeserializeObject<string>(productDashboardViewModelString);
            var deserializedProduct = JsonConvert.DeserializeObject<List<ProductDashboardViewModel>>(reserializedProduct);

            return View(deserializedProduct);
        }

        public string GetProducts()
        {
            var client = new HttpClient();
            return client.GetStringAsync(Properties.Settings.Default.GetProductsRESTAPI).Result;
        }

        public int CartCheckout(string cartData)
        {
            Console.WriteLine(cartData);
            return 1;
        }
    }
}