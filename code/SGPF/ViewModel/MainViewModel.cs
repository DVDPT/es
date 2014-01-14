using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Data;
using SGPF.DataController;
using SGPF.Messages;
using SGPF.Model;

namespace SGPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : BaseAppViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IAuthenticationService _authenticationService;
        private readonly IProjectController _controller;
        private readonly ProjectVizualizer _projHelper;

        /// <summary>
        /// The <see cref="ProjectId" /> property's name.
        /// </summary>
        public const string ProjectIdPropertyName = "ProjectId";

        private int _projId;

        /// <summary>
        /// Sets and gets the ProjectId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ProjectId
        {
            get
            {
                return _projId;
            }

            set
            {
                if (_projId == value)
                {
                    return;
                }

                RaisePropertyChanging(ProjectIdPropertyName);
                _projId = value;
                RaisePropertyChanged(ProjectIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentSession" /> property's name.
        /// </summary>
        public const string CurrentUserPropertyName = "CurrentSession";

        private UserSession _currSession;

        /// <summary>
        /// Sets and gets the CurrentSession property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public UserSession CurrentSession
        {
            get
            {
                return _currSession;
            }

            set
            {
                if (_currSession == value)
                {
                    return;
                }

                RaisePropertyChanging(CurrentUserPropertyName);
                _currSession = value;
                RaisePropertyChanged(CurrentUserPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Projects" /> property's name.
        /// </summary>
        public const string ProjectsPropertyName = "Projects";

        private ObservableCollection<Project> _projects;

        /// <summary>
        /// Sets and gets the Projects property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Project> Projects
        {
            get
            {
                return _projects;
            }

            set
            {
                if (_projects == value)
                {
                    return;
                }

                RaisePropertyChanging(ProjectsPropertyName);
                _projects = value;
                RaisePropertyChanged(ProjectsPropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMessenger messenger, IAuthenticationService authenticationService, IProjectController controller, ProjectVizualizer projHelper)
        {
            _messenger = messenger;
            _authenticationService = authenticationService;
            _controller = controller;
            _projHelper = projHelper;
            messenger.Register<SessionMessage>(this, OnNewSession);


            LogoutCommand = new RelayCommand(LogoutCommandImpl);
            NewProjectCommand = new RelayCommand(NewProjectCommandImpl);
            RefreshCommand = new RelayCommand(RefreshCommandImpl);
            OpenProjectCommand = new RelayCommand<Project>(OpenProjectCommandImpl);
            OpenProjectCommandById = new RelayCommand(OpenProjectCommandByIdImpl);
        }

        private void OpenProjectCommandByIdImpl()
        {
            SafeRun(async () =>
            {
                var proj = await _controller.GetById(CurrentSession.UserDetails, ProjectId);
                OpenProjectCommandImpl(proj);
            });
        }

        private void OpenProjectCommandImpl(Project p)
        {
            _projHelper.OpenProject(p);
            RefreshCommandImpl();
        }

        private void RefreshCommandImpl()
        {
            SafeRun(async () =>
            {
                Projects = new ObservableCollection<Project>(await _controller.GetProjectsFor(CurrentSession.UserDetails));
            });
        }

        private void NewProjectCommandImpl()
        {
            _projHelper.CreateProject();
            RefreshCommandImpl();
        }

        private void LogoutCommandImpl()
        {
            SafeRun(async () =>
            {
                await _authenticationService.EndSession(CurrentSession);
                ClearCurrentSession();
            });
        }

        private void ClearCurrentSession()
        {
            CurrentSession = null;
        }

        public ICommand LogoutCommand { get; private set; }
        public ICommand NewProjectCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand OpenProjectCommand { get; set; }
        public ICommand OpenProjectCommandById { get; set; }


        private void OnNewSession(SessionMessage obj)
        {
            if (obj.Session == null || obj.Session.UserDetails == null || obj.Session == CurrentSession)
                return;

            CurrentSession = obj.Session;
            RefreshCommandImpl();

        }


    }


}