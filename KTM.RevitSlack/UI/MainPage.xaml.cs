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
using Autodesk.Revit.UI;

namespace KTM.RevitSlack.UI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, Autodesk.Revit.UI.IDockablePaneProvider
    {
        private DockPosition m_position = DockPosition.Right;
        private Guid m_targetGuid;

        public MainPage(string slackChannel)
        {
            InitializeComponent();
            this.slackbrowser.Navigate(slackChannel);
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            DockablePaneProviderData d = new DockablePaneProviderData();

            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = m_position;
            DockablePaneId targetPane;
            targetPane = m_targetGuid == Guid.Empty ? null : new DockablePaneId(m_targetGuid);
            

        }

        private void UpdateUI()
        {
            //fetch the new url
        }
    }
}
