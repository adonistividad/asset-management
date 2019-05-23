using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restapii2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //-- create database and populate seed data
            restapii2.Migrations.Configuration conf = new Migrations.Configuration();

            return View();
        }
    }
}
