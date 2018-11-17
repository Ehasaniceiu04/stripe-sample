using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions;
using System.Diagnostics;
using Stripe;
using Microsoft.Extensions.Options;

namespace StripeCoreClient.Controllers
{
    public class BookController : Controller
    {
        StripeSettings _stripeSettings;
        public BookController(IOptions<StripeSettings> stripeSettingsoptions)
        {
            _stripeSettings = stripeSettingsoptions.Value;
        }
        // embedded form
        public ActionResult Index()
        {
            string stripePublishableKey = _stripeSettings.PublishedKey;
            var model = new IndexViewModel() { StripePublishableKey = stripePublishableKey };
            return View(model);
        }
        [HttpPost]
        public ActionResult Charge(ChargeViewModel chargeViewModel)
        {
            var chargeService = new ChargeService();
            var chargeOptions = new ChargeCreateOptions()
            {
                //required
                Amount = 4000,
                Currency = "usd",
                SourceId =  chargeViewModel.StripeToken,
                //optional
                Description = string.Format("Fundamental book for {0}", chargeViewModel.StripeEmail),
                ReceiptEmail = chargeViewModel.StripeEmail
            };
            try
            {
                var stripeCharge = chargeService.Create(chargeOptions);
            }
            catch (StripeException stripeException)
            {
                Debug.WriteLine(stripeException.Message);
                ModelState.AddModelError(string.Empty, stripeException.Message);
                return View(chargeViewModel);
            }
            Debug.WriteLine(chargeViewModel.StripeEmail);
            Debug.WriteLine(chargeViewModel.StripeToken);
            return RedirectToAction("Confirmation");
        }
        public ActionResult Confirmation()
        {
            return View();
        }
        //custom form
        public ActionResult Custom()
        {
            return View();
        }
    }
}