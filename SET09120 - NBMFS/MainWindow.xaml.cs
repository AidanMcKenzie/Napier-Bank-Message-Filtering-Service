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
        public string sender;
        public string subject;
        public string body;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        /*private void txtHeader_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }*/

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            header = txtHeader.Text;
            messageType = header.Substring(0, 1).ToUpper();
            sender = txtSender.Text;
            //subject = txtSubject.Text
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
                        }
                        else
                        {
                            MessageBox.Show("Please ensure the SMS body is between 0 and 140 characters.");
                        }
                        break;
                    case "E":
                        MessageBox.Show("Email!");
                        break;
                    case "T":
                        MessageBox.Show("Tweet!");
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
