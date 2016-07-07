using System;
using System.Windows;
using KTM.RevitSlack.API;

namespace KTM.RevitSlack.UI
{
    /// <summary>
    /// Interaction logic for ctrlProgress.xaml
    /// </summary>
    public partial class ctrlProgress : Window
    {
        public ctrlProgress(string searchTerm)
        {
            InitializeComponent();
            var giphy = Api.RandomGiphyUrl(searchTerm);
            if (null != giphy)
            {
                browser.Source = new Uri(giphy["image_url"].ToString());
                browser.MaxWidth = Double.Parse(giphy["image_width"].ToString());
                browser.MaxHeight = Double.Parse(giphy["image_height"].ToString());
            }
            else
            {
                browser.Visibility = Visibility.Hidden;
            }
        }
    }
}
