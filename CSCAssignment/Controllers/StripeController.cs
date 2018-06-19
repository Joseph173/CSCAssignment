using CSCAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Stripe;
using System.Web.Mvc;

namespace CSCAssignment.Controllers
{
    public class StripeController : Controller
    {
        // GET: Stripe
        public ActionResult Stripe()
        {
            return View();
        }

        public ActionResult Charge()
        {
            ViewBag.Message = "Learn how to process payments with Stripe";
            return View(new StripeChargeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var chargeId = await ProcessPayment(model);
            // You should do something with the chargeId --> Persist it maybe?

            return View("Success");
        }

        private static async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    // convert the amount of £12.50 to pennies i.e. 1250
                    Amount = (int)(model.Amount * 100),
                    Currency = "gbp",
                    Description = "Description for test charge",
                    SourceTokenOrExistingSourceId = model.Token
                };

                var chargeService = new StripeChargeService("sk_test_ovyKKmCRsaGz2bMMC8itVXfH");
                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge.Id;
            });
        }

    }
}
