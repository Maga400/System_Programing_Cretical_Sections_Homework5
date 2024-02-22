using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace System_Programing_Cretical_Sections_Homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public ObservableCollection<Thread> NewThreads { get; set; }
        public ObservableCollection<Thread> WaitingThreads { get; set; }
        public ObservableCollection<Thread> WorkingThreads { get; set; }
        public Semaphore Semaphore { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
                    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindow()
        {
            InitializeComponent();
            NewThreads = new ObservableCollection<Thread>();
            WaitingThreads = new ObservableCollection<Thread>();
            WorkingThreads = new ObservableCollection<Thread>();
            Semaphore = new Semaphore(2, 2, "Semaphore");

            DataContext = this;
        }

        private void CreatingNewThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread((obj) =>
            {
                var th = obj as Thread;
                Dispatcher dispatcher = Dispatcher;
                int value = Convert.ToInt32(text);
                Semaphore = new Semaphore(value,value, "Semaphore");
                
                Thread.Sleep(1000);
                Semaphore.WaitOne();
                dispatcher.Invoke(() =>
                {

                    WorkingThreads.Add(th!);
                    WaitingThreads.Remove(th);

                });
                Semaphore.Release();

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
                th.Start(th);
                //Semaphore.WaitOne();
                //WorkingThreads.Add(th);
                //Thread.Sleep(3000);
                //WaitingThreads.Remove(th);


                //Semaphore.Release();
            }
        }

        private void DeleteThreadsFromWorkingThreads(object sender, MouseButtonEventArgs e)
        {
            var th = items.SelectedItem as Thread;
            WorkingThreads.Remove(th);
        }
    }
}