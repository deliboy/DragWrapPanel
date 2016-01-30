using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DragWrapPanel
{
    public class DragPanel<T> : WrapPanel where T : UIElement
    {
        private ObservableCollection<T> _items;

        public DragPanel()
        {
            this.AllowDrop = true;
        }

        public ObservableCollection<T> Items
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;

                foreach (var t in value)
                {
                    Add(t);
                }
            }
        }

        public void Add(T item)
        {
            if (_items == null)
            {
                _items = new ObservableCollection<T>();
            }

            item.MouseMove += T_MouseMove;
            item.DragEnter += T_DragEnter;
            item.DragLeave += T_DragLeave;
            item.DragOver += T_DragOver;
            item.Drop += T_Drop;
            item.MouseDown += T_MouseDown;
            item.AllowDrop = true;
            _items.Add(item);
            this.Children.Add(item);
        }

        private void T_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(T)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void T_DragLeave(object sender, DragEventArgs e)
        {
        //    Drag(sender, e);

        }

        private void T_DragOver(object sender, DragEventArgs e)
        {
            Drag(sender, e);

        }

        private void T_Drop(object sender, DragEventArgs e)
        {
      //      Drag(sender, e);
        }

        private void Drag(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(T));

            if (source == sender)
            {
                return;
            }
            this.Children.Remove((T)source);
            var index = this.Children.IndexOf((T)sender);
            this.Children.Insert(index--, (T)source);
        }

        private void T_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop((DependencyObject)sender, sender, DragDropEffects.Move);
            }
        }

        private void T_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }
    }
}