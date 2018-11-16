using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Runtime.Serialization;


namespace SET09120___NBMFS
{
    public partial class MainWindow : Window
    {
        // Declare strings used for text boxes
        public string header;
        public string messageType;
        public string msgSender;
        public string subject;
        public string emailType;
        public string body;

        public static MsgList messageList;

        // Create dictionary from the textwords CSV file
        //public static Dictionary<string, string> textwordsList = File.ReadLines("c:\Users\aidan\Documents\textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);


        public MainWindow()
        {
            InitializeComponent();
        }


        // Application logic for when the user clicks the 'Send Message' button
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            header = txtHeader.Text;
            if (!string.IsNullOrEmpty(header))
            {
                messageType = header.Substring(0, 1).ToUpper();
            }

            msgSender = txtSender.Text;
            subject = txtSubject.Text;
            if (!string.IsNullOrEmpty(subject))
            {
                emailType = subject.Substring(0, 3).ToUpper();
            }

            body = txtBody.Text;

            // Header must be a letter followed by nine numbers
            if (header.Length == 10)
            {
                // Begin case statement for different message types
                switch (messageType)
                {
                    // If header begins with 'S', message is an SMS
                    case "S":
                        // If the body text is between 1 and 140 characters
                        if (body.Length > 0 && body.Length <= 140)
                        {
                            // Create SMS object (id, header, sender, body)
                            Message sms = new Message(header, msgSender, subject, body);

                            WriteMessageToFile(sms);
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the SMS body is between 0 and 140 characters.");
                        }
                        break;

                    // If header begins with 'E', message is an Email
                    case "E":
                        if (body.Length > 0 && body.Length <= 1028)
                        {
                            if (emailType.Equals("SIR"))
                            {
                                MessageBox.Show("SIR Email");
                                // Create email object (id, header, sender, body)
                                Message email = new Message(header, msgSender, subject, body);
                                WriteMessageToFile(email);

                                // Quarantine URLs
                            }
                            else
                            {
                                MessageBox.Show("Standard email");
                                // Create email object (id, header, sender, body)
                                Message email = new Message(header, msgSender, subject, body);
                                WriteMessageToFile(email);

                                // Quarantine URLs
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the email body is between 0 and 1028 characters.");
                        }

                        break;

                    // If header begins with 'T', message is a Tweet
                    case "T":
                        if (body.Length > 0 && body.Length <= 140)
                        {
                            // Sender must be twitter ID
                            // Create Tweet object
                            Message tweet = new Message(header, msgSender, subject, body);
                            WriteMessageToFile(tweet);

                            // Convert textspeak, add to hashtag list, add to sender list
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the Tweet body is between 0 and 140 characters.");
                        }
                        break;

                    // If header begins with any character other than 'S', 'E' or 'T', alert the user
                    default:
                        MessageBox.Show("Please enter a valid message header.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please enter a message header that is a single character followed by nine numbers. \n e.g.: 'E123456789'");
            }
        }


        private void btnSIR_Click(object sender, RoutedEventArgs e)
        {
            Window SIRList = new SIRList();
            SIRList.Show();
        }

        private void btnTrending_Click(object sender, RoutedEventArgs e)
        {
            //Page TrendingList = new TrendingList();
            //TrendingList.Show();
        }

        private void btnMentions_Click(object sender, RoutedEventArgs e)
        {
            Window MentionList = new MentionList();
            MentionList.Show();
        }

        private void txtHeader_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHeader.Text))
            {
                // --consider either removing these or the public versions of these

                header = txtHeader.Text;
                messageType = header.Substring(0, 1).ToUpper();
                
                switch (messageType)
                {
                    // If header begins with 'S', message is an SMS
                    case "S":
                        btnSend.IsEnabled = true;
                        txtSubject.Visibility = Visibility.Hidden;
                        lblSubject.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "SMS";

                        break;

                    case "E":
                        btnSend.IsEnabled = true;
                        txtSubject.Visibility = Visibility.Visible;
                        lblSubject.Visibility = Visibility.Visible;

                        blkMsgType.Text = "Email";
                        break;

                    case "T":
                        btnSend.IsEnabled = true;
                        txtSubject.Visibility = Visibility.Hidden;
                        lblSubject.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "Tweet";
                        break;

                    case "":
                        //btnSend.IsEnabled = false;
                        blkMsgType.Text = "Empty";
                        break;

                    default:
                        btnSend.IsEnabled = false;
                        txtSubject.Visibility = Visibility.Hidden;
                        lblSubject.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "";
                        break;
                }

            }
            else
            {
                txtSubject.Visibility = Visibility.Hidden;
                lblSubject.Visibility = Visibility.Hidden;
                blkMsgType.Text = "";
            }
        }

        private void WriteMessageToFile(Message msgIn)
        {
            if (File.Exists(@"c:\Users\aidan\Documents\messages.json"))
            {
                messageList = JsonConvert.DeserializeObject<MsgList>(File.ReadAllText(@"c:\Users\aidan\Documents\messages.json"));
                messageList.Messages.Add(msgIn);

                File.WriteAllText(@"c:\Users\aidan\Documents\messages.json", JsonConvert.SerializeObject(messageList, Formatting.Indented) + Environment.NewLine);

                clearFields();
            }
            // Else create a new file and write to it
            else
            {
                File.WriteAllText(@"c:\Users\aidan\Documents\messages.json", "{\"messages\": []}");

                messageList = JsonConvert.DeserializeObject<MsgList>(File.ReadAllText(@"c:\Users\aidan\Documents\messages.json"));
                messageList.Messages.Add(msgIn);

                File.WriteAllText(@"c:\Users\aidan\Documents\messages.json", JsonConvert.SerializeObject(messageList, Formatting.Indented) + Environment.NewLine);

                clearFields();
            }
        }

        private void clearFields()
        {
            txtHeader.Clear();
            txtSubject.Clear();
            txtSender.Clear();
            txtBody.Clear();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubject.Visibility = System.Windows.Visibility.Hidden;
            lblSubject.Visibility = System.Windows.Visibility.Hidden;
        }
    }

    /*whenWindowLoadsEvent()
    {
        System.IO.Directory.CreateDirectory(NMBFS);
    }*/
}
