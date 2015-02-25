using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleInstaller.Annotations;
using SimpleInstaller.Elements;

namespace SimpleInstaller.ViewModels
{
    public class InstallationElementWrapper : INotifyPropertyChanged
    {
        private readonly InstallerElement _element;

        public string Name { get; private set; }

        public InstallationElementWrapper(InstallerElement element)
        {
            _element = element;
            Name = element.Name;
        }

        private bool _inProgress;
        private bool _completed;
        private int _progress;

        public bool InProgress
        {
            get { return _inProgress; }
            set
            {
                if (value.Equals(_inProgress)) return;
                _inProgress = value;
                OnPropertyChanged();
            }
        }

        public bool Completed
        {
            get { return _completed; }
            set
            {
                if (value.Equals(_completed)) return;
                _completed = value;
                OnPropertyChanged();
            }
        }

        public int Progress
        {
            get { return _progress; }
            set
            {
                if (value == _progress) return;
                _progress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Install()
        {
            InProgress = true;
            Progress = 0;
            await _element.InstallAsync();
            InProgress = false;
            Completed = true;
            Progress = 1;
        }
    }

    public class InstallationViewModel : INotifyPropertyChanged
    {
        private Installation _installation;
        private string _name;

        public ObservableCollection<InstallationElementWrapper> Elements { get; set; }
        public ObservableCollection<string> Logs { get; set; }
        
        public void SetInstaller(Installation installation)
        {
            Logs = new ObservableCollection<string>();
            Logs.Clear();

            foreach (var element in installation.Elements)
            {
                element.Logger = s => Logs.Add(s);
            }

            Elements = new ObservableCollection<InstallationElementWrapper>(installation.Elements.Select(x => new InstallationElementWrapper(x)));
            _installation = installation;
        }

        public void UpdateInformation()
        {
            if (_installation == null)
                return;

            Name = _installation.Name;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public async Task Install()
        {
            foreach (var installerElement in Elements)
            {
                await installerElement.Install();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            var handler = PropertyChanged;            
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}