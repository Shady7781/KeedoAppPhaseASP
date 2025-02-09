﻿using KeedoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KeedoApp.Controllers.Kindergarten
{
    public class KindergartenController : Controller


    {

        HttpClient httpClient;
        string baseAddress;

        public KindergartenController()
        {
            baseAddress = "http://localhost:8082/SpringMVC/servlet/User/Service/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Kindergarten
        public ActionResult Index()
        {
            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/retrieve-all-kindergartens").Result;

            IEnumerable<Kindergarden> kindergardens;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                kindergardens = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Kindergarden>>().Result;
            }
            else
            {
                kindergardens = null;

            }



            return View(kindergardens);

        }



        // GET: Kindergarten
        public ActionResult ClientIndex()
        {
            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/retrieve-all-kindergartens").Result;
            HttpResponseMessage httpResponseMessage2 = httpClient.GetAsync(baseAddress + "kid/static/nb").Result;
            HttpResponseMessage httpResponseMessage3 = httpClient.GetAsync(baseAddress + "daycare/nb").Result;
            int kids;
            int daycares;
            IEnumerable<Kindergarden> kindergardens;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                kindergardens = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Kindergarden>>().Result;
            }
            else
            {
                kindergardens = null;

            }
            if (httpResponseMessage2.IsSuccessStatusCode)
            {

                ViewBag.kids = httpResponseMessage2.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                ViewBag.kids = null;
            }

            if (httpResponseMessage3.IsSuccessStatusCode)
            {

                ViewBag.daycares = httpResponseMessage3.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                ViewBag.daycares = null;
            }



            return View(kindergardens);

        }



        public ActionResult Search1(string name)
        {
            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/kindergarden/"+name).Result;

            Kindergarden kindergarden;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                kindergarden = httpResponseMessage.Content.ReadAsAsync<Kindergarden>().Result;
            }
            else
            {
                kindergarden = null;

            }



            return View(kindergarden);

        }

        // GET: Kindergarten/Details/5



        [HttpGet]
        public ActionResult Details(int id)
        {

            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/retrieve-kindergarten-details/" + id.ToString()).Result;
            HttpResponseMessage httpResponseMessage2 = httpClient.GetAsync(baseAddress + "kid/static/nb" ).Result;
            HttpResponseMessage httpResponseMessage3 = httpClient.GetAsync(baseAddress + "daycare/nb").Result;
            HttpResponseMessage httpResponseMessage4 = httpClient.GetAsync(baseAddress + "Kindergartens/kindergarden-revieww/"+id.ToString()).Result;
            Kindergarden kindergarden;
            int kids;
            int daycares;
            Boolean review;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                kindergarden = httpResponseMessage.Content.ReadAsAsync<Kindergarden>().Result;
            }
            else
            {
                kindergarden = null;
            }


            if (httpResponseMessage2.IsSuccessStatusCode)
            {
                
                ViewBag.kids = httpResponseMessage2.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                ViewBag.kids = null;
            }

            if (httpResponseMessage3.IsSuccessStatusCode)
            {

                ViewBag.daycares = httpResponseMessage3.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                ViewBag.daycares = null;
            }

            if (httpResponseMessage4.IsSuccessStatusCode)
            {

                ViewBag.review = httpResponseMessage4.Content.ReadAsAsync<Boolean>().Result;
            }
            else
            {
                ViewBag.review = null;
            }
            return View(kindergarden);





        }
        // GET: Kindergarten/Create
        public ActionResult Create()
        {


            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/directors").Result;

            IEnumerable<User> directors;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                directors = httpResponseMessage.Content.ReadAsAsync<IEnumerable<User>>().Result;
            }
            else
            {
                directors = null;

            }
            ViewBag.directorFK = new SelectList(directors, "idUser", "Login");



            return View();
        }

        // POST: Kindergarten/Create
        [HttpPost]
        public async Task<ActionResult> Create(Kindergarden kindergarden) {


            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            var response = await httpClient.PostAsJsonAsync(baseAddress + "Kindergartens/add-kindergarden/" + kindergarden.directorFk, kindergarden);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/directors").Result;
            IEnumerable<User> directors;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                directors = httpResponseMessage.Content.ReadAsAsync<IEnumerable<User>>().Result;
            }
            else
            {
                directors = null;
            }

            ViewBag.directorFK = new SelectList(directors, "idUser", "Login");

            return View(kindergarden);

        }

        public ActionResult Edit(int id)
        {


            Kindergarden kindergarden = null;



            var responseTask = httpClient.GetAsync(baseAddress + "Kindergartens/retrieve-kindergarten-details/" + id);
            // responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Kindergarden>();
                readTask.Wait();

                kindergarden = readTask.Result;
            }
            //------------
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "Kindergartens/directors").Result;
            IEnumerable<User> directors;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                directors = httpResponseMessage.Content.ReadAsAsync<IEnumerable<User>>().Result;
            }
            else
            {
                directors = null;
            }

            ViewBag.directorFk = new SelectList(directors, "idUser", "Login");


            return View(kindergarden);
        }


        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Kindergarden kindergarden)
        {
            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            var putTask = httpClient.PutAsJsonAsync<Kindergarden>(baseAddress + "Kindergartens/update-kindergarten/" + id.ToString(), kindergarden);
            putTask.Wait();

            var result = putTask.Result;

            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }









        // GET: Kindergarten/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kindergarten/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _AccessToken = Session["AccessToken"];
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer " + _AccessToken));
            //HTTP POST
            var putTask = httpClient.DeleteAsync(baseAddress+"Kindergartens/remove-kindergarten/" + id.ToString());
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            
            return View();
        }
    }
}
