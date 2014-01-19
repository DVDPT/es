using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace SGPF.Data
{
    public enum TechnicalOpinion
    {
        Undefined,
        Approve,
        Reject,
        ConvertToLoan
    }

    public enum ProjectType
    {
        Undefined,
        Incentive,
        Loan //bonificação
    }


    public enum ProjectState
    {
        Undefined,
        Open,
        AwaitingDispatch,
        Rejected,
        InPayment,
        Closed,
        Archived,
        Completed
    }

    public class ProjectTechnicalDispatch : ObservableObject
    {


        /// <summary>
        /// The <see cref="Opinion" /> property's name.
        /// </summary>
        public const string OpinionPropertyName = "Opinion";

        private TechnicalOpinion _opinion;

        /// <summary>
        /// Sets and gets the Opinion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TechnicalOpinion Opinion
        {
            get
            {
                return _opinion;
            }

            set
            {
                if (_opinion == value)
                {
                    return;
                }

                RaisePropertyChanging(OpinionPropertyName);
                _opinion = value;
                RaisePropertyChanged(OpinionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Comment" /> property's name.
        /// </summary>
        public const string TextPropertyName = "Comment";

        private string _comment = string.Empty;

        /// <summary>
        /// Sets and gets the Comment property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                RaisePropertyChanging(TextPropertyName);
                _comment = value;
                RaisePropertyChanged(TextPropertyName);
            }
        }

        public override string ToString()
        {
            return string.Format("Dispatch Opinion:{0}, Comments: {1}", Opinion, Comment);
        }
    }

    public class Project : ObservableObject, IEquatable<Project>
    {
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// The <see cref="Promoter" /> property's name.
        /// </summary>
        public const string PromoterPropertyName = "Promoter";

        private Promoter _promoter = null;


        public const string IsSuspendedPropertyName = "IsSuspended";
        public bool IsSuspended
        {
            get { return SuspendedBy != null; }
        }

        public const string IsEditablePropertyName = "IsSuspended";
        public bool IsEditable
        {
            get
            {
                return State == ProjectState.AwaitingDispatch || State == ProjectState.Open ||
                       State == ProjectState.Undefined;
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private int _id = -1;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (_id == value)
                {
                    return;
                }

                RaisePropertyChanging(IdPropertyName);
                _id = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the Promoter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Promoter Promoter
        {
            get
            {
                return _promoter;
            }

            set
            {
                if (_promoter == value)
                {
                    return;
                }

                RaisePropertyChanging(PromoterPropertyName);
                _promoter = value;
                RaisePropertyChanged(PromoterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AccountId" /> property's name.
        /// </summary>
        public const string AccountIdPropertyName = "AccountId";

        private string _accoun = string.Empty;

        /// <summary>
        /// Sets and gets the AccountId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AccountId
        {
            get
            {
                return _accoun;
            }

            set
            {
                if (_accoun == value)
                {
                    return;
                }

                RaisePropertyChanging(AccountIdPropertyName);
                _accoun = value;
                RaisePropertyChanged(AccountIdPropertyName);
            }
        }



        /// <summary>
        /// The <see cref="Type" /> property's name.
        /// </summary>
        public const string TypePropertyName = "Type";

        private ProjectType _type;

        /// <summary>
        /// Sets and gets the Type property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProjectType Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (_type == value)
                {
                    return;
                }

                RaisePropertyChanging(TypePropertyName);
                _type = value;
                RaisePropertyChanged(TypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Amount" /> property's name.
        /// </summary>
        public const string AmountPropertyName = "Amount";

        private double _amount = 0.0;

        /// <summary>
        /// Sets and gets the Amount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                if (_amount == value)
                {
                    return;
                }

                RaisePropertyChanging(AmountPropertyName);
                _amount = value;
                RaisePropertyChanged(AmountPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = string.Empty;

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                RaisePropertyChanging(DescriptionPropertyName);
                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="State" /> property's name.
        /// </summary>
        public const string StatePropertyName = "State";

        private ProjectState _state;

        /// <summary>
        /// Sets and gets the State property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProjectState State
        {
            get
            {
                return _state;
            }

            set
            {
                if (_state == value)
                {
                    return;
                }
                var canEdit = IsEditable;
                RaisePropertyChanging(StatePropertyName);
                _state = value;


                RaisePropertyChanged(IsEditablePropertyName);
                RaisePropertyChanged(StatePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="ExecutionDate" /> property's name.
        /// </summary>
        public const string ExecutionDatePropertyName = "ExecutionDate";

        private DateTime _executionDate;

        /// <summary>
        /// Sets and gets the ExecutionDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime ExecutionDate
        {
            get
            {
                return _executionDate;
            }

            set
            {
                if (_executionDate == value)
                {
                    return;
                }

                RaisePropertyChanging(ExecutionDatePropertyName);
                _executionDate = value;
                RaisePropertyChanged(ExecutionDatePropertyName);
            }
        }



        /// <summary>
        /// The <see cref="LoanRate" /> property's name.
        /// </summary>
        public const string LoanRatePropertyName = "LoanRate";

        private double _loanRate = 0.0;

        /// <summary>
        /// Sets and gets the LoanRate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double LoanRate
        {
            get
            {
                return _loanRate;
            }

            set
            {
                if (_loanRate == value)
                {
                    return;
                }

                RaisePropertyChanging(LoanRatePropertyName);
                _loanRate = value;
                RaisePropertyChanged(LoanRatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Payments" /> property's name.
        /// </summary>
        public const string PaymentsPropertyName = "Payments";

        private ObservableCollection<ProjectPayment> _payments;

        /// <summary>
        /// Sets and gets the Payments property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ProjectPayment> Payments
        {
            get
            {
                return _payments;
            }

            set
            {
                if (_payments == value)
                {
                    return;
                }

                RaisePropertyChanging(PaymentsPropertyName);
                _payments = value;
                RaisePropertyChanged(PaymentsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="History" /> property's name.
        /// </summary>
        public const string HistoryPropertyName = "History";

        private ObservableCollection<ProjectHistory> _histories;

        /// <summary>
        /// Sets and gets the History property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ProjectHistory> History
        {
            get
            {
                return _histories;
            }

            set
            {
                if (_histories == value)
                {
                    return;
                }

                RaisePropertyChanging(HistoryPropertyName);
                _histories = value;
                RaisePropertyChanged(HistoryPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Representer" /> property's name.
        /// </summary>
        public const string RepresenterPropertyName = "Representer";

        private Person _representerPerson;

        /// <summary>
        /// Sets and gets the Representer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Person Representer
        {
            get
            {
                return _representerPerson;
            }

            set
            {
                if (_representerPerson == value)
                {
                    return;
                }


                RaisePropertyChanging(RepresenterPropertyName);
                _representerPerson = value;
                RaisePropertyChanged(RepresenterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SuspendedBy" /> property's name.
        /// </summary>
        public const string SuspendedByPropertyName = "SuspendedBy";

        private BasePerson _suspendedByPerson;

        /// <summary>
        /// Sets and gets the SuspendedBy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BasePerson SuspendedBy
        {
            get
            {
                return _suspendedByPerson;
            }

            set
            {
                if (_suspendedByPerson == value)
                {
                    return;
                }

                RaisePropertyChanging(SuspendedByPropertyName);
                RaisePropertyChanging(IsSuspendedPropertyName);
                _suspendedByPerson = value;
                RaisePropertyChanged(SuspendedByPropertyName);
                RaisePropertyChanged(IsSuspendedPropertyName);
            }
        }

        public ProjectState PrevSuspendedState { get; set; }

        /// <summary>
        /// The <see cref="Manager" /> property's name.
        /// </summary>
        public const string ManagerPropertyName = "Manager";

        private FinancialManager _managerPerson;

        /// <summary>
        /// Sets and gets the Manager property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public FinancialManager Manager
        {
            get
            {
                return _managerPerson;
            }

            set
            {
                if (_managerPerson == value)
                {
                    return;
                }

                RaisePropertyChanging(ManagerPropertyName);
                _managerPerson = value;
                RaisePropertyChanged(ManagerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TechnicalDispatch" /> property's name.
        /// </summary>
        public const string TechnicalDispatchPropertyName = "TechnicalDispatch";

        private ProjectTechnicalDispatch _dispatch;

        /// <summary>
        /// Sets and gets the TechnicalDispatch property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ProjectTechnicalDispatch TechnicalDispatch
        {
            get
            {
                return _dispatch;
            }

            set
            {
                if (_dispatch == value)
                {
                    return;
                }

                RaisePropertyChanging(TechnicalDispatchPropertyName);
                _dispatch = value;
                RaisePropertyChanged(TechnicalDispatchPropertyName);
            }
        }

        public bool IsValid()
        {
            if (((int)Amount) == 0)
            {
                return false;

            }

            if (string.IsNullOrEmpty(Description)) return false;


            if (ExecutionDate <= DateTime.Now) return false;



            if (Promoter.IsValid() == false)
                return false;

            if (this.Representer.IsValid() == false)
            {
                return false;
            }
            return true;
        }

        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Project)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool operator ==(Project left, Project right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Project left, Project right)
        {
            return !Equals(left, right);
        }

    }
}
