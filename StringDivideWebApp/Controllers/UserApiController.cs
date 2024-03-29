using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StringDivideWebApp.Models;
using System.Text;

namespace StringDivideWebApp.Controllers
{
    public class UserApiController : Controller
    {
        public string url = "https://localhost:7166/api/StringDivideUser/";
        private HttpClient client= new HttpClient();
        public IActionResult Index()
        {
            List<StringDivideAppUser> users = new List<StringDivideAppUser>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<StringDivideAppUser>>(result);
                if (data != null)
                {
                    users = data;
                }
            }
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StringDivideAppUser std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["insert-message"] = "User Added...";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            StringDivideAppUser std = new StringDivideAppUser();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;   
                var data = JsonConvert.DeserializeObject<StringDivideAppUser>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(StringDivideAppUser std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url+std.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update-message"] = "User updated...";
                return RedirectToAction("Index");
            }
            return View();
        } 
        public IActionResult Details(int id)
        {
            StringDivideAppUser std = new StringDivideAppUser();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<StringDivideAppUser>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }
        public IActionResult Delete(int id)
        {
            StringDivideAppUser std = new StringDivideAppUser();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<StringDivideAppUser>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult Delete_confirm(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["delete-message"] = "User deleted...";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
