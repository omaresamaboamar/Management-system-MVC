using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentMVC.BLL.Service.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions =  [".jpg", ".png", ".pdf", ".docx" ];
        const int maxSize = 1024 * 1024 * 2;
        public string? Uploud(IFormFile file, string folderName)
        {
            //1.Check Extension
            var extention = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extention)) return null;
            //2.Check Size
            if (file.Length == 0 || file.Length > maxSize) return null;
            //3.Get Located Folder Path

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            //7.Use Stream To Copy File
            file.CopyTo(fs);
            //8.Return FileName To Store In Database
            return fileName;

        }
        public bool Delete(string fileName , string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

    }
}
