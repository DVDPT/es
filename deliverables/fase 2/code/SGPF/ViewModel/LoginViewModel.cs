using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SGPF.DataController;
using SGPF.Messages;

namespace SGPF.ViewModel
{
    public class LoginViewModel : BaseAppViewModel
    {
        private readonly IMessenger _messenger;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// The <see cref="UserId" /> property's name.
        /// </summary>
        public const string UserIdPropertyName = "UserId";

        private string _userId = string.Empty;

        /// <summary>
        /// Sets and gets the UserId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                if (_userId == value)
                {
                    return;
                }

                RaisePropertyChanging(UserIdPropertyName);
                _userId = value;
                RaisePropertyChanged(UserIdPropertyName);
            }
        }

        public LoginViewModel(IMessenger messenger, IAuthenticationService authenticationService)
        {
            _messenger = messenger;
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(LoginCommandImpl);
        }

        private async void LoginCommandImpl()
        {
            if (UserId == null || string.IsNullOrEmpty(UserId))
            {
                MessageBox.Show("Please insert a valid user id");
                return;
            }


            SafeRun(async () =>
            {
                var session = await _authenticationService.StartSession(UserId);
                _messenger.Send(new SessionMessage { Session = session });
            });

        }

        public ICommand LoginCommand { get; private set; }
    }
}
