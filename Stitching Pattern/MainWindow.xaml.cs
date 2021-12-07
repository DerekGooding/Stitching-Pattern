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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stitching_Pattern
{
    [ValueConversion(typeof(double), typeof(double))]
    public class InverseDoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(double))
            //    throw new InvalidOperationException("The target must be a double, not " + targetType );

            return 100 - (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            currentColor = RandomColor();
            StartTimer();
            SpawnNext();
            SetSeed();
        }

        SolidColorBrush targetColor = Brushes.BlueViolet;
        SolidColorBrush currentColor = Brushes.Red;
        readonly int tickInterval = 5;
        int colorCount = 5;
        private void SetColor()
        {
            var current = ConvertColorToByte(currentColor);
            var target = ConvertColorToByte(targetColor);
            byte incriment = 10;
            for (int i = 0; i < current.Length; i++)
            {
                if (current[i] > target[i])
                {
                    if (current[i] - incriment < current[i])
                        current[i] -= incriment;
                    else
                        current[i] = target[i];
                    if (current[i] < target[i]) current[i] = target[i];
                }
                else if (current[i] < target[i])
                {
                    if (current[i] + incriment > current[i])
                        current[i] += incriment;
                    else
                        current[i] = target[i];
                    if (current[i] > target[i]) current[i] = target[i];
                }
            }
            currentColor = new SolidColorBrush(Color.FromRgb(current[0], current[1], current[2]));
            Console.WriteLine(currentColor.ToString());
            colorCount++;
            if (colorCount >= tickInterval)
            {
                colorCount = 0;
                targetColor = RandomColor();
                Console.WriteLine("Changing Color | " + targetColor.ToString());
            }
        }

        private SolidColorBrush RandomColor()
        {
            byte[] colours = new byte[3];
            rand.NextBytes(colours);
            return new SolidColorBrush(Color.FromRgb(colours[0], colours[1], colours[2]));
        }


        private byte[] ConvertColorToByte(SolidColorBrush colorBrush)
        {
            byte[] output = new byte[3];
            output[0] = colorBrush.Color.R;
            output[1] = colorBrush.Color.G;
            output[2] = colorBrush.Color.B;

            return output;
        }

        private void StartTimer()
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(Tick);
            myTimer.Interval = 1000; // 1000 ms is one second
            myTimer.Enabled = true;
        }

        private void Tick(object o, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                SpawnNext();
                DespawnDots();
                SetColor();
            });
        }


        private readonly int dotDistanceMax = 680;
        private readonly int dotSpacing = 35;
        private readonly int timeInveraval = 20;
        private readonly int lineCount = 25;
        private readonly bool[] seed = new bool[26]; //Needs to be 1 more than lineCount
        private void DespawnDots()
        {
            List<UIElement> removed = new List<UIElement>();
            foreach(UIElement child in PrintArea.Children)
                    if (Canvas.GetLeft(child) > dotDistanceMax)
                        removed.Add(child);
            foreach (var item in removed) 
                PrintArea.Children.Remove(item);
        }

        private void SpawnNext()
        {
            /*for (int i = 0; i < 10; i++)
                SpawnDot(i);*/
            if (RollLine(true))
                for (int i = 0; i < lineCount; i += 2)
                        SpawnLine(i, true);
            else
                for (int i = 1; i < lineCount; i += 2)
                        SpawnLine(i, true);

            for (int i = 0; i < lineCount; i++)
                if (CheckSeed(i))
                    SpawnLine(i, false);
        }

        private readonly Random rand = new Random();
        private bool RollLine(bool vertical)
        {
            double odds;
            if (vertical)
                odds = Slider_Ratio.Value;
            else
                odds = 100 - Slider_Ratio.Value;
            var result = rand.Next(0, 100);
            return result >= odds; 
        }
        /*private void SpawnDot(int y)
        {
            Border dot = new Border
            {
                Width = 5,
                Height = 5,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(2)
            };
            Canvas.SetLeft(dot, 10);
            Canvas.SetTop(dot, y * dotSpacing);
            
            PrintArea.Children.Add(dot);
            AnimateTo(dot);
        }*/

        private void SpawnLine(int y, bool vertical)
        {
            Rectangle line = new Rectangle
            {
                Fill = currentColor
            };
            if (vertical)
            {
                line.Width = 2;
                line.Height = dotSpacing + 2;
            }
            else
            {
                line.Width = dotSpacing;
                line.Height = 2;
            }

            Canvas.SetLeft(line, 10);
            Canvas.SetTop(line, y * dotSpacing);

            PrintArea.Children.Add(line);
            AnimateTo(line);
        }

        private void MarkLine()
        {
            Rectangle line = new Rectangle
            {
                Width = 2,
                Height = dotSpacing * 10,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(line, 10);
            PrintArea.Children.Add(line);
            AnimateTo(line);
        }

        private void AnimateTo(UIElement target)
        {
            int distance = dotSpacing * timeInveraval;
            DoubleAnimation da = new DoubleAnimation
            {
                To = distance,
                Duration = new Duration(new TimeSpan(0, 0, timeInveraval))
            };
            target.BeginAnimation(Canvas.LeftProperty, da);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetSeed();
            MarkLine();
        }

        
        private void SetSeed()
        {
            for (int i = 0; i < lineCount; i++)
                seed[i] = RollLine(false);
        }

        private bool CheckSeed(int i)
        {
            var temp = seed[i];
            seed[i] = !seed[i];
            return temp;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Border_MouseEnter(object sender, MouseEventArgs e) => AnimateOpacity((Border)sender, 1);

        private void Border_MouseLeave(object sender, MouseEventArgs e) => AnimateOpacity((Border)sender, 0.01);

        private void AnimateOpacity(UIElement target, double opacity)
        {
            DoubleAnimation da = new DoubleAnimation
            {
                To = opacity,
                Duration = new Duration(new TimeSpan(0, 0, 0,0,200))
            };
            target.BeginAnimation(OpacityProperty, da);
        }
    }
}
