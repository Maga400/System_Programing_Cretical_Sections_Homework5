using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace System_Programing_Cretical_Sections_Homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Thread> NewThreads { get; set; }
        public ObservableCollection<Thread> WaitingThreads { get; set; }
        public ObservableCollection<Thread> WorkingThreads { get; set; }
        Semaphore Semaphore { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            NewThreads = new ObservableCollection<Thread>();
            WaitingThreads = new ObservableCollection<Thread>();
            WorkingThreads = new ObservableCollection<Thread>();

            Semaphore = new Semaphore(5, 5, "mehemmed");
            DataContext = this;
        }

        private void CreatingNewThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                // news  silinib wait dushmelidi
                //Thread.Sleep(500);

                //Semaphore.WaitOne();

                //// waitden silinib workining
                //Thread.Sleep(5000);


                //Semaphore.Release();


                // workigden silinmelidir


            });

            thread.Name = "Thread " + thread.ManagedThreadId;

            NewThreads.Add(thread);
        }

        private void SendNewThreadsForWaitingThreads(object sender, MouseButtonEventArgs e)
        {
            var th = items3.SelectedItem as Thread;
            if (th != null)
            {
            
                NewThreads.Remove(th);
                WaitingThreads.Add(th);
                
                Thread.Sleep(3000);
                
                Semaphore.WaitOne();
                WorkingThreads.Add(th);
                Thread.Sleep(3000);
                WaitingThreads.Remove(th);
               

                Semaphore.Release();
            }
        }
    }
}