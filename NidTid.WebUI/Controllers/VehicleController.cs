using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using Newtonsoft.Json;

namespace NidTid.WebUI.Controllers
{
    public class VehicleController : Controller
    {
        private IVehicleRepository repository;
        private IMeterPostRepository meterpostRepository;

        public VehicleController(IVehicleRepository vehicleRepository, IMeterPostRepository meterpostRepo)
        {
            this.repository = vehicleRepository;
            this.meterpostRepository = meterpostRepo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create() {
            Vehicle vehicle = new Vehicle();
            return PartialView(vehicle);
        }

        [HttpPost]
        public string Create(Vehicle vehicle)
        {
            string msg = "";
            if (ModelState.IsValid) {
                repository.SaveVehicle(vehicle);
                msg = "Fordonet är tillagt i databasen.";
            }
            else {
                msg = "Ett fel inträffade, försök igen.";
            }
            return msg;
        }

        [HttpGet]
        public ActionResult List()
        {
            VehicleListViewModel model = new VehicleListViewModel();
            model.Vehicles = repository.Vehicles;

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult VehicleInfo(int id)
        {
            Vehicle vehicle = repository.Vehicles.FirstOrDefault(v => v.Id == id);
            VehicleViewModel model = new VehicleViewModel();
            if (vehicle.MeterPost.Count() > 0) {
                MeterPost lastPost = vehicle.MeterPost.LastOrDefault(m => m.VehicleId == id);
                model.CurrentMeter = lastPost.CurrentMeter;
                model.Description = vehicle.Description;
                model.Date = lastPost.Date.ToShortDateString();
                model.User = lastPost.User.Name;
                model.RegNr = vehicle.RegNr;
            }
            else {
                model.CurrentMeter = 0;
                model.Description = vehicle.Description;
                model.Date = "-";
                model.User = "-";
                model.RegNr = vehicle.RegNr;
            }

            return PartialView(model);
        }

    }
}