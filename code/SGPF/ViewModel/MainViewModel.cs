using System.Runtime.Remoting;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Data;
using SGPF.DataController;
using SGPF.Messages;

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
        private readonly IAuthenticationService _authenticationService;

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
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMessenger messenger, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            messenger.Register<SessionMessage>(this, OnNewSession);


            LogoutCommand = new RelayCommand(LogoutCommandImpl);
        }

        private void LogoutCommandImpl()
        {
            SafeRun(() => _authenticationService.EndSession(CurrentSession));
        }

        public ICommand LogoutCommand { get; private set; }

        private void OnNewSession(SessionMessage obj)
        {
            if (obj.Session == null || obj.Session.UserDetails == null || obj.Session.UserDetails == CurrentSession.UserDetails)
                return;

            CurrentSession = obj.Session;

        }
    }
}