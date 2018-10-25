using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AssemblyBrowserWPF.ViewModel;
using AssemblyBrowser;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.Model
{
    class AssemblyBrowserModel : INotifyPropertyChanged
    {
        private readonly IAssemblyBrowser _assemblyBrowser = AssemblyBrowser.AssemblyBrowser.GetInstance();

        private AssemblyViewModel _assemblyInfoViewModel;
        public AssemblyViewModel AssemblyViewModelInfo
        {
            get
            {
                return _assemblyInfoViewModel;
            }
            private set
            {
                if (_assemblyInfoViewModel != value)
                {
                    _assemblyInfoViewModel = value;
                    RaisePropertyChanged("AssemblyViewModelInfo");
                }
            }
        }

        public AssemblyBrowserModel()
        {

        }

        public void OpenAssembly(string path)
        {
            try
            {
                AssemblyInfo assemblyInfo = _assemblyBrowser.GetAssemblyInfo(path);
                AssemblyViewModelInfo = new AssemblyViewModel(assemblyInfo);
            }
            catch (Exception)
            {

            }
        }

        void RaisePropertyChanged(string prop)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
