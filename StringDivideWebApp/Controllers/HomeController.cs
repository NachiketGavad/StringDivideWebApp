using Microsoft.AspNetCore.Mvc;
using StringDivideWebApp.Models;
using System.Diagnostics;
using System.Linq;

namespace StringDivideWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessStringArray(StringDivideModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Index", model);
            //}

            // Splitting comma-separated string into array
            string[] inputArray = model.InputArray.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            // Perform processing of the string array
            string processedResult = processing_string(inputArray);

            // Store input and processed result in the database
            model.ProcessedResult = processedResult;
            //_context.StringDivideModels.Add(model);
            //_context.SaveChanges();

            // Display the processed result
            model.ProcessedResult = processedResult;
            return View("Index", model);
        }


        public string processing_string(string[] inputArray)
        {
            int maxLength = 0;
            string result = "";

            for (int i = 0; i < inputArray.Length;)
            {
                if (inputArray[i] == "-")
                {
                    i++;
                    continue;
                }

                int count = 0;
                string last = "";

                while (i < inputArray.Length && inputArray[i] != "-")
                {
                    last = inputArray[i];
                    count++;
                    i++;
                }

                if (count > maxLength || (count == maxLength && last.Length > result.Length))
                {
                    maxLength = count;
                    result = last;
                }
            }

            return result;
        }
    }
}
