using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Data;
using SGPF.Database;
using SGPF.DataController;
using SGPF.Messages;

namespace SGPF.ViewModel
{
    public class ProjectViewModel : BaseAppViewModel
    {
        /// <summary>
        /// The <see cref="User" /> property's name.
        /// </summary>
        public const string UserPropertyName = "User";

        private BasePerson _person;

        /// <summary>
        /// Sets and gets the User property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BasePerson User
        {
            get
            {
                return _person;
            }

            set
            {
                if (_person == value)
                {
                    return;
                }

                RaisePropertyChanging(UserPropertyName);
                _person = value;
                RaisePropertyChanged(UserPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNew" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "IsNew";

        private bool _isNew = false;

        /// <summary>
        /// Sets and gets the IsNew property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                if (_isNew == value)
                {
                    return;
                }

                RaisePropertyChanging(IsNewPropertyName);
                _isNew = value;
                RaisePropertyChanged(IsNewPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Project" /> property's name.
        /// </summary>
        public const string ProjectPropertyName = "Project";

        private Project _project;

        /// <summary>
        /// Sets and gets the Project property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Project Project
        {
            get
            {
                return _project;
            }

            set
            {
                if (_project == value)
                {
                    return;
                }

                RaisePropertyChanging(ProjectPropertyName);
                _project = value;
                RaisePropertyChanged(ProjectPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FinancialManagers" /> property's name.
        /// </summary>
        public const string FinancialManagersPropertyName = "FinancialManagers";

        private ObservableCollection<FinancialManager> _managers;

        /// <summary>
        /// Sets and gets the FinancialManagers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<FinancialManager> FinancialManagers
        {
            get
            {
                return _managers;
            }

            set
            {
                if (_managers == value)
                {
                    return;
                }

                RaisePropertyChanging(FinancialManagersPropertyName);
                _managers = value;
                RaisePropertyChanged(FinancialManagersPropertyName);
            }
        }


        private readonly IMessenger _messenger;
        private readonly IProjectController _controller;
        private readonly ISGPFDatabase _database;

        public ProjectViewModel(IMessenger messenger, IProjectController controller, ISGPFDatabase database)
        {
            _messenger = messenger;
            _controller = controller;
            _database = database;


            CreateProjectCommand = new RelayCommand(CreateProjectCommandImpl);
            UpdateProjectCommand = new RelayCommand(UpdateProjectCommandImpl);
            SuspendProjectCommand = new RelayCommand(SuspendProjectCommandImpl);
            ArchiveProjectCommand = new RelayCommand(ArchiveProjectCommandImpl);
            _messenger.Register<ProjectMessage>(this, OnNewMessage);


        }

        private void ArchiveProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                await _controller.Archive(User, Project);
            });
        }

        private void SuspendProjectCommandImpl()
        {
            throw new NotImplementedException();
        }

        private void UpdateProjectCommandImpl()
        {
            if (Project.IsSuspended)
            {
                MessageBox.Show("Can't update a project that is suspended");
                return;
            }
        }

        private void CreateProjectCommandImpl()
        {

            if (Project.IsValid() == false)
            {
                MessageBox.Show("Please fill out all the project fields correctly.");
                return;
            }

            SafeRun(async () =>
            {
                await _controller.Create(User, Project);
                MessageBox.Show("Project Created");
                IsNew = false;
            });

        }

        public ICommand CreateProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand SuspendProjectCommand { get; set; }
        public ICommand ArchiveProjectCommand { get; set; }

        private async void OnNewMessage(ProjectMessage obj)
        {
            User = obj.UserDetails;

            if (User == null)
                return;

            if (obj.Project == null)
            {
                obj.Project = new Project();
                await NewProject(obj.Project);
            }

            if (User is FinantialCommitteeMember)
                FillManagers();

            Project = obj.Project;


        }

        private void FillManagers()
        {
            SafeRun(async () =>
            {
                FinancialManagers = new ObservableCollection<FinancialManager>((await _database.Persons.All()).OfType<FinancialManager>());
            });
        }

        private async Task NewProject(Project project)
        {
            IsNew = true;
            project.CreatedTime = DateTime.Now;
            project.Promoter = new Promoter();
            project.Representer = new Person();
            project.Type = ProjectType.Incentive;
            project.ExecutionDate = DateTime.Now;
            project.History = new ObservableCollection<ProjectHistory>();
            project.Payments = new ObservableCollection<ProjectPayment>();
        }
    }
}
