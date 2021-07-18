using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_excercise_backend.Models.Response
{
  public class Files
  {
    public string FileName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string FilePath { get; set; }

    public string FileType { get; set; }
  }
}
