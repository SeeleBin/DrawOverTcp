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
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DrawTcp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        
        private int _portNum;

        public int PortNum { get => _portNum; set => _portNum = value; }
        /// <summary>
        /// 
        /// </summary>
        public void connectionCreate()
        {
            Socket socket=null;
            TcpListener server = null;
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _portNum = int.Parse(PortTxt.Text);
                    InfoGather.IsEnabled = false;
                    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                    server = new TcpListener(localAddr, _portNum);
                    server.Start();
                    socket = server.AcceptSocket();
                    while (socket == null)
                    {
                        waitInfo.Visibility = Visibility.Visible;
                    }
                    this.Hide();
                    MainWindow mw = new MainWindow(socket);
                    mw.ShowDialog();

                }));
                
                // Enter the listening loop.
                
               
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Errore", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /*finally
            {
                // Stop listening for new clients.
                server.Stop();
            }*/

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoGather_Click(object sender, RoutedEventArgs e)
        {
            
            Thread thread = new Thread(connectionCreate);
            thread.Start();
        }
    }
}
