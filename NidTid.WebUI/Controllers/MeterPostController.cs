using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using NidTid.WebUI.Models;
using System.Web.Services;
using NidTid.WebUI.Security;


namespace NidTid.WebUI.Controllers
{
    public class MeterPostController : Controller
    {
        private IMeterPostRepository repository;
        private IVehicleRepository vehicleRepo;

        public MeterPostController(IMeterPostRepository repository, IVehicleRepository vehicleRepository)
        {
            this.repository = repository;
            this.vehicleRepo = vehicleRepository;
        }


        [HttpGet]
        [Authorize]
        public ActionResult NewMeterPost()
        {
            MeterPostViewModel meterpost = new MeterPostViewModel();
            meterpost.Vehicles = vehicleRepo.Vehicles;
            meterpost.MeterPost = new MeterPost();
            return PartialView(meterpost);
        }

        [HttpPost]
        [Authorize]
        public String SaveMeterPost(MeterPostViewModel meterModel)
        {
            MeterPost meterpost = meterModel.MeterPost;
            var identity = ((CustomPrincipal)HttpContext.User).CustomIdentity;
            meterpost.UserId = identity.UserId;
            //meterpost.CurrentMeter = Integer(meterModel.MeterPost.CurrentMeter);
            String message = "";
            if (ModelState.IsValid)
            {
                repository.SaveMeterPost(meterpost);
                message = "Rapporten har sparats!";
            }
            else
            {
                message = "Fel!";
            }
            return message;
        }

        [HttpPost]
        [Authorize]
        public string DeletePost(int id)
        {
            MeterPost post = repository.MeterPosts.FirstOrDefault(r => r.Id == id);
            repository.DeletePost(post);
            return "Posten är raderad";
        }

        [HttpGet]
        [Authorize]
        public ActionResult TableByVehicle(int vehicleId)
        {
            MeterPostListModel model = new MeterPostListModel();
            model.MeterPosts = repository.MeterPosts.Where(m => m.VehicleId == vehicleId).OrderByDescending(r => r.Id);
            return PartialView(model);
        }

    }
}

