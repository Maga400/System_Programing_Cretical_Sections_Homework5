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
    public partial class MainWindow : Window, INotifyPropertyChanged
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


            DataContext = this;
        }

        private void CreatingNewThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread((obj) =>
            {
                var th = obj as Thread;
                Dispatcher dispatcher = Dispatcher;


                Semaphore.WaitOne();
                Thread.Sleep(1000);
                App.Current.Dispatcher.Invoke(() => { WaitingThreads.Remove(th!); });
                Thread.Sleep(500);
                App.Current.Dispatcher.Invoke(() => { WorkingThreads.Add(th!); });
                Thread.Sleep(1000);

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
            App.Current.Dispatcher.Invoke(() => { NewThreads.Add(thread); });


        }

        private void SendNewThreadsForWaitingThreads(object sender, MouseButtonEventArgs e)
        {

            var th = items3.SelectedItem as Thread;
            if (th != null)
            {
                int value = Convert.ToInt32(text);
                if (value == 0)
                {
                    MessageBox.Show("Semaphore ucun places yeni value daxil edin", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    Semaphore = new Semaphore(value, value, "Semaphore");

                }
                App.Current.Dispatcher.Invoke(() => { NewThreads.Remove(th); ; });
                App.Current.Dispatcher.Invoke(() => { WaitingThreads.Add(th); });

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
            WorkingThreads.Remove(th!);
        }
    }
}