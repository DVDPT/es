using System.Windows.Media.Animation;
using GalaSoft.MvvmLight.Messaging;
using SGPF.Data;
using SGPF.Messages;
using SGPF.ViewModel;

namespace SGPF.Model
{
    public class ProjectVizualizer
    {
        private readonly IMessenger _messenger;
        private BasePerson _currUser;

        public ProjectVizualizer(IMessenger messenger)
        {
            _messenger = messenger;
            messenger.Register<SessionMessage>(this, OnNewSession);
        }

        private void OnNewSession(SessionMessage obj)
        {
            if (obj == null || obj.Session == null)
            {
                _currUser = null;
                return;
            }

            _currUser = obj.Session.UserDetails;
        }

        public void OpenProject(Project p)
        {
            _messenger.Send(new ProjectMessage(_currUser, p));
            OpenProjectDialog();
        }

        public void CreateProject()
        {
            _messenger.Send(new ProjectMessage(_currUser));
            OpenProjectDialog();
        }

        private void OpenProjectDialog()
        {
            new ProjectInfo().ShowDialog();
        }
    }
}
