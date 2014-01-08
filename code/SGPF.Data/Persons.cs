using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace SGPF.Data
{
    public enum PersonType
    {
        Regular,
        FinancialManager,
        FinantialCommitteeMember,
        Technician
    }

    public class Technician : BasePerson
    {
        public override PersonType Type
        {
            get { return PersonType.Technician; }
        }
    }

    public class FinancialManager : BasePerson
    {
        public override PersonType Type
        {
            get { return PersonType.FinancialManager; }
        }
    }

    public class FinantialCommitteeMember : BasePerson
    {
        public override PersonType Type
        {
            get { return PersonType.FinantialCommitteeMember; }
        }
    }

    public class Person : BasePerson
    {
        public override PersonType Type
        {
            get { return PersonType.Regular; }
        }
    }
    public abstract class BasePerson : ObservableObject
    {
        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = string.Empty;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                RaisePropertyChanging(NamePropertyName);
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private string _id = string.Empty;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Id
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
        /// The <see cref="Phone" /> property's name.
        /// </summary>
        public const string PhonePropertyName = "Phone";

        private string _phone = string.Empty;

        /// <summary>
        /// Sets and gets the Phone property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                if (_phone == value)
                {
                    return;
                }

                RaisePropertyChanging(PhonePropertyName);
                _phone = value;
                RaisePropertyChanged(PhonePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Email" /> property's name.
        /// </summary>
        public const string EmailPropertyName = "Email";

        private string _email = string.Empty;

        /// <summary>
        /// Sets and gets the Email property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email == value)
                {
                    return;
                }

                RaisePropertyChanging(EmailPropertyName);
                _email = value;
                RaisePropertyChanged(EmailPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Designation" /> property's name.
        /// </summary>
        public const string DesignationPropertyName = "Designation";

        private string _desig = string.Empty;

        /// <summary>
        /// Sets and gets the Designation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Designation
        {
            get
            {
                return _desig;
            }

            set
            {
                if (_desig == value)
                {
                    return;
                }

                RaisePropertyChanging(DesignationPropertyName);
                _desig = value;
                RaisePropertyChanged(DesignationPropertyName);
            }
        }

        public abstract PersonType Type { get; }
    }
}
