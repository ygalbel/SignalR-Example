using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace SignalR.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IHubProxy drawHubProxy;

        public MainWindow()
        {
            InitializeComponent();
            var hubConnection = new HubConnection("http://localhost:17158/");
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = new DebugTextWriter();
             drawHubProxy = hubConnection.CreateHubProxy("Draw");
/*            stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock =>
                Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));*/

             hubConnection.Start(new LongPollingTransport()).Wait();
        }

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            if (ellipse != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Debug.WriteLine("HERE " + Mouse.GetPosition(this).X +"," +Mouse.GetPosition(this).Y);
                DragDrop.DoDragDrop(ellipse,
                                     Mouse.GetPosition(this),
                                     DragDropEffects.None);
            }
        }



        private void ellipse_DragOver(object sender, DragEventArgs e)
        {
            var source = e.OriginalSource as UIElement;
            Ellipse ellipse = sender as Ellipse;
           // Canvas.GetLeft(Mouse);
            var p2 = Mouse.GetPosition(source);
            Canvas.SetLeft(ellipse, p2.X);
            Canvas.SetTop(ellipse, p2.Y);
            Debug.WriteLine(p2.X + " " + p2.Y + "\t" + Canvas.GetLeft(ellipse) + " " + Canvas.GetTop(ellipse));
        }

        private void LeftClick(object sender, RoutedEventArgs e)
        {
            drawHubProxy.Invoke("SetDeltaPosition", new { x = -5, y = 0 });
        }
        private void RightClick(object sender, RoutedEventArgs e)
        {
            drawHubProxy.Invoke("SetDeltaPosition", new { x = 5, y = 0 });
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            drawHubProxy.Invoke("SetDeltaPosition", new { x = 0, y = -5 });
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            drawHubProxy.Invoke("SetDeltaPosition", new { x = 0, y = +5 });
        }
    }


    public class DebugTextWriter : TextWriter
    {
        public override void WriteLine(string value)
        {
            Debug.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            Debug.WriteLine(value);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            Debug.WriteLine(format, arg);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
