using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CelebrationRegister.Core.Convertors;
using CelebrationRegister.Core.Generator;


namespace CelebrationRegister.Core.Tools
{
    public static class ImageTools
    {
        public static string SaveReportCardImage(IFormFile image, string employeeName, string childName)
        {
            if (image != null)
            {
                //Check Exist Folder
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Information/ReportCard/", employeeName);
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);

                string imageName = childName+"-ReportCard" + Path.GetExtension(image.FileName);

                string imagePath = Path.Combine(path ,imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return imageName;

            }

            return null;
        }

        public static string SavePersonalImage(IFormFile image, string employeeName, string childName)
        {
            if (image != null)
            {
                //Check Exist Folder
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Information/PersonalImage/", employeeName);
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);

                string imageName = childName + "-PersonalImage" + Path.GetExtension(image.FileName);

                string imagePath = Path.Combine(path, imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return imageName;

            }

            return null;
        }

        public static string SaveOptionalDetailImage(IFormFile image, string employeeName, string childName,string detailTitle)
        {
            if (image != null)
            {
                //Check Exist Folder
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Information/ReportCard/", employeeName);
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);

                string imageName = childName + "-" + detailTitle + Path.GetExtension(image.FileName);

                string imagePath = Path.Combine(path, imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return imageName;

            }

            return null;

        }

        public static string SaveImage(IFormFile image, string folder)
        {
            if (image != null)
            {
                string imageName = CodeGenerator.GenerateCode() + Path.GetExtension(image.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", folder,
                    imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return imageName;
            }

            return null;
        }

        public static void DeleteImage(IFormFile image, string path)
        {

        }

        public static string UpdateImage(IFormFile image, string folder, string defaultImageName, string oldImage)
        {
            if (image != null)
            {
                string imagePath = "";
                if (oldImage != defaultImageName)
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", folder, oldImage);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                return SaveImage(image, folder);
            }

            return null;
        }

        public static string SaveImportExcelFile(IFormFile file)
        {
            if (file != null)
            {
                //Check Exist Folder
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Data/");
                bool exists = Directory.Exists(path);
                if (!exists)
                    Directory.CreateDirectory(path);

                string fileName = "DataImport-" + DateTime.Now.ToShamsi().Replace("/","-") + Path.GetExtension(file.FileName);

                string imagePath = Path.Combine(path, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fileName;

            }

            return null;

        }

    }
}
