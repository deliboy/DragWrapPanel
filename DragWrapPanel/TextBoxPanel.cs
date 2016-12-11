using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DragWrapPanel
{
    public class TextBoxPanel : DragPanel<CustomMenu>
    {
        public TextBoxPanel()
        {
            Init();
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

        private void HotKeyRemove(OrakaiMenu sender, Key key)
        {
            var list = this.Menus.Where(x => x != sender && x.HotKey == (int)key);

            foreach (var l in list)
            {
                l.SetHotKey(Key.None);
            }
        }
    }
}