using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace DragWrapPanel
{
    public class TextBoxPanel : DragPanel<CustomMenu>
    {
        public TextBoxPanel()
        {
            Init();
            //   KeyDown += TextBoxPanel_KeyDown;
        }

        public ObservableCollection<OrakaiMenu> Menus { get; set; }

        private void Init()
        {
            Menus = new ObservableCollection<OrakaiMenu>();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("./icons");

            var files = di.GetFiles();

            foreach (var img in files)
            {
                Menus.Add(new OrakaiMenu
                {
                    Name = img.Name,
                    IconPath = img.FullName
                });

                //Image myImage = new Image();
                //myImage.Width = 48;
                //myImage.Height = 48;
                //BitmapImage myImageSource = new BitmapImage();
                //myImageSource.BeginInit();
                //myImageSource.UriSource = new Uri(img.FullName);
                //myImageSource.EndInit();
                //myImage.Source = myImageSource;

                //   Add(myImage);
            }

            foreach (var i in Menus)
            {
                var c = new CustomMenu()
                {
                    DataContext = i
                };
                c.KeyDown += TextBoxPanel_KeyDown;
                Add(c);
            }
     
        }

        private void TextBoxPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var sender2 = sender as FrameworkElement;
                if (sender2 != null)
                {
                    var dc = sender2.DataContext as OrakaiMenu;
                    dc.SetHotKey(e.Key);
                    HotKeyRemove(dc, e.Key);
                }
            }
        }

        private void HotKeyRemove( OrakaiMenu sender,  Key key)
        {
            var list = this.Menus.Where(x => x != sender && x.HotKey == (int)key);

            foreach(var l in list)
            {
                l.SetHotKey(Key.None);
            }
        }
    }
}