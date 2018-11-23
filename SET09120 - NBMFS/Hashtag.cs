using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class Hashtag
    {
        // Getters and setters
        public string hashtag { get; set; }
        public int count { get; set; }


        public Hashtag(string hashtagIn, int countIn)
        {
            hashtag = hashtagIn;
            count = countIn;
        }
    }

    public class HashList
    {
        public List<Hashtag> Hashtags { get; set; }
    }
}
