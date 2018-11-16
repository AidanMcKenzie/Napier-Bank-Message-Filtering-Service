using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
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

        string headerRegex = "";
        string smsSenderRegex = @"^(\+[1-9][0-9]*(\([0-9]*\)|-[0-9]*-))?[0]?[1-9][0-9\- ]*$";
        string emailSenderRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        string tweetSenderRegex = @"\A([0-9a-zA-Z_]{1,15})|(@([0-9a-zA-Z_]{1,15}))\Z";
        string sirRegex = @"((\d{2})|(\d))\/((\d{2})|(\d))\/((\d{4})|(\d{2}))";
        string sortcodeRegex = @"^(\d){2}-(\d){2}-(\d){2}$";



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
            string sortcode = txtSortCode.Text;

            // Header must be a letter followed by nine numbers
            if (header.Length == 10)
            {
                // Begin case statement for different message types
                switch (messageType)
                {
                    // If header begins with 'S', message is an SMS
                    case "S":
                        
                        Match smsSenderValid = Regex.Match(msgSender, smsSenderRegex);

                        if (smsSenderValid.Success && msgSender.Length <= 15)
                        {
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
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the sender for this SMS is a valid international phone number.");
                        }
                        break;

                    // If header begins with 'E', message is an Email
                    case "E":
                        
                        //if sender is a valid email address
                        Match senderValid = Regex.Match(msgSender, emailSenderRegex);

                        if (senderValid.Success)
                        {
                            if (subject.Length > 0 && subject.Length <= 20)
                            {
                                if (body.Length > 0 && body.Length <= 1028)
                                {
                                    Match detectSIR = Regex.Match(subject, sirRegex);

                                    if (detectSIR.Success)
                                    {
                                        Match sortcodeValid = Regex.Match(sortcode, sortcodeRegex);

                                        if (sortcodeValid.Success)
                                        {
                                            MessageBox.Show("SIR Email");

                                            Message email = new Message(header, msgSender, subject, body);
                                            WriteMessageToFile(email);

                                            // Quarantine URLs
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please ensure sort code is in the format XX-XX-XX.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Standard email");

                                        Message email = new Message(header, msgSender, subject, body);
                                        WriteMessageToFile(email);

                                        // Quarantine URLs
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please ensure the email body is between 0 and 1028 characters.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please ensure the email subject is 20 characters or less.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the sender field is a valid email address (e.g. johnsmith@email.com, jane.smith@email.co.uk");
                        }

                        break;

                    // If header begins with 'T', message is a Tweet
                    case "T":

                        Match tweeterValid = Regex.Match(msgSender, tweetSenderRegex);

                        if (tweeterValid.Success)
                        {
                            // if sender is @ followed by 15 characters
                            if (body.Length > 0 && body.Length <= 140)
                            {
                                if (msgSender.Substring(0,1) != "@" && msgSender.Length < 15)
                                {
                                    msgSender = "@" + msgSender;
                                    MessageBox.Show("Added @ to handle");
                                }
                                
                                // Create Tweet object
                                Message tweet = new Message(header, msgSender, subject, body);
                                WriteMessageToFile(tweet);

                                // Convert textspeak, add to hashtag list, add to sender list
                            }
                            else
                            {
                                MessageBox.Show("Please ensure the Tweet body is between 0 and 140 characters.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the Tweet sender is a maximum of 15 characters.");
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
            Window HashtagList = new HashtagList();
            HashtagList.Show();
        }

        private void btnMentions_Click(object sender, RoutedEventArgs e)
        {
            Window MentionList = new MentionList();
            MentionList.ShowDialog();
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

                        txtSortCode.Visibility = Visibility.Hidden;
                        lblSortCode.Visibility = Visibility.Hidden;

                        cmbIncident.Visibility = Visibility.Hidden;
                        lblIncident.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "SMS";

                        blkMaxCharCount.Text = "140";

                        break;

                    case "E":
                        btnSend.IsEnabled = true;
                        txtSubject.Visibility = Visibility.Visible;
                        lblSubject.Visibility = Visibility.Visible;

                        txtSortCode.Visibility = Visibility.Visible;
                        lblSortCode.Visibility = Visibility.Visible;

                        cmbIncident.Visibility = Visibility.Visible;
                        lblIncident.Visibility = Visibility.Visible;

                        blkMsgType.Text = "Email";
                        blkMaxCharCount.Text = "1028";

                        break;

                    case "T":
                        btnSend.IsEnabled = true;
                        txtSubject.Visibility = Visibility.Hidden;
                        lblSubject.Visibility = Visibility.Hidden;

                        txtSortCode.Visibility = Visibility.Hidden;
                        lblSortCode.Visibility = Visibility.Hidden;

                        cmbIncident.Visibility = Visibility.Hidden;
                        lblIncident.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "Tweet";
                        blkMaxCharCount.Text = "140";
                        break;

                    case "":
                        //btnSend.IsEnabled = false;
                        blkMsgType.Text = "Empty";
                        break;

                    default:
                        btnSend.IsEnabled = false;
                        txtSubject.Visibility = Visibility.Hidden;
                        lblSubject.Visibility = Visibility.Hidden;

                        txtSortCode.Visibility = Visibility.Hidden;
                        lblSortCode.Visibility = Visibility.Hidden;

                        cmbIncident.Visibility = Visibility.Hidden;
                        lblIncident.Visibility = Visibility.Hidden;

                        blkMsgType.Text = "";
                        blkMaxCharCount.Text = "";
                        break;
                }

            }
            else
            {
                txtSubject.Visibility = Visibility.Hidden;
                lblSubject.Visibility = Visibility.Hidden;

                txtSortCode.Visibility = Visibility.Hidden;
                lblSortCode.Visibility = Visibility.Hidden;

                cmbIncident.Visibility = Visibility.Hidden;
                lblIncident.Visibility = Visibility.Hidden;

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
            txtSender.Clear();
            txtSubject.Clear();
            txtSortCode.Clear();
            //cmbIncident.ClearValue();
            txtBody.Clear();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubject.Visibility = Visibility.Hidden;
            lblSubject.Visibility = Visibility.Hidden;

            txtSortCode.Visibility = Visibility.Hidden;
            lblSortCode.Visibility = Visibility.Hidden;

            cmbIncident.Visibility = Visibility.Hidden;
            lblIncident.Visibility = Visibility.Hidden;

            blkCurCharCount.Text = txtBody.Text.Length.ToString();
            blkMaxCharCount.Text = "140";
        }

        private void TxtBody_TextChanged(object sender, TextChangedEventArgs e)
        {
            float charCount = txtBody.Text.Length;
            float maxCount = Int32.Parse(blkMaxCharCount.Text);

            float percentile = (charCount / maxCount) * 100;

            blkCurCharCount.Text = charCount.ToString();

            if (percentile > 100)
            {
                blkCurCharCount.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                blkCurCharCount.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TxtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (subject.Substring(0, 3).ToUpper() == "SIR")
            {
                txtSortCode.Visibility = Visibility.Visible;
                lblSortCode.Visibility = Visibility.Visible;

                cmbIncident.Visibility = Visibility.Visible;
                lblIncident.Visibility = Visibility.Visible;
            }
            else
            {
                txtSortCode.Visibility = Visibility.Hidden;
                lblSortCode.Visibility = Visibility.Hidden;

                cmbIncident.Visibility = Visibility.Hidden;
                lblIncident.Visibility = Visibility.Hidden;
            }*/
        }
    }

    /*whenWindowLoadsEvent()
    {
        System.IO.Directory.CreateDirectory(NMBFS);
    }*/
}
