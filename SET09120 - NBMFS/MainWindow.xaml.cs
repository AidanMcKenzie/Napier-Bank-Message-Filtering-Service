using System;
using System.Collections.Generic;
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


namespace SET09120___NBMFS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            header = txtHeader.Text;
            messageType = header.Substring(0, 1).ToUpper();
            msgSender = txtSender.Text;
            subject = txtSubject.Text; //For emails only
            emailType = header.Substring(0, 3).ToUpper();
            body = txtBody.Text;


            if (header.Length == 9)
            {
                switch (messageType)
                {
                    case "S":
                        MessageBox.Show("SMS!");

                        if (body.Length > 0 && body.Length <= 140)
                        {
                            MessageBox.Show(body);
                            // Sender must be intl phone number
                            // Create SMS object (id, header, sender, body)
                            SMS sms = new SMS(1, header, msgSender, body);
                            // Convert textspeak
                            // Output to file in JSON format
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the SMS body is between 0 and 140 characters.");
                        }
                        break;
                    case "E":
                        MessageBox.Show("Email!");

                        if (body.Length > 0 && body.Length <= 1028)
                        {
                            if (emailType.Equals("SIR"))
                            {
                                MessageBox.Show("SIR Email!");
                            }
                            else
                            {
                                MessageBox.Show("Standard Email!");
                            }

                            MessageBox.Show(body);
                            // if subject is in form "SIR dd/mm/yy"
                            // Sender must be standard email address
                            // Create email object (id, header, sender, body)
                                    //Email email = new Email(1, header, msgSender, body);
                            // Quarantine URLs
                            // Output to file in JSON format
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the email body is between 0 and 1028 characters.");
                        }

                        break;
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
    }
}
