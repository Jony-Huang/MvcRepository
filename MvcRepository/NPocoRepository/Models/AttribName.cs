using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPocoRepository.Models
{
    public partial class AttribName
    {
        public AttribName()
        {
            this.Attrib = new HashSet<Attrib>();
        }

        public string AttribName1 { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Attrib> Attrib { get; set; }
    }
}