using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_excercise_backend.Models.Request
{
  public class EmailRequest
  {
    public string Subject { get; set; }

    public string CC { get; set; }

    public string BCC { get; set; }

    public string To { get; set; }

    public string Body { get; set; }

    public bool IsPdf { get; set; }

    public bool IsCsv { get; set; }

    public bool IsDoc { get; set; }
  }

}
