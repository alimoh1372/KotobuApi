using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KotobuApi.Models;

namespace KotobuApi.Controllers
{
    public class HomeController : Controller
    {
        //api get student
        public ActionResult Index()
        {
            var bookController = new BookController();
            IEnumerable<Book> _books;
            ViewBag.Title = "تست API های مربوط به Book";
            _books = bookController.Get();
            return View(_books);
        }
        public ActionResult GetBooks()
        {
            IEnumerable<Book> _books = null;
            ViewBag.Title = "تست API Get|API/Book";
            string baseUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}{Url.Content("~")}";
            string getReust = baseUrl + "api/";
            
            
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(getReust);
                //http get
                Task<HttpResponseMessage> responseTask = client.GetAsync("book");
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<IList<Book>> readTask = result.Content.ReadAsAsync<IList<Book>>();
                    readTask.Wait();
                    _books = readTask.Result;

                }
                else //if web api sent error 
                {
                    //log the state here
                    _books = Enumerable.Empty<Book>();
                    ModelState.AddModelError(string.Empty,"web api error to response please contact administrator");
                }
            }
            return View(_books);
        }
        [HttpGet]
        public ActionResult GetBook(int id)
        {
            Book _book = null;
            ViewBag.Title = "تست API Get|API/Book";
            string baseUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}{Url.Content("~")}";
            string getReust = baseUrl + "api/";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(getReust);
                //http get
                Task<HttpResponseMessage> responseTask = client.GetAsync($"book/{id}");
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<Book> readTask = result.Content.ReadAsAsync<Book>();
                    readTask.Wait();
                    _book = readTask.Result;

                }
                else //if web api sent error 
                {
                    //log the state here
                    _book = new Book();
                    ModelState.AddModelError(string.Empty, "web api error to response please contact administrator");
                }
            }
            return View(_book);
        }
    }
}
