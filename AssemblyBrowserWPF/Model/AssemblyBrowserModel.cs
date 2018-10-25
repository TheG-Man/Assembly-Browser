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

        private AssemblyViewModel _assemblyViewModel;
        public AssemblyViewModel AssemblyViewModel
        {
            get
            {
                return _assemblyViewModel;
            }
            private set
            {
                if (_assemblyViewModel != value)
                {
                    _assemblyViewModel = value;
                    RaisePropertyChanged("AssemblyViewModel");
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
                AssemblyViewModel = new AssemblyViewModel(assemblyInfo);
            }
            catch (LoadAssemblyException e)
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
