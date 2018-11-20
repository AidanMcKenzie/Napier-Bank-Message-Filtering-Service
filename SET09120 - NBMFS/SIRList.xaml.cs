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

            foreach(SIR sirNow in incidentList.Incidents)
            {
                lstSIR.Items.Add(sirNow.header + "\n" + sirNow.incident + "\n" + sirNow.sortcode + "\n" + sirNow.subject);
            }
        }
    }
}
