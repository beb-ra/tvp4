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

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        Model model;
        private Ellipse[,] ellipses;

        public MainWindow()
        {
            InitializeComponent();
            model = new Model(10, 10);
            ellipses = new Ellipse[10, 10];
            CreateGameBoard();
        }

        private void CreateGameBoard()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    model.Init();

                    Ellipse ellipse = new Ellipse();
                    if (row == 0 && col == 0) 
                    { 
                        ellipse.Fill = Brushes.Red; 
                    }
                    else
                    {
                        if (row == 9 && col == 9)
                        {
                            ellipse.Fill = Brushes.Blue;
                        }
                        else
                        {
                            ellipse.Fill = Brushes.White;
                        }
                    }
                    ellipse.MouseLeftButtonDown += Cell_MouseLeftButtonDown;

                    ellipse.Width = 50;
                    ellipse.Height = 50;

                    Grid.SetRow(ellipse, row);
                    Grid.SetColumn(ellipse, col);

                    grid.Children.Add(ellipse);
                    ellipses[row, col] = ellipse;
                }
            }
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            int row = Grid.GetRow(ellipse);
            int col = Grid.GetColumn(ellipse);

            if (model.CanMove(row, col, model.whoWalks))
            {
                AddVirus(row, col);
            }

            if (model.IsFinall())
            {
                this.Close();
            }
        }

        private void AddVirus(int row, int col)
        {
            if (ellipses[row, col] != null)
            {
                model.AddObject(row, col);

                if (model.field[row, col] == 1) 
                {
                    ellipses[row, col].Fill = Brushes.Red;
                }
                if (model.field[row, col] == 2)
                {
                    ellipses[row, col].Fill = Brushes.Blue;
                }

                if (model.field[row, col] == 3)
                {
                    ellipses[row, col].Fill = Brushes.Red;
                    ellipses[row, col].Width = 100;
                    ellipses[row, col].Height = 100;
                    ellipses[row, col].HorizontalAlignment = HorizontalAlignment.Center;
                    ellipses[row, col].VerticalAlignment = VerticalAlignment.Center;   
                }
                if (model.field[row, col] == 4)
                {
                    ellipses[row, col].Fill = Brushes.Blue;
                    ellipses[row, col].Width = 100;
                    ellipses[row, col].Height = 100;
                    ellipses[row, col].HorizontalAlignment = HorizontalAlignment.Center;
                    ellipses[row, col].VerticalAlignment = VerticalAlignment.Center;
                }
            }
        }

    }
}
