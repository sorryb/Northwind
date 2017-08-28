using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSampleSearchEngine.Models;

namespace MVCSampleSearchEngine.Controllers
{
    public class SearchEngineController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /**************************************************************************************************
         Get the Autocomplete data based on search String
         *************************************************************************************************/

        public JsonResult GetAutoCompleteData(String searchText,int? maxResults=5)
        {
            if (searchText == null)
            {
                searchText = "";
            }

            var data = GetMockData();

            var finalData = data.Where(i => i.Title.Contains(searchText))
                .Select(i => new
                {
                    text = i.Title,
                    value = i.Id
                })
                .Take(maxResults.GetValueOrDefault())
               ;

            return Json(finalData, JsonRequestBehavior.AllowGet);
        }

        /**************************************************************************************************
         Get the Selected Data
         *************************************************************************************************/

        public JsonResult GetSelectedResultData(int id)
        {
            var data = GetMockData().Where(i=>i.Id.Equals(id));

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /**************************************************************************************************
         *  Return mock data *
         * ************************************************************************************************/
        private List<SearchEngineModel> GetMockData()
        {
            List<SearchEngineModel> sampleList = new List<SearchEngineModel> { 
            new SearchEngineModel(){Id=1,Title="Hyderabad School Of Innovations",Description="Complexity has no meaning in our Dictionary. I joined Hyderabad Schools of Innovations. What about you???"},
            new SearchEngineModel(){Id=2,Title="Good news to Software Engineer aspirants and Students...",Description="Do you want to learn how to develop a Website instead of browsing and enjoying the site..Do you want to create softwares like Media Player, Instead of enjoying music with existing players..Do you want to develop games rather just playing games..<br />Here is the chance..<br />Hyderabad Techies, A Tier #1, non commercial Microsoft User Group starting new batch for Hyderabad School Of Innovations...<br />You need not to pay single paisa, You just need enthu and commitment towards your learnings.<br /><br />For registrations contact admin@hyderabadtechies.info"},
            new SearchEngineModel(){Id=3,Title="Chandra's MVC Nugets",Description="You dont need to Code!!! <br /><br />We will write the code for you!!!"},
            new SearchEngineModel(){Id=4,Title="HyderabadTechies",Description="A 100% Non-Comercial community"},
            new SearchEngineModel(){Id=5,Title="Chandra Shekar Thota",Description="Founder of HyderabadTechies"},
             new SearchEngineModel(){Id=6,Title="HyderabadTechies.info",Description="if I need some technical help, Why do the techies, help Members like anything????"},
             new SearchEngineModel(){Id=7,Title="Techies Network",Description="Let's not define Helping Limits!!"},
             new SearchEngineModel(){Id=8,Title="Stuck with work",Description="No more tension at work!! <br /> Add Techies and Start Chatting with Techies!!!"},
             new SearchEngineModel(){Id=9,Title="Tech Pati",Description="Do you have guts to play Technical tecpati???? Start playing at <a href=http://www.hyderabadtechies.info/index.php?option=com_wrapper&view=wrapper&Itemid=346>HyderabadTechies</a>"},
             new SearchEngineModel(){Id=10,Title="HT Radio",Description="Listen to Hyderabad Techies Radio"},
             new SearchEngineModel(){Id=11,Title="HyderabadTechies Features",Description="Try to explore different feaatures of Hyderabad Techies, to find more interesting stuff"}

            };
            return sampleList;
        }



    }
}
