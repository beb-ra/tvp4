using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;


namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        Random rnd;
        DispatcherTimer timer;
        private int RectanglesAtStart = 20;

        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
            CreateRectangles(RectanglesAtStart);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!CheckWin())
                CreateRectangles(1);
        }

        private void CreateRectangles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 200;
                rect.Height = 100;

                int x = rnd.Next((int)(this.Width - rect.Width - 50));
                int y = rnd.Next((int)(this.Height - rect.Height - 50));

                rect.Margin = new Thickness(x, y, 0, 0);
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;

                byte r = (byte)rnd.Next(255);
                byte g = (byte)rnd.Next(255);
                byte b = (byte)rnd.Next(255);

                rect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                rect.Stroke = Brushes.Black;

                rect.MouseDown += Rect_MouseDown;

                grid.Children.Add(rect);
            }
        }

        private bool CheckWin()
        {
            if (grid.Children.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Вы победили!");
                return true;
            }
            if (grid.Children.Count == RectanglesAtStart * 2)
            {
                timer.Stop();
                MessageBox.Show("Вы проиграли");
                return true;
            }
            return false;
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckWin();
            Rectangle rect = (Rectangle)sender;
            Rect r1 = new Rect(rect.Margin.Left, rect.Margin.Top, rect.Width, rect.Height);

            bool over = true;

            int iRect = grid.Children.IndexOf(rect);

            for (int i = iRect + 1; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] is Rectangle)
                {
                    Rectangle otherRect = (Rectangle)grid.Children[i];
                    Rect r2 = new Rect(otherRect.Margin.Left, otherRect.Margin.Top, otherRect.Width, otherRect.Height);

                    // Проверка, перекрывает ли grid.Children[i] rect сверху
                    if (r1.IntersectsWith(r2))
                    {
                        over = false;
                        break;
                    }
                }
            }
            if (over)
            {
                grid.Children.Remove(rect);
            }
        }
    }
}
               
