using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPocoDemo.Models
{
    public class AttribName
    {

        //public string AttribName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Attrib> Attrib { get; set; }
    }
}