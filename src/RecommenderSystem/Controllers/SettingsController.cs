using InventoryManagement.Filters;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace InventoryManagement.ViewModels.Settings
{



    public class WebSettingsViewModel
    {
        public WebSettings wb { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public WebSettingsViewModel(WebSettings wb)
        {
            this.wb = wb;
        }
        public WebSettingsViewModel()
        {
            this.wb = new WebSettings();
        }
    }
}

namespace InventoryManagement.Controllers
{
    public class SettingsController : PanelController
    {
        public WebSettingsRepository WebRepo { get; set; }
        public SettingsController() : base()
        {
            this.WebRepo = new WebSettingsRepository();
        }

        [HttpGet]
        public ActionResult ProjectSetting()
        {
            return View(new WebSettingsViewModel(WebRepo.Get()));
        }

        [HttpPost]
        public ActionResult ProjectSetting(WebSettingsViewModel viewmodel)
        {
            if (viewmodel.ImageFile != null)
            {
                viewmodel.wb.ProjectImagePath = UploadProjectLogo(viewmodel.ImageFile);
            }
          
            WebRepo.Update(viewmodel.wb);
            
            return View(new WebSettingsViewModel(WebRepo.Get()));
        }

        public string UploadProjectLogo(HttpPostedFileBase file)
        {
            return UploadAndGetLocation(file, "~/Images/ProjectImages", "Project-Logo");
        }

        public string UploadAndGetLocation(HttpPostedFileBase file, string filepath, string FileName)
        {
            string SaveLocation = $"{filepath}/{FileName}{Path.GetExtension(file.FileName)}";
            var path = System.Web.HttpContext.Current.Server.MapPath(SaveLocation);
            new FileInfo(path).Directory.Create();
            file.SaveAs(path);
            return SaveLocation;
        }

    }
}