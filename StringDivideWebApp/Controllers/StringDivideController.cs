using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StringDivideWebApp.Models;
using System;
using System.Collections.Generic;

namespace StringDivideWebApp.Controllers
{
    public class StringDivideController : Controller
    {

        private readonly ApplicationDbContext _context;

        public StringDivideController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new StringDivideModel());
        }

        [HttpPost]
        public IActionResult ProcessStringArray(StringDivideModel model)
        {
            // Splitting comma-separated string into array
            string[] inputArray = model.InputArray.Split(',');

            // Perform any required operations here
            string processedResult = processing_string(inputArray); // Placeholder, perform your operations

            // Convert processed result back to string
            string processedResultString = processedResult; // Perform conversion if needed

            // Store to database
            model.ProcessedResult = processedResultString;
            _context.StringDivideModels.Add(model);
            _context.SaveChanges();

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
