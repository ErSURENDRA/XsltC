using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace XSLTCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MappingWindow w;
        public MainWindow()
        {
            InitializeComponent();
            XLSView.Visibility = Visibility.Hidden;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (w == null)
                w = new MappingWindow(window);
            w.Show();
            window.WindowState = WindowState.Minimized;

        }

        private void Window_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files[0].LastIndexOf(".xls") > 0)
                {
                    try
                    {
                        DataView dv = FileReader.ReadExcelFile(files[0]);
                        if (dv != null)
                        {
                            if (XLSView.ItemsSource != null)
                                XLSView.ItemsSource = null;

                            XLSView.ItemsSource = dv;
                            XLSView.Visibility = Visibility.Visible;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show("File not supported");
            }

        }

    }
}