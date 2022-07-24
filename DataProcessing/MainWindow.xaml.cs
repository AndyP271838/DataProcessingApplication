using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Galileo6;
using DataProcessing.Test;
using System.Reflection;


namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Declaration Global Variables
        public static LinkedList<double> SensorA = new LinkedList<double>();
        public static LinkedList<double> SensorB = new LinkedList<double>();

        public MainWindow()
        {
            InitializeComponent();
            upDownMu.Text = "50";
            upDownSig.Text = "10";
        }

        //Load Data Method, return void no solution, uses loop to populate linked list to size not greater than 400

        public void LoadData()
        {
            ReadData dataReader = new ReadData();
            for (int i = 0; i < 400; i++)
            {
                SensorA.AddFirst(dataReader.SensorA(Double.Parse(upDownMu.Text), Double.Parse(upDownSig.Text)));
                SensorB.AddFirst(dataReader.SensorB(Double.Parse(upDownMu.Text), Double.Parse(upDownSig.Text)));
            }


        //Show all sensors method, display both LinkedLists in a ListView
        public void ShowAllSensorData()
        {
            lstViewStaticDisplay.Items.Add(item);
            //ListViewItem
            //lstViewStaticDisplay.
            //for (int i = 0; i < SensorA.Count; i++)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Content = SensorA;
            //    lstViewStaticDisplay.Items.Add(new ListViewItem());
            //}
        }

        //Call LoadData method, and ShowAllSensorData methods, no parameters
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
        }

        //Show all sensors method, display both LinkedLists in a ListView
        public void ShowAllSensorData()
        {
            
        }

        //Call LoadData method, and ShowAllSensorData methods, no parameters
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Temporarily Commented out in order to test custome class
            LoadData();
            ShowAllSensorData();
            */
    
            DataClass test = new DataClass(Double.Parse(upDownMu.Text), Double.Parse(upDownSig.Text));
            DataClassIEnumerable enumerable = new DataClassIEnumerable(test);
            
            lstViewStaticDisplay.ItemsSource = enumerable;
 
            lblCounter.Content = lstViewStaticDisplay.Items.Count.ToString();
        }

    

    }
}
