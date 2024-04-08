using Microsoft.AspNetCore.Mvc;

namespace StringDivideWebApp.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.Mysession = HttpContext.Session.GetString("UserSession").ToString();
            }
            return View();
        }

        //[HttpPost]
        //public IActionResult Calculate(double num1, double num2, string operation)
        //{
        //    double result = 0;
        //    switch (operation)
        //    {
        //        case "add":
        //            result = num1 + num2;
        //            break;
        //        case "subtract":
        //            result = num1 - num2;
        //            break;
        //        case "multiply":
        //            result = num1 * num2;
        //            break;
        //        case "divide":
        //            if (num2 != 0)
        //                result = num1 / num2;
        //            else
        //                return BadRequest("Cannot divide by zero");
        //            break;
        //        default:
        //            return BadRequest("Invalid operation");
        //    }
        //    return Ok(result);
        //}

        [HttpPost]
        public IActionResult Calculate(string input)
        {
            try
            {
                // Evaluate the expression using DataTable
                var result = new System.Data.DataTable().Compute(input, null);
                ViewBag.Result = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Invalid input: " + ex.Message;
                return PartialView("_ErrorPartial");
            }

            return PartialView("_ResultPartial");
        }

    }
}
