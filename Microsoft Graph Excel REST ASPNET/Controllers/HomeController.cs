/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft_Graph_Excel_REST_ASPNET.Helpers;
using Microsoft_Graph_Excel_REST_ASPNET.Models;
using Resources;
using System;
using System.IO;

namespace Microsoft_Graph_Excel_REST_ASPNET.Controllers
{
    public class HomeController : Controller
    {
        GraphService graphService = new GraphService();

        public ActionResult Index()
        {
            return View("Graph");
        }

        [Authorize]
        // Get the current user's email address from their profile.
        public async Task<ActionResult> GetMyUserInfo()
        {
            try
            {

                // Get an access token.
                string accessToken = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();

                // Get the current user's email address. 
                UserInfo myInfo = await graphService.GetMe(accessToken);
                ViewBag.Name = myInfo.Name;
                ViewBag.Email = myInfo.Address;
                return View("Graph");
            }
            catch (Exception e)
            {
                if (e.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = Resource.Error_Message + Request.RawUrl + ": " + e.Message });
            }
        }

        [Authorize]
        // Upload user information to Excel workbook "demo.xslx."
        public async Task<ActionResult> UploadInfoToExcel()
        {

            try
            {

                // Get an access token.
                string accessToken = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();

                // Upload user info to Excel file "demo.xslx".
                ViewBag.Message = await graphService.AddInfoToExcel(accessToken, Request.Form["user-name"], Request.Form["user-email"]);

                ViewBag.Name = Request.Form["user-name"];
                ViewBag.Email = Request.Form["user-email"];
                return View("Graph");
            }
            catch (Exception e)
            {
                if (e.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = Resource.Error_Message + Request.RawUrl + ": " + e.Message });
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}