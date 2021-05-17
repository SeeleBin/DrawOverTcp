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
using System.Net.Sockets;

namespace PaintTcp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentSize = (int)Sizes.small;
        private Brush currentColor = Brushes.Black;
        private Point sendPos;
        private bool activePaint=false;
        private Socket socket1;
        private string answer;

        private enum Sizes
        {
            small=10,
            medium=15,
            large=20
        }

        private void drawLine(Brush color,Point mousePos)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = color;
            ellipse.Width = currentSize;
            ellipse.Height = currentSize;
            Canvas.SetTop(ellipse, mousePos.Y);
            Canvas.SetLeft(ellipse, mousePos.X);
            drawingCanvas.Children.Add(ellipse);
            Byte[] data = Encoding.ASCII.GetBytes(sendPos.X + "!" + sendPos.Y + "!" + color.ToString() + "!" + currentSize + "!" + answer + "!");
            socket1.Send(data);
        }

        public MainWindow(Socket socket)
        {
            InitializeComponent();
            socket1 = socket;
            Listen();
        }

        private async void Listen()
        {

            await Task.Run(() =>
            {
                int i;
                byte[] bytes = new byte[32];
                while (socket1 != null)
                {
                    try
                    {
                        string message = null;
                        if ((i = socket1.Receive(bytes)) != 0)
                        {
                            message = Encoding.ASCII.GetString(bytes);
                            if (message.Contains("EndGame"))
                            {
                                MessageBox.Show("L'utente ha indovinato!");
                                startBtn.IsEnabled = true;
                                drawingCanvas.Children.Clear();
                                drawingCanvas.IsEnabled = false;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            });

        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (activePaint == true)
            {
                sendPos = e.GetPosition(drawingCanvas);
                drawLine(currentColor, sendPos);
            }
        }

        private void BlackPen_Checked(object sender, RoutedEventArgs e)
        {
            currentColor = Brushes.Black;
        }

        private void RedPen_Checked(object sender, RoutedEventArgs e)
        {
            currentColor = Brushes.Red;
        }

        private void BluePen_Checked(object sender, RoutedEventArgs e)
        {
            currentColor = Brushes.Blue;
        }

        private void YellowPen_Checked(object sender, RoutedEventArgs e)
        {
            currentColor = Brushes.Yellow;
        }

        private void SmallPen_Checked(object sender, RoutedEventArgs e)
        {
            currentSize = (int)Sizes.small;
        }

        private void Medium_Checked(object sender, RoutedEventArgs e)
        {
            currentSize = (int)Sizes.medium;
        }

        private void BigPen_Checked(object sender, RoutedEventArgs e)
        {
            currentSize = (int)Sizes.large;
        }

        private void deleteDrawing_Click(object sender, RoutedEventArgs e)
        {
            int cont = drawingCanvas.Children.Count;
            
            if (cont > 0)
            {
                Byte[] data = Encoding.ASCII.GetBytes("Cancella");
                drawingCanvas.Children.Clear();
                socket1.Send(data);
            }
                
        }

        private void drawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            activePaint = true;
        }

        private void drawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            activePaint = false;
        }

        private void EraserPen_Checked(object sender, RoutedEventArgs e)
        {
            currentColor = Brushes.White;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if(drawName.Text != "")
            {
                drawingCanvas.IsEnabled = true;
                answer = drawName.Text.ToUpper();
                startBtn.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Mettere una parola da far indovinare!");
            }
                
        }
    }
}

