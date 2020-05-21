using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for ReportViewUI.xaml
    /// </summary>
    public partial class ReportViewUI : Window
    {
        public ReportViewUI()
        {
            InitializeComponent();

            //----------Buoc 1
            var sidepanel = crystalReportsViewer.FindName("btnToggleSidePanel") as ToggleButton;
            if (sidepanel != null)
            {
                crystalReportsViewer.ViewChange += (x, y) => sidepanel.IsChecked = false;

            }
        }
        // Buoc 2-----------------------------------------------------------------------------
        public void setReportSource(CrystalDecisions.CrystalReports.Engine.ReportDocument aReport)
        {
            //  crystalReportsViewer.ViewerCore.ToggleGroupTree = true; dang con loi

            this.crystalReportsViewer.ViewerCore.ReportSource = aReport;

        }

    }
}
