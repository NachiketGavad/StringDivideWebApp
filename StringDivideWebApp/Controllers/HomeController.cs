using Microsoft.AspNetCore.Mvc;
using StringDivideWebApp.Models;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

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
            //string[] inputArray = model.InputArray.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            bool formISValid = ModelState.IsValid;

            //if (!formISValid)
            //{
            //    return View("Index");
            //}
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if (state.Errors.Any())
                {
                    var errors = state.Errors.Select(e => e.ErrorMessage);
                    // Log or inspect the errors here to understand why the model state is not valid
                }
            }

            string processedResult = string.Empty;
            // Perform processing of the string array
            if (model.InputArray != null)
            {
                processedResult = processing_string(model.InputArray);
            }

            // Store input and processed result in the database
            model.ProcessedResult = processedResult;
            _context.StringDivideModels.Add(model);
            _context.SaveChanges();

            // Display the processed result
            model.ProcessedResult = processedResult;
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Index2()
        {
            var model = new StringDivideModel();
            return View("StringProcessPartial", model);
        }

        [HttpPost]
        public PartialViewResult ProcessString(StringDivideModel model)
        {

            // Check if the input is blank
            if (string.IsNullOrWhiteSpace(model.InputArray))
            {
                ModelState.AddModelError("InputArray", "Input cannot be blank");
                // Return the same view with validation errors
                return PartialView("_ProcessedResultPartial", model);
            }

            var processedString = string.Empty;

            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if (state.Errors.Any())
                {
                    var errors = state.Errors.Select(e => e.ErrorMessage);
                    // Log or inspect the errors here to understand why the model state is not valid

                }
            }
            if (!ModelState.IsValid)
            {
                // display errors here if required 
                //return a different partial view to display errors 
            }

            processedString = processing_string(model.InputArray);
            model.ProcessedResult = processedString;

            _context.StringDivideModels.Add(model);
            _context.SaveChanges();


            return PartialView("_ProcessedResultPartial", model);

        }


        public string processing_string(string input)
        {
            string[] inputArray = input.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

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

        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double num1, double num2, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "add":
                    result = num1 + num2;
                    break;
                case "subtract":
                    result = num1 - num2;
                    break;
                case "multiply":
                    result = num1 * num2;
                    break;
                case "divide":
                    if (num2 != 0)
                        result = num1 / num2;
                    else
                        return BadRequest("Cannot divide by zero");
                    break;
                default:
                    return BadRequest("Invalid operation");
            }
            return Ok(result);
        }
    }
}
