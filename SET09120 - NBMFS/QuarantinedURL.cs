using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class QuarantinedURL
    {
        // Getters and setters
        public string url { get; set; }


        public QuarantinedURL(string urlIn)
        {
            url = urlIn;
        }
    }

    public class URLsList
    {
        public List<QuarantinedURL> URLs { get; set; }
    }
}
