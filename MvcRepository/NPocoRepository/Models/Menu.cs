using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPocoRepository.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string ActionName { get; set; }

    }
}