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
    /// Interaction logic for HashtagList.xaml
    /// </summary>
    public partial class HashtagList : Window
    {
        public HashtagList()
        {
            InitializeComponent();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HashList hashtagList = JsonConvert.DeserializeObject<HashList>(File.ReadAllText(@"c:\Users\aidan\Documents\hashtags.json"));

            List<Hashtag> hashlist = new List<Hashtag>();

            // Loop through Hashtag list
            foreach (Hashtag hashtag in hashtagList.Hashtags)
            {
                lstHashtagList.Items.Add("Hashtag: " + hashtag.hashtag + "\nCount: " + hashtag.count);
            }
            
            //TODO: Order this list
        }
    }
}
