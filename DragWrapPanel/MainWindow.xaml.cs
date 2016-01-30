using System.Windows;

namespace DragWrapPanel
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }
    }
}