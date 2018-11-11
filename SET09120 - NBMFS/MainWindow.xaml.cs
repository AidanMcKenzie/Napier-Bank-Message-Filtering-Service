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
//using System.Windows.Forms;
using Newtonsoft.Json;


namespace SET09120___NBMFS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declare strings used for text boxes
        public string header;
        public string messageType;
        public string msgSender;
        public string subject;
        public string emailType;
        public string body;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        
        // Application logic for when the user clicks the 'Send Message' button
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            header = txtHeader.Text;
            messageType = header.Substring(0, 1).ToUpper();
            msgSender = txtSender.Text;
            subject = txtSubject.Text; //For emails only
            emailType = header.Substring(0, 3).ToUpper();
            body = txtBody.Text;

            // Header must be a letter followed by nine numbers
            if (header.Length == 10)
            {
                // Begin case statement for different message types
                switch (messageType)
                {
                    // If header begins with 'S', message is an SMS
                    case "S":
                        MessageBox.Show("SMS!");

                        if (body.Length > 0 && body.Length <= 140)
                        {
                            MessageBox.Show(body);
                            // Sender must be intl phone number
                            // Create SMS object (id, header, sender, body)
                            SMS sms  = new SMS(header, msgSender, body);
                            // Convert textspeak

                            // serialize JSON to a string and then write string to a file
                            
                            if (File.Exists(@"c:\Users\aidan\Documents\messages.json"))
                            {
                                File.AppendAllText(@"c:\Users\aidan\Documents\messages.json", JsonConvert.SerializeObject(sms, Formatting.Indented) + Environment.NewLine);
                            }
                            else
                            {
                                File.WriteAllText(@"c:\Users\aidan\Documents\messages.json", JsonConvert.SerializeObject(sms, Formatting.Indented));
                            }

                            MessageBox.Show("Written to file!");
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the SMS body is between 0 and 140 characters.");
                        }
                        break;

                    // If header begins with 'E', message is an Email
                    case "E":
                        MessageBox.Show("Email!");

                        if (body.Length > 0 && body.Length <= 1028)
                        {
                            if (emailType.Equals("SIR"))
                            {
                                MessageBox.Show("SIR Email!");
                                
                                // Create email object (id, header, sender, body)
                                //Email email = new Email(1, header, msgSender, body);
                                // Quarantine URLs
                                // Output to file in JSON format
                            }
                            else
                            {
                                MessageBox.Show("Standard Email!");

                                // Create email object (id, header, sender, body)
                                //Email email = new Email(1, header, msgSender, body);
                                // Quarantine URLs
                                // Output to file in JSON format
                            }

                            MessageBox.Show(body);
                            // Sender must be standard email address
                            
                            
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the email body is between 0 and 1028 characters.");
                        }

                        break;

                    // If header begins with 'T', message is a Tweet
                    case "T":
                        MessageBox.Show("Tweet!");

                        if (body.Length > 0 && body.Length <= 140)
                        {
                            MessageBox.Show(body);
                            // Sender must be twitter ID
                            // Create Tweet object (id, header, sender, body)
                            //Tweet tweet = new Tweet(1, header, msgSender, body);
                            // Convert textspeak, add to hashtag list, add to sender list
                            // Output to file in JSON format
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

        private void txtHeader_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (e)

            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                    //...
            }*/
        }
    }
}
