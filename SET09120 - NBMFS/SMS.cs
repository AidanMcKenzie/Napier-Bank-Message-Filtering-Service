using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class SMS
    {
        // Getters and setters for SMS variables
        public int id { get; set; }
        public string header { get; set; }
        public string msgSender { get; set; }
        public string body { get; set; }


        public SMS(string headerIn, string senderIn, string bodyIn)
        {
            id = id++;
            header = headerIn;
            msgSender = senderIn;
            body = bodyIn;
        }

        public bool ValidateMessage(string header, string body)
        {
            if ((header.Length == 9) && (body.Length > 0 && body.Length <= 140))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
