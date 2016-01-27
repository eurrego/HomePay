using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomePay.Models;
using System.Text.RegularExpressions;

namespace HomePay.ViewModels.IngresosViewModels
{
    public class NewIngresoViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Singleton

        private static NewIngresoViewModel instance;

        public static NewIngresoViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new NewIngresoViewModel();
            }

            return instance;
        }
        #endregion

        #region Propertys

        private string _persona;

        public string Persona
        {
            get { return _persona; }
            set
            {
                this.MutateVerbose(ref _persona, value, RaisePropertyChanged());
            }
        }

        private string _valor;

        public string Valor
        {
            get { return _valor; }
            set
            {
                this.MutateVerbose(ref _valor, value, RaisePropertyChanged());
            }
        }

        private int _selected;

        public int Selected
        {
            get { return _selected; }
            set
            {
                this.MutateVerbose(ref _selected, value, RaisePropertyChanged());
            }
        }

        private int _categoria;

        public int Categoria
        {
            get { return _categoria; }
            set
            {
                this.MutateVerbose(ref _categoria, value, RaisePropertyChanged());
            }
        }

        private DateTime _fecha = DateTime.Now;

        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                this.MutateVerbose(ref _fecha, value, RaisePropertyChanged());
            }
        }

        private List<Categoria> _listCmbCategoria;

        public List<Categoria> listCmbCategoria
        {
            get { return _listCmbCategoria; }
            set
            {
                this.MutateVerbose(ref _listCmbCategoria, value, RaisePropertyChanged());
            }
        }

        #endregion

        #region Costructor

        public NewIngresoViewModel()
        {
            listCmbCategoria = ComboBoxList();
        }

        #endregion

        #region Methods

        private List<Categoria> ComboBoxList()
        {
            using (var entity = new DBHomePayEntities())
            {
                var query = from c in entity.Categoria
                            where c.Tipo == "I"
                            select c;

                return query.ToList();
            }
        }

        #endregion

        #region DataValidation

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                Regex numeros = new Regex("^[0-9]*$");

                switch (name)
                {
                    case "Persona":
                        if (string.IsNullOrWhiteSpace(_persona))
                        {
                            result = "El campo es requerido";
                        }
                        else
                        {
                            if (_persona.Length > 50)
                            {
                                result = "El nombre de la persona no debe ser superior a 50 caracteres";
                            }
                        }
                        break;
                    case "Valor":
                        if (string.IsNullOrWhiteSpace(_valor))
                        {
                            result = "El campo es requerido";
                        }
                        else
                        {
                            if (!numeros.IsMatch(_valor))
                            {
                                result = "El campo solo acepta números";
                            }
                        }

                        break;
                    case "Fecha":

                        if (_fecha == null)
                        {
                            result = "El campo es obligatorio.";
                        }

                        break;
                    case "Selected":
                        if (_selected < 0)
                        {
                            result = "Debe seleccionar un valor";
                        }

                        break;

                    default:
                        break;
                }


                return result;
            }

        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

    }
}
