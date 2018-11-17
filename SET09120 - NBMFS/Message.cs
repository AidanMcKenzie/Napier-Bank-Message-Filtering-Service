using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class Message
    {
        // Getters and setters
        public string Header { get; set; }
        public string MsgSender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


        public Message(string headerIn, string senderIn, string subjectIn, string bodyIn)
        {
            Header = headerIn;
            MsgSender = senderIn;
            Subject = subjectIn;
            Body = bodyIn;
        }


        /* public string ConvertTextspeak(bodyIn)
         {

         }

         public string QuarantineURLs(bodyIn)
         {

         }*/ 
    }

    public class MsgList
    {
        public List<Message> Messages { get; set; }
    }
}
