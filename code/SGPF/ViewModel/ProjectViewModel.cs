using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Data;
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

        private readonly IMessenger _messenger;
        private readonly IProjectController _controller;

        public ProjectViewModel(IMessenger messenger, IProjectController controller)
        {
            _messenger = messenger;
            _controller = controller;

            _messenger.Register<ProjectMessage>(this, OnNewMessage);
        }

        private async void OnNewMessage(ProjectMessage obj)
        {
            User = obj.UserDetails;

            if (obj.Project == null)
            {
                obj.Project = new Project();
                IsNew = true;
            }

            Project = obj.Project;

        }

    }
}
