using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;

namespace StarPRNTSDK
{
    public class SelectedPrinter : INotifyPropertyChanged
    {
        static private string _ModelName;
        static private string _MacAddress;
        static private string _PortName;

        static private string _BackupPrinterModelName;
        static private string _BackupPrinterMacAddress;
        static private string _BackupPrinterPortName;

        static private bool _IsSelectedMainPrinter;

        public event PropertyChangedEventHandler PropertyChanged;

        public SelectedPrinter()
        {
        }

        public SelectedPrinter(string ModelName, string PortName, string MacAddress, bool isMainPriner)
        {
            this.IsSelectedMainPrinter = isMainPriner;

            if (isMainPriner)
            {
                this.ModelName = ModelName;
                this.MacAddress = MacAddress;
                this.PortName = PortName;
            }
            else
            {
                this.BackupPrinterModelName = ModelName;
                this.BackupPrinterMacAddress = MacAddress;
                this.BackupPrinterPortName = PortName;
            }

        }


        protected void OnPropertyChaned([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string ModelName
        {
            get
            {
                return _ModelName;
            }

            set
            {
                if (value == _ModelName)
                {
                    return;
                }

                _ModelName = value;
                OnPropertyChaned();
            }
        }

        public string MacAddress
        {
            get
            {
                return _MacAddress;
            }

            set
            {
                if (value == _MacAddress)
                {
                    return;
                }

                _MacAddress = value;
                OnPropertyChaned();
            }
        }

        public string PortName
        {
            get
            {
                return _PortName;
            }

            set
            {
                if (value == _PortName)
                {
                    return;
                }

                _PortName = value;
                OnPropertyChaned();
            }
        }

        public string BackupPrinterModelName
        {
            get
            {
                return _BackupPrinterModelName;
            }

            set
            {
                if (value == _BackupPrinterModelName)
                {
                    return;
                }

                _BackupPrinterModelName = value;
                OnPropertyChaned();
            }
        }

        public string BackupPrinterMacAddress
        {
            get
            {
                return _BackupPrinterMacAddress;
            }

            set
            {
                if (value == _BackupPrinterMacAddress)
                {
                    return;
                }

                _BackupPrinterMacAddress = value;
                OnPropertyChaned();
            }
        }

        public string BackupPrinterPortName
        {
            get
            {
                return _BackupPrinterPortName;
            }

            set
            {
                if (value == _BackupPrinterPortName)
                {
                    return;
                }

                _BackupPrinterPortName = value;
                OnPropertyChaned();
            }
        }


        public bool IsSelectedMainPrinter
        {
            get
            {
                object _IsSelectedMainPrinter;
                CoreApplication.Properties.TryGetValue("SelectedMainPrinter", out _IsSelectedMainPrinter);

                return _IsSelectedMainPrinter != null ? (bool)_IsSelectedMainPrinter : false;

            }

            set
            {
                if (value == _IsSelectedMainPrinter)
                {
                    return;
                }

                _IsSelectedMainPrinter = value;


                object _SelectedMainPrinter;
                if (CoreApplication.Properties.TryGetValue("SelectedMainPrinter", out _SelectedMainPrinter))
                {
                    CoreApplication.Properties.Remove("SelectedMainPrinter");
                }

                CoreApplication.Properties.Add("SelectedMainPrinter", _IsSelectedMainPrinter);
            }
        }

    }
}
