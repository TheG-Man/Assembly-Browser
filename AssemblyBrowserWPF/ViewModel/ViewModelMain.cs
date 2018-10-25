using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using AssemblyBrowserWPF.Model;
using AssemblyBrowser;

namespace AssemblyBrowserWPF.ViewModel
{
    class ViewModelMain : INotifyPropertyChanged
    {
        private AssemblyBrowserModel _assemblyBrowserModel;

        public AssemblyViewModel AssemblyViewModel => _assemblyBrowserModel.AssemblyViewModel;
        public RelayCommand OpenAssemblyCommand { get; set; }

        public ViewModelMain()
        {
            _assemblyBrowserModel = new AssemblyBrowserModel();

            OpenAssemblyCommand = new RelayCommand(OpenAssembly);
            _assemblyBrowserModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        }

        void OpenAssembly(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog().Value)
            {
                _assemblyBrowserModel.OpenAssembly(openFileDialog.FileName);
            }
        }

        internal void RaisePropertyChanged(string prop)
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
