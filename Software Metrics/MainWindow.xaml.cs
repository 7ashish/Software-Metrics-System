using Backend;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Software_Metrics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFP FPCalculator;
        public MainWindow()
        {
            FPCalculator = FPFactory.CreateFP(FPImplementation.BasicFP);
            for(int i = 0; i < ComplexitySet.Length; i++)
            {
                ComplexitySet[i] = false;
            }
            InitializeComponent();
        }
        private readonly int[] Complexity = new int[15];
        private readonly bool[] ComplexitySet = new bool[15];
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            string[] arr = radioButton.Name.Split('x');
            int criteria = int.Parse(arr[0].Substring(1));
            int complexity = int.Parse(arr[1]);
            Complexity[criteria] = complexity;
            ComplexitySet[criteria] = true;
            foreach(var oBjEqT in fpGrid.Children)
            {
                if(oBjEqT is TextBox t && !string.IsNullOrWhiteSpace(t.Text) && t.Name.Substring(1) == arr[0].Substring(1))
                {
                    if(t.Name[0] == 'T')
                        t.Text =  (radioButton.IsChecked ?? false) ? FPCalculator.GetUFPMultiplier(criteria / 3, complexity).ToString() : "0";
                    else if(t.Name[0] == 'I')
                    {
                        FPCalculator.SetUFPValue(criteria, complexity, int.Parse(t.Text));
                    }
                }
                if(oBjEqT is RadioButton c && c != radioButton && c.Name.Split('x')[0].Substring(1) == arr[0].Substring(1))
                {
                    c.IsChecked = false;
                }
            }
        }
        private void RadioButtonUnchecked(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            string[] arr = radioButton.Name.Split('x');
            int criteria = int.Parse(arr[0].Substring(1));
            int complexity = int.Parse(arr[1]);
            FPCalculator.SetUFPValue(criteria, complexity, 0);
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int criteria = int.Parse(t.Name.Substring(1));
            try
            {
                if (!ComplexitySet[criteria]) return;
                FPCalculator.SetUFPValue(criteria, Complexity[criteria], int.Parse(t.Text));
                t.Background = Brushes.Transparent;
            }
            catch (FormatException) 
            {
                t.Background = Brushes.Red;
            }
        }
        private void CalculateUFPButton_Click(object sender, RoutedEventArgs e)
        {
            UFPCount.Text = FPCalculator.CalculateUFP().ToString();
        }
        private readonly static int[][] AddButtonCollapsedItems = new int[5][]
        {
            new int[2]{ 1, 2 },
            new int[2]{ 4, 5 },
            new int[2]{ 7, 8 },
            new int[2]{ 10, 11 },
            new int[2]{ 13, 14 }
        };
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int i = int.Parse(b.Name.Substring(1));
            b.Content = (string)b.Content == "+" ? "-" : "+";
            foreach(var control in fpGrid.Children)
            {
                int j = 0;
                if(control is RadioButton c)
                {
                    j = int.Parse(c.Name.Split('x')[0].Substring(1));
                }
                else if(control is FrameworkElement t && !string.IsNullOrWhiteSpace(t.Name) && t.Name[0] != 'B')
                {
                    j = int.Parse(t.Name.Substring(1));
                }
                if(AddButtonCollapsedItems[i].Contains(j))
                {
                    FrameworkElement t = (FrameworkElement)control;
                    t.Visibility = t.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }
        private void CalculateDI_Click(object sender, RoutedEventArgs e)
        {
            int res = 0;
            foreach(var control in Stackk.Children)
            {
                if(control is ComboBox c)
                {
                   res += c.SelectedIndex;
                }
            }
            DI.Text = res.ToString();
            FPCalculator.SetDI(res);
        }
        private void Calculate_TCT_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DI.Text))
            {
                MessageBox.Show("Please Insert The DI Value or Manually Calculate it by inserting the Degree of the 14 Factors");
            }
            else
            {
                TCF.Text = FPCalculator.CalculateTCF().ToString();
            }
        }
        private void Calculate_FP_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TCF.Text) || string.IsNullOrWhiteSpace(UFPCount.Text))
            {
                MessageBox.Show("Please Calculate the Values of UFP and TCF First");
            }
            else
            {
                FP.Text = FPCalculator.CalculateFP().ToString();
            }
        }
        private void AVC_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var RadioButton = (RadioButton)sender;
            var RadioButtonValue = RadioButton.Content.ToString();
            FPCalculator.SetAverageLineOfCodes(int.Parse(RadioButtonValue));
        }
        private void Calculate_LOC_Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FP.Text))
            {
                MessageBox.Show("Please Calculate FP and Select the Programming Language First");
            }
            else
            {
                LOC.Text = FPCalculator.CalculateLOC().ToString();
            }
        }
        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach(var control in Stackk.Children)
            {
                if(control is ComboBox c)
                {
                    c.SelectedIndex = CB0.SelectedIndex;
                }
            }
        }
        private void SelectAllComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (SelectAll == null) return;
            if (SelectAll.IsChecked == true)
            {
                SelectAll_Checked(null, null);
            }
        }

    }
}


