using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class Tweet
    {
        // Getters and setters for Tweet variables
        //public int id { get; set; }
        public string header { get; set; }
        public string msgSender { get; set; }
        public string body { get; set; }


        public Tweet(string headerIn, string senderIn, string bodyIn)
        {
            header = headerIn;
            msgSender = senderIn;
            body = bodyIn;
        }
    }
}
