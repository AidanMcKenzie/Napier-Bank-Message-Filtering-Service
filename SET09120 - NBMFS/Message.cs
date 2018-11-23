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

        public void WriteHashtags(string bodyIn)
        {
            List<string> hashtagsList = new List<string>();

            Regex hashRegex = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)");

            HashList hashyList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtag.json"));

            if (File.Exists(@"c:\Users\aidan\Documents\hashtag.json"))
            {
                //HashList hashyList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtag.json"));

                //Hashtag hashtag = new Hashtag(null, 1);

                foreach (var hTag in hashRegex.Matches(bodyIn))
                {
                    if (!hashtagsList.Contains(hTag.ToString()))
                    {
                        //Hashtag tempHashTagName = new Hashtag(hashtag.hashtag, 1);
                       // hashyList.Hashtags.Add(tempHashTagName);

                        hashtagsList.Add(hTag.ToString());
                        File.WriteAllText(@"c:\Users\aidan\Documents\" + hTag.ToString() + ".json", hTag.ToString());

                        //File.WriteAllText(@"c:\Users\aidan\Documents\hashtag.json", JsonConvert.SerializeObject(hashyList, Formatting.Indented) + Environment.NewLine);
                    }
                    /*else
                    {
                        hashtag.count++;
                        hashyList.Hashtags.Add(hashtag);

                        File.WriteAllText(@"c:\Users\aidan\Documents\hashtag.json", JsonConvert.SerializeObject(hashyList, Formatting.Indented) + Environment.NewLine);
                    }*/
                }
            }
            // Else create a new file and write to it
            else
            {
                /*File.WriteAllText(@"c:\Users\aidan\Documents\hashtag.json", "{\"Hashtag\": []}");

                HashList hashyList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtag.json"));

                foreach (Hashtag matchedHashtag in hashRegex.Matches(bodyIn))
                {
                    if (!hashyList.Hashtags.Contains(matchedHashtag))
                    {
                        Hashtag tempHashTagName = new Hashtag(matchedHashtag.ToString(), 1);
                        hashyList.Hashtags.Add(tempHashTagName);

                        File.WriteAllText(@"c:\Users\aidan\Documents\hashtag.json", JsonConvert.SerializeObject(hashyList, Formatting.Indented) + Environment.NewLine);
                    }
                    else
                    {
                        matchedHashtag.count++;

                        File.WriteAllText(@"c:\Users\aidan\Documents\hashtag.json", JsonConvert.SerializeObject(hashyList, Formatting.Indented) + Environment.NewLine);
                    }
                }*/
            }
        }






    }

    public class MsgList
    {
        public List<Message> Messages { get; set; }
    }
}
