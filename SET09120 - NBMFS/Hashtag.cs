using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class Hashtag
    {
        // Getters and setters
        public string header { get; set; }
        public string hashtag { get; set; }
        public int count { get; set; }


        public Hashtag(string headerIn, string hashtagIn, int countIn)
        {
            header = headerIn;
            hashtag = hashtagIn;
            count = countIn;
        }
    }

    public class HashList
    {
        public List<Hashtag> hashtags { get; set; }
    }
}
