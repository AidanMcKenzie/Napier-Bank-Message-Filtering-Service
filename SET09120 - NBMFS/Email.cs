using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class Email
    {
        // Getters and setters for Email variables
        //public int id { get; set; }
        public string header { get; set; }
        public string msgSender { get; set; }
        public string subject { get; set; }
        public string body { get; set; }


        public Email(string headerIn, string senderIn, string subjectIn, string bodyIn)
        {
            header = headerIn;
            msgSender = senderIn;
            subject = subjectIn;
            body = bodyIn;
        }
    }
}
