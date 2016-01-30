using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DragWrapPanel
{
    public class TextBoxPanel : DragPanel<Label>
    {
        public TextBoxPanel()
        {
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 100; i++)
            {
                this.Add(new Label()
                {
                    Content = i.ToString(),
                    Width = 60,
                    BorderThickness = new Thickness(1, 1, 1, 1),
                    BorderBrush = Brushes.Gray
                });
            }
        }
    }
}