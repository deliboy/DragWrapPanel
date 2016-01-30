using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DragWrapPanel
{
    public class DragPanel<T>:WrapPanel where T : UIElement
    {
        public DragPanel()
        {
            this.AllowDrop = true;
        }
        private ObservableCollection<T> _items;

        public ObservableCollection<T> Items
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;

                foreach(var t in value)
                {
                    Add(t);
                    
                    
                    
                }
            }
        }

       

        private void T_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //System.Windows.Point position = e.GetPosition(this);
            //double pX = position.X;
            //double pY = position.Y;

            //// Sets the Height/Width of the circle to the mouse coordinates.
            //ellipse.Width = pX;
            //ellipse.Height = pY;
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

        private void T_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
          //  DataObject dragData = new DataObject("myFormat", contact);
            DragDrop.DoDragDrop((DependencyObject)sender, sender, DragDropEffects.Move);
        }

        private void T_Drop(object sender, DragEventArgs e)
        {

            var source = e.Data.GetData(typeof(T));

            if(source == sender)
            {
                return;
            }
            this.Children.Remove((T)source);
            var index = this.Children.IndexOf((T)sender);
            this.Children.Insert(index--, (T)source);
        }

        private void T_DragOver(object sender, DragEventArgs e)
        {
        }

        private void T_DragLeave(object sender, DragEventArgs e)
        {
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
    }

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
                    Content = i.ToString()   ,
                    Width = 60,
                    BorderThickness = new Thickness(1, 1, 1, 1)   ,
                    BorderBrush = Brushes.Gray
                });
            }
        }
    }
}
