/*
* Name: Riley de Gans
* Date: May 27th, 2019
* Description: A game that simulates life with a grid
*/
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
using System.Drawing;
using System.Media;
using System.IO;
using System.Windows.Threading;

namespace _184863GameOfLife3._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[][] grid;
        bool[][] gridLive;
        bool?[][] newLive;
        bool gameStart = false;
        Button btnPlay;
        //Label lblPrompt;
        int counter = 0;
        DispatcherTimer gameTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            btnPlay = new Button();
            btnPlay.BorderBrush = Brushes.Black;
            btnPlay.Content = "Click me to play";
            btnPlay.Height = 50;
            btnPlay.Width = 400;
            btnPlay.FontSize = 30;
            btnPlay.Click += playGame;
            Canvas.SetTop(btnPlay, 600);
            Canvas.SetLeft(btnPlay, 0);
            canvas.Children.Add(btnPlay);
            grid = new Button[100][];
            gridLive = new bool[100][];
            newLive = new bool?[100][];
            for (int i = 0; i < 100; i++)
            {
                grid[i] = new Button[100];
                gridLive[i] = new bool[100];
                newLive[i] = new bool?[100];
            }
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    gridLive[x][y] = false;
                    grid[x][y] = new Button();
                    grid[x][y].Height = 6;
                    grid[x][y].Width = 6;
                    grid[x][y].Background = Brushes.White;
                    grid[x][y].BorderBrush = Brushes.Black;
                    grid[x][y].BorderThickness = new Thickness(1);
                    grid[x][y].Click += clickEvent;
                    Canvas.SetTop(grid[x][y], y * 6);
                    Canvas.SetLeft(grid[x][y], x * 6);
                    canvas.Children.Add(grid[x][y]);
                }
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    bool?[] neighbours = new bool?[8];
                    try
                    {
                        neighbours[0] = gridLive[x + 1][y];
                    }
                    catch
                    {
                        neighbours[0] = null;
                    }
                    try
                    {
                        neighbours[1] = gridLive[x - 1][y];
                    }
                    catch
                    {
                        neighbours[1] = null;
                    }
                    try
                    {
                        neighbours[2] = gridLive[x][y + 1];
                    }
                    catch
                    {
                        neighbours[2] = null;
                    }
                    try
                    {
                        neighbours[3] = gridLive[x][y - 1];
                    }
                    catch
                    {
                        neighbours[3] = null;
                    }
                    try
                    {
                        neighbours[4] = gridLive[x + 1][y + 1];
                    }
                    catch
                    {
                        neighbours[4] = null;
                    }
                    try
                    {
                        neighbours[5] = gridLive[x + 1][y - 1];
                    }
                    catch
                    {
                        neighbours[5] = null;
                    }
                    try
                    {
                        neighbours[6] = gridLive[x - 1][y + 1];
                    }
                    catch
                    {
                        neighbours[6] = null;
                    }
                    try
                    {
                        neighbours[7] = gridLive[x - 1][y - 1];
                    }
                    catch
                    {
                        neighbours[7] = null;
                    }
                    int neighbourCount = 0;
                    for (int i = 0; i < neighbours.Length; i++)
                    {
                        if (neighbours[i] == true)
                        {
                            neighbourCount++;
                        }
                    }
                    if (neighbourCount < 2 || neighbourCount > 3)
                    {
                        newLive[x][y] = false;
                    }
                    else if (neighbourCount == 3)
                    {
                        newLive[x][y] = true;
                    }
                }
            }
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    try
                    {
                        if (newLive[x][y] == true)
                        {

                            grid[x][y].Background = Brushes.Red;
                            grid[x][y].BorderBrush = Brushes.Black;
                        }
                        else if (newLive[x][y] == false)
                        {
                            grid[x][y].Background = Brushes.White;
                            grid[x][y].BorderBrush = Brushes.Black;
                        }
                        gridLive[x][y] = (bool)(newLive[x][y]);
                        newLive[x][y] = null;
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void clickEvent(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (gameStart == false)
            {
                for (int x = 0; x < 100; x++)
                {
                    for (int y = 0; y < 100; y++)
                    {
                        if (grid[x][y] == button)
                        {
                            if (gridLive[x][y] == false)
                            {
                                gridLive[x][y] = true;
                                grid[x][y].Background = Brushes.Black;
                                grid[x][y].BorderBrush = Brushes.White;
                            }
                            else if (gridLive[x][y])
                            {
                                gridLive[x][y] = false;
                                grid[x][y].Background = Brushes.White;
                                grid[x][y].BorderBrush = Brushes.Black;
                            }

                        }
                    }
                }
            }
        }
        public void playGame(object sender, EventArgs e)
        {
            gameStart = true;
            gameTimer.Start();
        }
    }
}