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
using System.Net;
using System.Threading;

namespace DrawTcp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket1;
        public MainWindow(Socket socket)
        {
            InitializeComponent();
            socket1=socket;
            Listen();
        }
        
        public void WinGame(string answer)
        {
            if (OldwordsTxt.Items.Count > 0)
            {
                if (answer == OldwordsTxt.Items[0].ToString().ToUpper())
                {
                    MessageBox.Show("Hai indovinato correttamente");
                    Byte[] data = Encoding.ASCII.GetBytes("EndGame");
                    socket1.Send(data);
                    drawingCanvas.Children.Clear();
                }
            }
        }

        private void drawLine(string x, string y, string color, string size)
        {
            string ascissa, ordinata, colorazione,grandezza;
            
            ascissa = x;
            ordinata = y;
            colorazione = color;
            grandezza = size;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Ellipse ellipse = new Ellipse();
                switch(colorazione)
                {
                    case "#FF000000":
                        ellipse.Fill = Brushes.Black;
                        break;
                    case "#FFFF0000":
                        ellipse.Fill = Brushes.Red;
                        break;
                    case "#FFFFFF00":
                        ellipse.Fill = Brushes.Yellow;
                        break;
                    case "#FF0000FF":
                        ellipse.Fill = Brushes.Blue;
                        break;
                    case "#FFFFFFFF":
                        ellipse.Fill = Brushes.White;
                        break;
                }
                ellipse.Width = double.Parse(grandezza);
                ellipse.Height = double.Parse(grandezza);
                Canvas.SetTop(ellipse, float.Parse(ordinata));
                Canvas.SetLeft(ellipse, float.Parse(ascissa));
                drawingCanvas.Children.Add(ellipse);
            }));
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
                            if (message.Contains("Cancella")) 
                                drawingCanvas.Children.Clear();
                            else if (message=="EndGame")
                            {
                                WinGame(message.Split('!')[4]);
                            }
                            else
                            {
                                drawLine(message.Split('!')[0], message.Split('!')[1], message.Split('!')[2], message.Split('!')[3]);
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

        private void checkAnswBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GuessTxt.Text != "")
                OldwordsTxt.Items.Add(GuessTxt.Text);
            
        }
    }
}
