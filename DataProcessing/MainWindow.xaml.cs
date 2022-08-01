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
using System.Reflection;
using System.Diagnostics;

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
        #region Methods
        public void LoadData()
        {
            int dataSet = 400;

            SensorA.Clear();
            SensorB.Clear();

            ReadData dataReader = new ReadData();
            for (int i = 0; i < dataSet; i++)                                               //FOR TEST PURPOSES modify 400 to 10
            {
                SensorA.AddFirst(dataReader.SensorA(Double.Parse(upDownMu.Text), Double.Parse(upDownSig.Text)));
                SensorB.AddFirst(dataReader.SensorB(Double.Parse(upDownMu.Text), Double.Parse(upDownSig.Text)));
            }
        }

        //Show all sensors method, display both LinkedLists in a ListView
        public void ShowAllSensorData()
        {
            for (int a = 0; a < SensorA.Count; a++)
            {
                lstViewStaticDisplay.Items.Add(NewRow(a));
            }
        }

        //Method for creating new string array's
        private String[] NewRow(int indexA)
        {
            string[] rowArray = new String[2];

            rowArray[0] = SensorA.ElementAt(indexA).ToString();
            rowArray[1] = SensorB.ElementAt(indexA).ToString();
            return rowArray;
        }

        //Number of nodes method, output is equal to the number of nodes present in an LinkedList
        public int NumberOfNodes(LinkedList<double> mLink)  //lstViewStaticDisplay.Items.Add(NewRow());
        {
            return mLink.Count();
        }

        //Display the content of a linked list inside of a list box
        public void DisplayListBoxData(LinkedList<double> mLink, ListBox listBoxName)
        {
            foreach (double i in mLink)
            {
                listBoxName.Items.Add(i);
            }
        }

            #region Sort And Search Methods
        //Selection Sort Method
        public bool SelectionSort(LinkedList<double> mLink)
        {
            int min = 0;
            int max = NumberOfNodes(mLink);

            for (int i = 0; i < max; i++)
            {
                min = i;
                for (int j = i + 1; j < max; j++)
                {
                    if (mLink.ElementAt(j) < mLink.ElementAt(min))
                    {
                        min = j;
                    }
                }

                LinkedListNode<double> currentMin = mLink.Find(mLink.ElementAt(min));
                LinkedListNode<double> currentI = mLink.Find(mLink.ElementAt(i));

                var temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;

            }

            return true;
        }

        //Insertion Sort Method
        public bool InsertionSort(LinkedList<double> mLink)
        {
            int max = NumberOfNodes(mLink);

            for (int i = 0; i < (max - 1); i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (mLink.ElementAt(j-1) > mLink.ElementAt(j))
                    {
                        LinkedListNode<double> current = mLink.Find(mLink.ElementAt(j));
                        LinkedListNode<double> previous = mLink.Find(mLink.ElementAt(j - 1));
                        double temp = current.Value;
                        current.Value = previous.Value;
                        previous.Value = temp;
                    }
                }
            }

            return true;
        }

        //Iterative Binary Search Method
        public int BinarySearchIterative(LinkedList<double> mLink, double searchValue, int minimum, int maximum)
        {
            while (minimum <= (maximum - 1))
            {
                int middle = (minimum + maximum) / 2 ;

                if (searchValue.Equals(mLink.ElementAt(middle)))    //Convert Double to Int in line
                {
                    return middle;      //Removed ++middle, cheaky trick
                }
                else if (searchValue < mLink.ElementAt(middle))
                {
                    maximum = middle - 1;
                }
                else
                {
                    minimum = middle + 1;
                }
            }
            return minimum;
        }

        //Recursive Binary Search Method
        public int BinarySearchRecursive(LinkedList<double> mLink, double searchValue, int minimum, int maximum)
        {
            if (minimum <= maximum -1)
            {
                int middle = (minimum + maximum) / 2;
                if (searchValue.Equals(mLink.ElementAt(middle)))    //Convert Double to Int in line
                {
                    return middle;
                }
                else if (searchValue < mLink.ElementAt(middle))     //Convert Double to Int in line
                {
                    return BinarySearchRecursive(mLink, searchValue, minimum, middle - 1);
                }
                else
                {
                    return BinarySearchRecursive(mLink, searchValue, middle + 1, maximum);
                }
            }

            return minimum;
        }

        //Select's Data in a Range from the List Box
        public void SelectData(ListBox myBox, int result)
        {
            myBox.SelectedItems.Clear();
            //Select Rows Below Search Resutl
            for (int a = 0; a < 3; a++)
            {
                //Rows Above
                if ((result - a) > -1)
                {
                    myBox.SelectedItems.Add(myBox.Items[result - a]);
                }
            }
            myBox.SelectedItems.Add(result);
            for (int a = 0; a < 3; a++)
            {
                //Rows Below
                if ((result + a) <= myBox.Items.Count - 1)
                {
                    myBox.SelectedItems.Add(myBox.Items[result + a]);
                }
            }
        }

        //Returns a boolean, checks if the value entered into the search boc is out of range
        public bool RangeCheck(LinkedList<double> myList, double input)
        {
            if (input <= myList.Max() && input >= myList.Min())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
            #endregion
        #endregion
        #region Event Handlers
        //Call LoadData method, and ShowAllSensorData methods, no parameters
        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            lstViewStaticDisplay.Items.Clear();
            lstBoxSensA.Items.Clear();
            lstBoxSensB.Items.Clear();
            LoadData();
            ShowAllSensorData();
            DisplayListBoxData(SensorA, lstBoxSensA);
            DisplayListBoxData(SensorB, lstBoxSensB);
        }
        
        //Sensor A Iterative Search
        private void btnIterativeSearchA_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearchA.Text))
            {
                if (SelectionSort(SensorA) && RangeCheck(SensorA, Double.Parse(txtSearchA.Text)))
                {
                    lstBoxSensA.Items.Clear();
                    Stopwatch sw = Stopwatch.StartNew();
                    int result = BinarySearchIterative(SensorA, Double.Parse(txtSearchA.Text), 0, NumberOfNodes(SensorA));
                    sw.Stop();
                    txtIteA.Text = String.Format("{0} Ticks", sw.Elapsed.Ticks.ToString()); // Should be ticks
                    DisplayListBoxData(SensorA, lstBoxSensA);
                    SelectData(lstBoxSensA, result);

                    //User Feedback
                    lstBoxSensA.Focus();
                    lstBoxSensA.ScrollIntoView(lstBoxSensA.SelectedItems[lstBoxSensA.SelectedItems.Count - 1]);
                    lstBoxSensA.ScrollIntoView(lstBoxSensA.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show("Search value outside range of dataset");
                }
            }
            else
            {
                MessageBox.Show("Search Field is Empty");
                txtSearchA.Focus();
            }

        }

        //Sensor A Recursive search
        private void btnRecA_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearchA.Text))
            {
                if (SelectionSort(SensorA) && RangeCheck(SensorA, Double.Parse(txtSearchA.Text)))
                {
                    lstBoxSensA.Items.Clear();
                    Stopwatch sw = Stopwatch.StartNew();
                    int result = BinarySearchRecursive(SensorA, Double.Parse(txtSearchA.Text), 0, NumberOfNodes(SensorA));
                    sw.Stop();

                    txtRecA.Text = String.Format("{0} Ticks", sw.Elapsed.Ticks.ToString());
                    DisplayListBoxData(SensorA, lstBoxSensA);
                    SelectData(lstBoxSensA, result);

                    //User Feedback
                    lstBoxSensA.Focus();
                    lstBoxSensA.ScrollIntoView(lstBoxSensA.SelectedItems[lstBoxSensA.SelectedItems.Count - 1]);
                    lstBoxSensA.ScrollIntoView(lstBoxSensA.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show("Search value outside range of dataset");
                }
            }
            else
            {
                MessageBox.Show("Search Field is Empty");
                txtSearchA.Focus();
            }
        }

        //Sensor A Selection Sort
        private void btnSelSortA_Click(object sender, RoutedEventArgs e)
        {
            lstBoxSensA.Items.Clear();
            Stopwatch sw = Stopwatch.StartNew();
            SelectionSort(SensorA);
            sw.Stop();
            txtSelSortA.Text = String.Format("{0} Milliseconds", sw.Elapsed.Milliseconds.ToString());
            ShowAllSensorData();
            DisplayListBoxData(SensorA, lstBoxSensA);
            
        }

        //Sensor A Insertion Sort
        private void btnInsSortA_Click(object sender, RoutedEventArgs e)
        {
            lstBoxSensA.Items.Clear();
            Stopwatch sw = Stopwatch.StartNew();
            InsertionSort(SensorA);
            sw.Stop();
            txtInsSortA.Text = String.Format("{0} Milliseconds", sw.Elapsed.Milliseconds.ToString());
            ShowAllSensorData();
            DisplayListBoxData(SensorA, lstBoxSensA);
        }

        //Sensor B Iterative Search
        private void btnIterativeSearchB_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearchB.Text))
            {
                if (SelectionSort(SensorB) && RangeCheck(SensorB, Double.Parse(txtSearchB.Text)))
                {
                    lstBoxSensB.Items.Clear();
                    Stopwatch sw = Stopwatch.StartNew();
                    int result = BinarySearchIterative(SensorB, Double.Parse(txtSearchB.Text), 0, NumberOfNodes(SensorB));
                    sw.Stop();
                    txtIteB.Text = String.Format("{0} Ticks", sw.Elapsed.Ticks.ToString());
                    DisplayListBoxData(SensorB, lstBoxSensB);
                    SelectData(lstBoxSensB, result);
                    
                    //User Feedback
                    lstBoxSensB.Focus();
                    lstBoxSensB.ScrollIntoView(lstBoxSensB.SelectedItems[lstBoxSensB.SelectedItems.Count - 1]);
                    lstBoxSensB.ScrollIntoView(lstBoxSensB.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show("Search value outside range of dataset!");
                }
            }
            else
            {
                MessageBox.Show("Search Field is Empty");
                txtSearchB.Focus();
            }
        }

        //Sensor B recursive search
        private void btnRecB_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearchB.Text))
            {
                if (SelectionSort(SensorB) && RangeCheck(SensorB, Double.Parse(txtSearchB.Text)))
                {
                    lstBoxSensB.Items.Clear();
                    Stopwatch sw = Stopwatch.StartNew();
                    int result = BinarySearchRecursive(SensorB, Double.Parse(txtSearchB.Text), 0, NumberOfNodes(SensorB));
                    sw.Stop();
                    txtRecB.Text = String.Format("{0} Ticks", sw.Elapsed.Ticks.ToString());
                    DisplayListBoxData(SensorB, lstBoxSensB);
                    SelectData(lstBoxSensB, result);

                    //User Feedback
                    lstBoxSensB.Focus();
                    lstBoxSensB.ScrollIntoView(lstBoxSensB.SelectedItems[lstBoxSensB.SelectedItems.Count-1]);
                    lstBoxSensB.ScrollIntoView(lstBoxSensB.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show("Search value outside range of dataset!");
                }
            }
            else
            {
                MessageBox.Show("Search Field is Empty");
                txtSearchB.Focus();
            }
        }

        //Sensor B Selection Sort
        private void btnSelSortB_Click(object sender, RoutedEventArgs e)
        {
            lstBoxSensB.Items.Clear();
            Stopwatch sw = Stopwatch.StartNew();
            SelectionSort(SensorB);
            sw.Stop();
            txtSelSortB.Text = String.Format("{0} Milliseconds", sw.Elapsed.Milliseconds.ToString());
            ShowAllSensorData();
            DisplayListBoxData(SensorB, lstBoxSensB);
        }

        //Sensor B Insertion Sort
        private void btnInsSortB_Click(object sender, RoutedEventArgs e)
        {
            lstBoxSensB.Items.Clear();
            Stopwatch sw = Stopwatch.StartNew();
            InsertionSort(SensorB);
            sw.Stop();
            txtInsSortB.Text = String.Format("{0} Milliseconds", sw.Elapsed.Milliseconds.ToString());
            ShowAllSensorData();
            DisplayListBoxData(SensorB, lstBoxSensB);
        }

        //Event Handler for input type in Search A Text Box Input
        private void txtSearchA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.CompareTo(".") == 0)
            {
                e.Handled = false;
            }
            else if (Double.TryParse(e.Text,out  double input))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //Event Handler for input type in Search A Text Box Input
        private void txtSearchB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.CompareTo(".") == 0)
            {
                e.Handled = false;
            }
            else if (Double.TryParse(e.Text, out double input))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
    #endregion
}