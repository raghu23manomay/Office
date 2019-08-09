using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Calibration.Models;
using office.Models;

namespace Calibration.Controllers
{
    public class MailBoxController : Controller
    {
        // GET: MailBox
        public ActionResult MailList(string tempEmails="")
        {
            ViewBag.emails = tempEmails;
            return View();
        }
        
        public ActionResult ComposeMail()
        {
            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("ComposeMail")
                        : View("ComposeMail");
        }

        public ActionResult MailBody()
        {
            mailTemplate data = new mailTemplate();
            data.EmailBody = "";
            return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("MailBody",data)
                        : View("MailBody",data);
        }

        public List<SelectListItem> binddropdown(string action,int val = 0)
        {
              OfficeDbContext _db = new OfficeDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action, @val",
                    new SqlParameter("@action", action),
                    new SqlParameter("@val",val))
                   .ToList()
                   .AsEnumerable()
                   .Select(r => new SelectListItem
                   {
                       Text = r.Text.ToString(),
                       Value = r.Value.ToString(),
                       Selected = r.Value.Equals(Convert.ToString(val))
                   }).ToList();

            return res;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sendmail(string emailFrom, string emailto, string Description, string subject, string cc, string bcc)
        {
            try
            {                   
                String body = Description;          
                SendHtmlFormattedEmail(emailFrom,emailto,Description,subject,cc,bcc);
                return Json("mail Send Sucessfully");
            }
            catch (Exception ex)
            {
                var mgs = ex.Message;
                return Json(mgs);
            }
              
        }

        private void SendHtmlFormattedEmail(string emailFrom, string emailto, string Description, string subject,string cc,string bcc)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                Description = Description.Replace('"', ' ');
                mailMessage.From = new MailAddress(emailFrom);
                mailMessage.Subject = subject;
                mailMessage.Body = Description;
                mailMessage.IsBodyHtml = true;
                string[] multi = emailto.Split(',');
                foreach(var item in multi)
                {
                    mailMessage.To.Add(new MailAddress(item));
                }
                if(cc !="")
                {
                    string[] multiCC = cc.Split(',');
                    foreach (var item in multiCC)
                    {
                        mailMessage.CC.Add(new MailAddress(item));
                    }                        
                }
                if (bcc != "")
                {
                    string[] multiBCC = bcc.Split(',');
                    foreach (var item in multiBCC)
                    {
                        mailMessage.Bcc.Add(new MailAddress(item));
                    }                       
                }

                   

                //mailMessage.To.Add(new MailAddress(emailto));
                SmtpClient smtp = new SmtpClient();
                // smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "rmudgal23@gmail.com";
                NetworkCred.Password = "Uma@020395";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }



        public ActionResult GetTemplateData(int? TempId = 0)
        {
            
            string body = string.Empty;
            mailTemplate data = new mailTemplate();
            if (TempId > 0)
            {
                OfficeDbContext _db1 = new OfficeDbContext();
                //var result = _db1.mailTemplate.SqlQuery(@"exec USP_GetMailTemplate
                //@TempId",
                //new SqlParameter("@TempId", TempId)).ToList<mailTemplate>();
                //data = result.FirstOrDefault();
                // body = data.EmailBody;
            }

            ViewData["EmailTemplateList"] = binddropdown("EmailTemplateList", 0);
            ViewBag.mailbody = body;
            ViewBag.mailsub = data.Emailsubject;
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("Mailbody",data)
               : View("Mailbody",data);
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
              
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        //int ReqID = Convert.ToInt32(Request.Form[0]);
                        string filePath = Server.MapPath("~/UploadFile/");

                        bool isExists = System.IO.Directory.Exists(filePath);
                        if (!isExists) { System.IO.Directory.CreateDirectory(filePath); }


                        string extension = Path.GetExtension(file.FileName);
                        string fileName = fname;

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(filePath, fileName);
                        file.SaveAs(fname);                       

                    }



                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

    }
}