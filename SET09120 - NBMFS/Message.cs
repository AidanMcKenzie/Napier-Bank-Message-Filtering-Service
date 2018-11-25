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

         }*/

        
        public void WriteHashtags(string bodyIn)
        {
            Regex hashRegex = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)");
            
            if (File.Exists(@"c:\Users\aidan\Documents\hashtags.json"))
            {
                HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtags.json"));

                // For every hashtag found in the tweet body
                foreach (var foundHashtag in hashRegex.Matches(bodyIn))
                {
                    // Default state for this flag
                    bool hashtagInFile = true;
                    // Loop through every hashtag in the JSON file
                    foreach (Hashtag hashtag in hashtagList.Hashtags.ToList())
                    {
                        // If the current hashtag in the JSON file matches the hashtag in the tweet body
                        if (hashtag.hashtag == foundHashtag.ToString())
                        {
                            // Add to the number of times this hashtag has been used
                            hashtag.count++;

                            // Flag that a hashtag has been found
                            hashtagInFile = true;

                            // Write the count update to the JSON file
                            File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
                            
                            //Exit loop
                            goto HashFound;
                        }
                        else
                        {
                            // If the current hashtag is not in the JSON file, set flag to false
                            hashtagInFile = false;
                        }
                    }
                    // If hashtag is not in the file
                    if (hashtagInFile == false)
                    {
                        // Create a new entry in the JSON file for the hashtag
                        Hashtag newHashtag = new Hashtag(foundHashtag.ToString(), 1);
                        hashtagList.Hashtags.Add(newHashtag);

                        // Write the hashtag list to the JSON file
                        File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
                    }

                HashFound:
                    continue;
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", "{\"Hashtags\": []}");
                HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtags.json"));

                // For every hashtag found in the tweet body
                foreach (var foundHashtag in hashRegex.Matches(bodyIn))
                {
                    // Create new hashtag object to be written to the JSON file
                    Hashtag newHashtag = new Hashtag(foundHashtag.ToString(), 1);
                    hashtagList.Hashtags.Add(newHashtag);
                }

                // Write new hashtags to the JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\hashtags.json", JsonConvert.SerializeObject(hashtagList, Formatting.Indented) + Environment.NewLine);
            }
        }

        public void WriteMentions(string bodyIn)
        {
            Regex mentionRegex = new Regex(@"(?:(?<=\s)|^)@(\w*[A-Za-z_]+\w*)");

            if (File.Exists(@"c:\Users\aidan\Documents\mentions.json"))
            {
                MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(@"c:\Users\aidan\Documents\mentions.json"));

                // For every mention found in the tweet body
                foreach (var foundMention in mentionRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the mention
                    Mention newMention = new Mention(foundMention.ToString());
                    mentList.Mentions.Add(newMention);

                    // Write the mention list to the JSON file
                    File.WriteAllText(@"c:\Users\aidan\Documents\mentions.json", JsonConvert.SerializeObject(mentList, Formatting.Indented) + Environment.NewLine);
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\mentions.json", "{\"Mentions\": []}");
                MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(@"c:\Users\aidan\Documents\mentions.json"));

                // For every mention found in the tweet body
                foreach (var foundMention in mentionRegex.Matches(bodyIn))
                {
                    // Create new mention object to be written to the JSON file
                    Mention newMention = new Mention(foundMention.ToString());
                    mentList.Mentions.Add(newMention);
                }

                // Write new mentions to the JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\mentions.json", JsonConvert.SerializeObject(mentList, Formatting.Indented) + Environment.NewLine);
            }
        }

        
        public static void QuarantineURL(string bodyIn)
        {
            Regex urlRegex = new Regex(@"\S+\.\S+");

            if (File.Exists(@"c:\Users\aidan\Documents\quarantine.json"))
            {
                URLsList urlList = JsonConvert.DeserializeObject<URLsList>(File.ReadAllText(@"c:\Users\aidan\Documents\quarantine.json"));

                // For every URL found in the email body
                foreach (var foundURL in urlRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the URL
                    QuarantinedURL newQuarantine = new QuarantinedURL(foundURL.ToString());
                    urlList.URLs.Add(newQuarantine);

                    // Write the URL list to the JSON file
                    File.WriteAllText(@"c:\Users\aidan\Documents\quarantine.json", JsonConvert.SerializeObject(urlList, Formatting.Indented) + Environment.NewLine);
                }
            }
            // Else create a new file and write to it
            else
            {
                // Create new JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\quarantine.json", "{\"URLs\": []}");
                URLsList urlList = JsonConvert.DeserializeObject<URLsList>(File.ReadAllText(@"c:\Users\aidan\Documents\quarantine.json"));

                // For every URL found in the email body
                foreach (var foundURL in urlRegex.Matches(bodyIn))
                {
                    // Create a new entry in the JSON file for the URL
                    QuarantinedURL newQuarantine = new QuarantinedURL(foundURL.ToString());
                    urlList.URLs.Add(newQuarantine);
                }

                // Write new mentions to the JSON file
                File.WriteAllText(@"c:\Users\aidan\Documents\quarantine.json", JsonConvert.SerializeObject(urlList, Formatting.Indented) + Environment.NewLine);
            }
        }


    }

    public class MsgList
    {
        public List<Message> Messages { get; set; }
    }
}
