using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DragWrapPanel
{
    public class OrakaiMenu : INotifyPropertyChanged
    {
        //public event EventHandler<HotKeyBindEventArgs> HotKeyBinded;
        private string _hotKeyAlias;

        public event PropertyChangedEventHandler PropertyChanged;

        public int HotKey { get; set; }

        public string HotKeyAlias
        {
            get
            {
                return _hotKeyAlias;
            }
            set
            {
                _hotKeyAlias = value;
                OnPropertyChange("HotKeyAlias");
            }
        }

        public string IconPath { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public void SetHotKey(Key key)
        {
            if (key == Key.None)
            {
                this.HotKey = (int)Key.None;
                this.HotKeyAlias = string.Empty;
            }

            Key f2 = Key.F2;
            Key f12 = Key.F12;

            Key d0 = Key.D0;
            Key z = Key.Z;

            if ((key >= f2 && key <= f12) || (key >= d0 && key <= z))
            {
                HotKey = (int)key;
                HotKeyAlias = "(" + key.ToString() + ")";
            }
        }

        private void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}