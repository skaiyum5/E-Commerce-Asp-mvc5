using FashionClub.Entities;
using FashionClub.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionClub.Web.Controllers
{
    public class SharedController : Controller
    {
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();
            try
            {
                var pictureList = new List<Picture>();
                var pictures = Request.Files;
                for (int i = 0; i < pictures.Count; i++)
                {
                    var picture = pictures[i];
                    var pictureName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/") + pictureName);
                    picture.SaveAs(path);

                    var pic = new Picture();
                    pic.Url = pictureName;
                    pictureList.Add(pic);
                }

                if (SharedServices.Instance.SavePicture(pictureList))
                {
                    result.Data = new {Success=true, pictureList};
                }
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }


            return result;
        }

    }
}