using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_excercise_backend.Models
{
  public class EmailParams
  {
    public bool IsHtml { get; set; }

    public string To { get; set; }

    public string Cc { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string From { get; set; }

    public string FromName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public byte[] AttachFileBytes { get; set; }

    public string AttachFileName { get; set; }

    public string Bcc { get; set; }

    public bool EnableSsl { get; set; }

    public List<MailHeader> MailHeader { get; set; }
  }
}
