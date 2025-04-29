using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentMVC.BLL.Service.AttachmentService
{
    public interface IAttachmentService
    {
        public string? Uploud(IFormFile file, string foldername);

        bool Delete(string fileName , string folderName);
    }
}
