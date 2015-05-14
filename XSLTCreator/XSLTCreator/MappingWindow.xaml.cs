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
using System.Windows.Shapes;

namespace XSLTCreator
{
    /// <summary>
    /// Interaction logic for MappingWindow.xaml
    /// </summary>
    public partial class MappingWindow : Window
    {
        MainWindow mainwindow;
        List<XsltColumn> XsltColumns ;

        public MappingWindow(Window mw)
        {
              
            InitializeComponent();
            mainwindow = mw as MainWindow;
            this.Closed +=MappingWindow_Closed;
            XsltColumns = new List<XsltColumn>();
        }

        private void MappingWindow_Closed(object sender, EventArgs e)
        {
            mainwindow.WindowState = WindowState.Normal;
            mainwindow.w = null;
        }


        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                //HandleFileOpen(files[0]);
                if (files[0].LastIndexOf(".xsd") > 0)
                    FileReader.GetXsltColumns(ref XsltColumns,files[0]);
                else
                    MessageBox.Show("File not supported");

                XsltColumns.Sort();
                NeededColumns.ItemsSource = null;
                NeededColumns.ItemsSource = XsltColumns;
                CacheHandler.GetInstance().Listofcolumns = XsltColumns;
            }
        }
    }
}
