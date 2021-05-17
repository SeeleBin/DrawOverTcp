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

namespace PaintTcp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// 
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private string _ipAdd;
        private int _portNum;

        public string IpAdd { get => _ipAdd; set => _ipAdd = value; }
        public int PortNum { get => _portNum; set => _portNum = value; }
        /// <summary>
        /// 
        /// </summary>
        public void connectionCreate()
        {
            Socket socket = null;
            TcpClient client = null;
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _portNum = int.Parse(PortTxt.Text);
                    if (IPTxt.Text.Split('.').Length == 4 && int.Parse(IPTxt.Text.Split('.')[0]) < 255 && int.Parse(IPTxt.Text.Split('.')[1])
                    < 255 && int.Parse(IPTxt.Text.Split('.')[2]) < 255 && int.Parse(IPTxt.Text.Split('.')[3]) < 255)
                    {
                        _ipAdd = IPTxt.Text;
                        client = new TcpClient(_ipAdd, _portNum);
                        socket = client.Client;
                        this.Hide();
                        MainWindow mw = new MainWindow(socket);
                        mw.ShowDialog();
                    }
                }));
                
                

                // Enter the listening loop.

                
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Errore", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoGather_Click(object sender, RoutedEventArgs e)
        {
            waitInfo.Visibility = Visibility.Visible;
            Thread thread = new Thread(connectionCreate);
            thread.Start();
        }
    }
}
