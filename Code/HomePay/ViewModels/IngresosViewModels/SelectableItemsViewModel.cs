using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePay.ViewModels.IngresosViewModels
{
    public class SelectableItemsViewModel : INotifyBase
    {

        #region Propertys

        private bool _isSelected;
        private double _valor;
        private DateTime _fecha;
        private string _nombre;
        private string _persona;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre == value)
                {
                    return;
                }
                _nombre = value;
                OnPropertyChanged();
            }
        }

        public double Valor
        {
            get { return _valor; }
            set
            {
                if (_valor == value)
                {
                    return;
                }

                _valor = value;
                OnPropertyChanged();
            }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha == value)
                {
                    return;
                }
                _fecha = value;
                OnPropertyChanged();
            }
        }

        public string Persona
        {
            get { return _persona; }
            set
            {
                if (_persona == value)
                {
                    return;
                }

                _persona = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
