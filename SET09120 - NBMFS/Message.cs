using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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
            Regex hashRegex = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)");
            
            if (File.Exists(@"c:\Users\aidan\Documents\hashtags.json"))
            {
                HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtags.json"));

                // For every hashtag found in the tweet body
                foreach (var hTag in hashRegex.Matches(bodyIn))
                {
                    bool anyHashtagsFound = true;
                    // Loop through every hashtag in the JSON file
                    foreach (Hashtag hashtag in hashtagList.Hashtags.ToList())
                    {
                        // If the current hashtag in the JSON file matches the hashtag in the tweet body
                        if (hashtag.hashtag == hTag.ToString())
                        {
                            hashtag.count++;

                            anyHashtagsFound = true;

                            File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
                            
                            //Exit loop
                            goto HashFound;
                        }
                        else
                        {
                            anyHashtagsFound = false;
                        }
                    }

                    if (anyHashtagsFound == false)
                    {
                        Hashtag newHashtag = new Hashtag(hTag.ToString(), 1);
                        hashtagList.Hashtags.Add(newHashtag);

                        File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
                    }

                HashFound:
                    continue;
                }
            }
            // Else create a new file and write to it
            else
            {
                File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", "{\"Hashtags\": []}");
                HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtags.json"));

                // For every hashtag found in the tweet body
                foreach (var hTag in hashRegex.Matches(bodyIn))
                {
                    Hashtag newHashtag = new Hashtag(hTag.ToString(), 1);
                    hashtagList.Hashtags.Add(newHashtag);
                }

                File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
            }
        }
    }

    public class MsgList
    {
        public List<Message> Messages { get; set; }
    }
}
