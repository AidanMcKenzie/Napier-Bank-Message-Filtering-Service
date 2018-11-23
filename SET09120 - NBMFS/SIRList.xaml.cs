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
using System.ComponentModel;
using Newtonsoft.Json;

namespace SET09120___NBMFS
{
    /// <summary>
    /// Interaction logic for SIRList.xaml
    /// </summary>
    public partial class SIRList : Window
    {
        public static IncidentReportList incidentList;

        public SIRList()
        {
            InitializeComponent();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IncidentReportList incidentList = JsonConvert.DeserializeObject<IncidentReportList>(File.ReadAllText(@"c:\Users\aidan\Documents\sir.json"));
            MsgList messageList = JsonConvert.DeserializeObject<MsgList>(File.ReadAllText(@"c:\Users\aidan\Documents\messages.json"));

            // Loop through SIR emails
            foreach (SIR sir in incidentList.Incidents)
            {
                // Loop through Messages
                foreach (Message message in messageList.Messages)
                {
                    // If there is a Message with an associated SIR email, display in the listbox
                    if (message.Header == sir.header)
                    {
                        lstSIR.Items.Add("Header: " + sir.header + "\n" + "Incident: " + sir.incident + "\n" + "Sort Code: " + sir.sortcode + "\n" + "Subject: " + sir.subject + "\n" + "Body: " + message.Body);
                    }
                }
            }
        }
    }
}
