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
        /// The <see cref="CanEdit" /> property's name.
        /// </summary>
        public const string IsNewPropertyName = "CanEdit";

        private bool _canEdit = false;

        /// <summary>
        /// Sets and gets the CanEdit property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return _canEdit;
            }

            set
            {
                if (_canEdit == value)
                {
                    return;
                }

                RaisePropertyChanging(IsNewPropertyName);
                _canEdit = value;
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

        /// <summary>
        /// The <see cref="Payment" /> property's name.
        /// </summary>
        public const string PaymentPropertyName = "Payment";

        private ProjectPayment _payment;

        /// <summary>
        /// Sets and gets the Payment property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProjectPayment Payment
        {
            get
            {
                return _payment;
            }

            set
            {
                if (_payment == value)
                {
                    return;
                }

                RaisePropertyChanging(PaymentPropertyName);
                _payment = value;
                RaisePropertyChanged(PaymentPropertyName);
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


            CreateArchivedProjectCommand = new RelayCommand(() => CreateProjectCommandImpl(ProjectState.Archived));
            CreateOpenProjectCommand = new RelayCommand(() => CreateProjectCommandImpl(ProjectState.Open));
            UpdateProjectCommand = new RelayCommand(UpdateProjectCommandImpl);
            SuspendProjectCommand = new RelayCommand(SuspendProjectCommandImpl);
            ArchiveProjectCommand = new RelayCommand(ArchiveProjectCommandImpl);
            OpenProjectCommand = new RelayCommand(OpenProjectCommandImpl);
            ResumeProjectCommand = new RelayCommand(ResumeProjectCommandImpl);
            RejectProjectCommand = new RelayCommand(RejectProjectCommandImpl);
            SetTechnicalOpinionCommand = new RelayCommand(SetTechnicalOpinionImpl);
            AddPaymentCommand = new RelayCommand(AddPaymentCommandImpl);
            ApproveProjectCommand = new RelayCommand(ApproveProjectCommandImpl);

            _messenger.Register<ProjectMessage>(this, OnNewMessage);


        }

       

        public ICommand UpdateProjectCommand { get; private set; }
        public ICommand SuspendProjectCommand { get; private set; }
        public ICommand ArchiveProjectCommand { get; private set; }
        public ICommand OpenProjectCommand { get; private set; }
        public ICommand CreateArchivedProjectCommand { get; private set; }
        public ICommand CreateOpenProjectCommand { get; private set; }
        public ICommand ResumeProjectCommand { get; private set; }
        public ICommand RejectProjectCommand { get; private set; }
        public ICommand ApproveProjectCommand { get; private set; }
        public ICommand SetTechnicalOpinionCommand { get; private set; }
        public ICommand AddPaymentCommand { get; set; }


        private void ApproveProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                await _controller.ApproveProject(User, Project);
                CanEdit = Project.IsEditable;
            });
        }

        private void AddPaymentCommandImpl()
        {
            SafeRun(async () =>
            {
                if (double.IsNaN(Payment.Amount))
                {
                    MessageBox.Show("Please insert a payment value");
                    return;
                }
                await _controller.AddPayment(User, Project, Payment);
                InitializeManagerFields();
            });
        }

        private void RejectProjectCommandImpl()
        {

            SafeRun(async () =>
            {
                await _controller.Reject(User, Project);
                CanEdit = Project.IsEditable;
            });
        }
        private void ResumeProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                await _controller.Resume(User, Project);
                CanEdit = Project.IsEditable;
            });
        }

        private void OpenProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                await _controller.Open(User, Project);
                CanEdit = Project.IsEditable;
            });
        }

        private void ArchiveProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                ThrowIfSuspended();
                await _controller.Archive(User, Project);
            });
        }

        private void SuspendProjectCommandImpl()
        {
            SafeRun(async () =>
            {
                await _controller.Suspend(User, Project);
            });
        }

        private void UpdateProjectCommandImpl()
        {

            SafeRun(async () =>
            {
                ThrowIfSuspended();
                await _controller.Update(User, Project);
            });
        }

        private void SetTechnicalOpinionImpl()
        {
            SafeRun(async () =>
            {
                ThrowIfSuspended();
                await _controller.AddTechnicalOpinion(User, Project, Project.TechnicalDispatch);
            });
        }

        private void ThrowIfSuspended()
        {
            if (Project.IsSuspended)
            {
                throw new Exception("Can't update a project that is suspended");
            }
        }

        private void CreateProjectCommandImpl(ProjectState initialState)
        {



            SafeRun(async () =>
            {
                if (Project.IsValid() == false)
                {
                    MessageBox.Show("Please fill out all the project fields correctly.");
                    return;
                }

                await _controller.Create(User, Project);
                MessageBox.Show("Project Created");

                if (initialState == ProjectState.Open)
                    await _controller.Open(User, Project);
                else
                    await _controller.Archive(User, Project);

                CanEdit = Project.IsEditable;

            });

        }


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



            Project = obj.Project;
            CanEdit = Project.IsEditable;

            if (User is FinantialCommitteeMember)
                FillManagers();

            if (User is FinancialManager)
                InitializeManagerFields();
        }

        private void InitializeManagerFields()
        {
            if (Project.TechnicalDispatch == null)
                Project.TechnicalDispatch = new ProjectTechnicalDispatch();

            Payment = new ProjectPayment { PaymentDate = DateTime.Now, Amount = double.NaN };

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
            CanEdit = true;
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
