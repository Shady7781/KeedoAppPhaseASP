﻿using KeedoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Web.Mvc;
namespace KeedoApp.Controllers
{
    public class EmpruntBookController : Controller
    {

        HttpClient httpClient;
        string baseAddress;
        public EmpruntBookController()
        {
            baseAddress = "http://localhost:9293/SpringMVC/servlet/emprunt";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }



        // GET: EmpruntBook
        public ActionResult EmpruntBook()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/showAllEmprunt").Result;

            IEnumerable<EmpruntBook> emprunt;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                emprunt = httpResponseMessage.Content.ReadAsAsync<IEnumerable<EmpruntBook>>().Result;


            }
            else
            {
                emprunt = null;
            }


            return View(emprunt);
        }


        public ActionResult EmpruntEnCours()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/getEmpruntsEncours").Result;

            IEnumerable<EmpruntBook> empruntBook;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                empruntBook = httpResponseMessage.Content.ReadAsAsync<IEnumerable<EmpruntBook>>().Result;


            }
            else
            {
                empruntBook = null;
            }



            return View(empruntBook);


        }






        public ActionResult EmpruntEnCoursAdmin()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/getEmpruntsEncours").Result;

            IEnumerable<EmpruntBook> empruntBook;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                empruntBook = httpResponseMessage.Content.ReadAsAsync<IEnumerable<EmpruntBook>>().Result;


            }
            else
            {
                empruntBook = null;
            }



            return View(empruntBook);


        }






        [HttpGet]
        public ActionResult CreeEmprunt()
        {
            return View("CreeEmprunt");
        }

        // POST: EmpruntBook/CreeEmprunt
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreeEmprunt(EmpruntBook  empruntBook, int idBook)
        {


            string Baseurl = "http://localhost:9293/SpringMVC/servlet/emprunt/";

            using (var pb = new HttpClient())
            {
                pb.BaseAddress = new Uri(Baseurl);
                pb.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await pb.PostAsJsonAsync("creerEmprunt/" + idBook.ToString(), empruntBook);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EmpruntEnCours");
                }
            }
            return View(empruntBook);


            /* HttpClient client = new HttpClient();
             client.BaseAddress = new Uri("http://localhost:9293");
             client.PostAsJsonAsync<Bus>("SpringMVC/servlet/bus/saveBus", bus).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

             return RedirectToAction("Bus");
         }*/

        }

        public ActionResult LivreRendu(int idEmprunt)
        {

            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "/stoppedEmprunt/" + idEmprunt.ToString()).Result;
            EmpruntBook emprunt;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                emprunt = httpResponseMessage.Content.ReadAsAsync<EmpruntBook>().Result;
            }
            else
            {
                emprunt = null;
            }

            return View(emprunt);
        }

        public ActionResult ProlongEmprunt(int idEmprunt) {



            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "/prolongEmprunt/" + idEmprunt.ToString()).Result;
            EmpruntBook emprunt;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                emprunt = httpResponseMessage.Content.ReadAsAsync<EmpruntBook>().Result;
            }
            else
            {
                emprunt = null;
            }

            return View(emprunt);


        }



        // GET: EmpruntBook/Details/5
            public ActionResult Details(int idEmprunt)
        {
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + "/detailsEmprunt/" + idEmprunt.ToString()).Result;
            EmpruntBook emprunt;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                emprunt = httpResponseMessage.Content.ReadAsAsync<EmpruntBook>().Result;
            }
            else
            {
                emprunt = null;
            }

            return View(emprunt);
        }


        public ActionResult EmpruntsExpires()
        {



            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/listeEmpruntsExpires").Result;

            IEnumerable<EmpruntBook> empruntBook;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                empruntBook = httpResponseMessage.Content.ReadAsAsync<IEnumerable<EmpruntBook>>().Result;


            }
            else
            {
                empruntBook = null;
            }



            return View(empruntBook);


        }
       
           
        public ActionResult CleanEmpruntExpired()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/cleanEmpruntExpired").Result;

            IEnumerable<EmpruntBook> empruntBook;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                empruntBook = httpResponseMessage.Content.ReadAsAsync<IEnumerable<EmpruntBook>>().Result;


            }
            else
            {
                empruntBook = null;
            }



            return View(empruntBook);



        }

        public ActionResult batch()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9293");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.GetAsync("SpringMVC/servlet/emprunt/runMail").Result;

            //IEnumerable<EmpruntBook> empruntBook;
            return View();


        }






        // GET: EmpruntBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpruntBook/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpruntBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmpruntBook/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpruntBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmpruntBook/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
