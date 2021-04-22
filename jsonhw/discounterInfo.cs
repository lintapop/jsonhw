using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jsonhw
{
    public class DiscounterInfo
    {
        public Orgs orgs { get; set; }
    }

    public class Frg
    {
        public string id { get; set; }
        public Org[] org { get; set; }
    }

    public class Org
    {
        public string dataOrg { get; set; }
        public string doOrg { get; set; }
        public string hlink { get; set; }
        public string id { get; set; }
        public string informaddress { get; set; }
        public string informtel { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string servItem { get; set; }
        public string servTime { get; set; }
        public string text { get; set; }
    }

    public class Orgs
    {
        public Frg frg { get; set; }
    }
}