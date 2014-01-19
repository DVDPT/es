using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace SGPF.Data
{
    public class Promoter : ObservableObject
    {

        /// <summary>
        /// The <see cref="Address" /> property's name.
        /// </summary>
        public const string AddressPropertyName = "Address";

        private string _address = string.Empty;

        /// <summary>
        /// Sets and gets the Address property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (_address == value)
                {
                    return;
                }

                RaisePropertyChanging(AddressPropertyName);
                _address = value;
                RaisePropertyChanged(AddressPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Nif" /> property's name.
        /// </summary>
        public const string NifPropertyName = "Nif";

        private string _nif = String.Empty;

        /// <summary>
        /// Sets and gets the Nif property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Nif
        {
            get
            {
                return _nif;
            }

            set
            {
                if (_nif == value)
                {
                    return;
                }

                RaisePropertyChanging(NifPropertyName);
                _nif = value;
                RaisePropertyChanged(NifPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Nationality" /> property's name.
        /// </summary>
        public const string NationalityPropertyName = "Nationality";

        private string _nation = string.Empty;

        /// <summary>
        /// Sets and gets the Nationality property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Nationality
        {
            get
            {
                return _nation;
            }

            set
            {
                if (_nation == value)
                {
                    return;
                }

                RaisePropertyChanging(NationalityPropertyName);
                _nation = value;
                RaisePropertyChanged(NationalityPropertyName);
            }
        }

        public bool IsValid()
        {
            return
                !(string.IsNullOrEmpty(Nationality) || string.IsNullOrEmpty(this.Nif) ||
                  string.IsNullOrEmpty(this.Address));
        }
    }
}
