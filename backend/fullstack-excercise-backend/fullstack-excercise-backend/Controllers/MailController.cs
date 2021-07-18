using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullstack_excercise_backend.Models.Response;
using fullstack_excercise_backend.Models.Request;
using fullstack_excercise_backend.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace fullstack_excercise_backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MailController : ControllerBase
  {

    public static List<Files> AllFiles()
    {
      List<Files> files = new List<Files>();
      files.Add(new Files { CreatedAt = DateTime.Today, FileName = "Sample-3", FilePath =  "Files/Sample-3.pdf", FileType = "pdf" });
      files.Add(new Files { CreatedAt = DateTime.Today.AddDays(1), FileName = "Sample-2", FilePath = "Files/sample-2.csv", FileType = "csv" });
      files.Add(new Files { CreatedAt = DateTime.Today.AddDays(2), FileName = "Sample-1", FilePath = "Files/sample-1.csv", FileType = "csv" });
      files.Add(new Files { CreatedAt = DateTime.Today.AddDays(3), FileName = "Sample-4", FilePath = "Files/sample-4.doc", FileType = "doc" });
      files.Add(new Files { CreatedAt = DateTime.Today.AddDays(4), FileName = "Sample-5", FilePath = "Files/sample-5.doc", FileType = "doc" });
      return files;
    }
    private static void SetEmailParams(EmailParams emailParams)
    {
      emailParams.Host = "smtp.gmail.com";
      emailParams.Port = 587;
      emailParams.FromName =  "*** Your Name ***";
      emailParams.From = "*** From Email ***";
      emailParams.Username = "*** From Email ***";
      emailParams.Password = "*** Password ***";
      emailParams.EnableSsl = true;
    }
    public static async Task<EmailLog> SendEmailAsync(EmailParams emailParams, bool IsPdf, bool IsCsv, bool IsDoc)
    {
      try
      {
        SetEmailParams(emailParams);
        using (SmtpClient smtpClient = new SmtpClient(emailParams.Host, emailParams.Port))
        {
          using (MailMessage message = new MailMessage())
          {
            foreach (var item in emailParams.To.Split(';'))
            {
              if (!string.IsNullOrWhiteSpace(item))
              {
                message.To.Add(new MailAddress(item.Trim()));
              }
            }

            message.From = new MailAddress(emailParams.From, emailParams.FromName);

            message.Subject = emailParams.Subject;
            string linkBody = string.Empty;
            string fileName = string.Empty;

            emailParams.Body += linkBody;
            message.Body = emailParams.Body;
            message.IsBodyHtml = true;

            if (!string.IsNullOrWhiteSpace(emailParams.Bcc))
            {
              message.Bcc.Add(new MailAddress(emailParams.Bcc));
            }
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = emailParams.EnableSsl;

            if (IsPdf)
            {
              List<Files> pdfFiles = AllFiles().FindAll(x => x.FileType == "pdf");
              pdfFiles.ForEach((item) =>
              {
                message.Attachments.Add(new Attachment("wwwroot/"+item.FilePath, MediaTypeNames.Application.Pdf));
              });
            }

            if (IsCsv)
            {
              List<Files> pdfFiles = AllFiles().FindAll(x => x.FileType == "csv");
              pdfFiles.ForEach((item) =>
              {
                message.Attachments.Add(new Attachment("wwwroot/" + item.FilePath, MediaTypeNames.Application.Octet));
              });
            }

            if (IsDoc)
            {
              List<Files> pdfFiles = AllFiles().FindAll(x => x.FileType == "doc");
              pdfFiles.ForEach((item) =>
              {
                message.Attachments.Add(new Attachment("wwwroot/" + item.FilePath, MediaTypeNames.Application.Octet));
              });
            }

            smtpClient.Credentials = new NetworkCredential(emailParams.Username, emailParams.Password);
            await smtpClient.SendMailAsync(message);

            return new EmailLog(emailParams, true, "Success");
          }
        }
      }
      catch (Exception ex)
      {
        return new EmailLog(emailParams, false, ex.Message);
      }
    }

    [HttpGet]
    public List<Files> Get()
    {
      return AllFiles();
    }

    [HttpPost]
    public async Task<bool> Post(EmailRequest emailRequest)
    {
      try
      {
        if(emailRequest.To != null && emailRequest.Body != null)
        { 
          EmailParams emailParams = new EmailParams();
          emailParams.Body = emailRequest.Body;
          emailParams.Subject = emailRequest.Subject;
          emailParams.Cc = emailRequest.CC;
          emailParams.To = emailRequest.To;
          emailParams.Bcc = emailRequest.BCC;
          await SendEmailAsync(emailParams, emailRequest.IsPdf, emailRequest.IsCsv, emailRequest.IsDoc);
          return true;
        }
        return false;
      } catch(Exception E) {
        return false;
      }
    }
  }
}
