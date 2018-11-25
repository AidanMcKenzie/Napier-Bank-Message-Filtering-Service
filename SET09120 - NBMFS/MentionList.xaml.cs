using Newtonsoft.Json;
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

namespace SET09120___NBMFS
{
    /// <summary>
    /// Interaction logic for MentionList.xaml
    /// </summary>
    public partial class MentionList : Window
    {
        public MentionList()
        {
            InitializeComponent();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MentionsList mentList = JsonConvert.DeserializeObject<MentionsList>(File.ReadAllText(@"c:\Users\aidan\Documents\mentions.json"));

            // Loop through Mention list
            foreach (Mention mention in mentList.Mentions)
            {
                lstMentions.Items.Add("Mention: " + mention.mention);
            }

            //TODO: Order this list
        }
    }
}
