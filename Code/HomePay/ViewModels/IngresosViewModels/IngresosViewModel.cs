using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using HomePay.Models;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using HomePay.Views;
using System.ComponentModel;
using System.Text.RegularExpressions;
using HomePay.Views.ViewsIngresos;

namespace HomePay.ViewModels.IngresosViewModels
{
    public class IngresosViewModel : INotifyBase
    {

        #region Singleton

        private static IngresosViewModel instance;

        public static IngresosViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new IngresosViewModel();
            }

            return instance;
        }
        #endregion

        #region Propertys

        #region Message Notification Propertys


        /// <summary>
        /// Establece el color de la imagen o icono
        /// </summary>
        private string _color;

        public string Color
        {
            get { return _color; }
            set
            {
                if (_color == value)
                {
                    return;
                }

                _color = value;
                OnPropertyChanged();
            }
        }


        private string _imagen;

        public string Imagen
        {
            get { return _imagen; }
            set
            {
                if (_imagen == value)
                {
                    return;
                }

                _imagen = value;
                OnPropertyChanged();
            }
        }

        private string _titulo;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (_titulo == value)
                {
                    return;
                }

                _titulo = value;
                OnPropertyChanged();
            }
        }

        private string _mensaje;

        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                if (_mensaje == value)
                {
                    return;
                }

                _mensaje = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region View Propertys

        /// <summary>
        /// Propiedad enlazada con el boton Mes, el cual cambia la propiedad content
        /// </summary>
        private string _mes;

        public string Mes
        {
            get { return _mes; }
            set
            {
                if (_mes == value)
                {
                    return;
                }

                _mes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// propiedad relacionada con Mes y se utiliza para realizar consulta 
        /// con Linq
        /// </summary>
        private int _mesValue;

        public int MesValue
        {
            get { return _mesValue; }
            set
            {
                if (_mesValue == value)
                {
                    return;
                }

                _mesValue = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<SelectableItemsViewModel> _items;

        public ObservableCollection<SelectableItemsViewModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items == value)
                {
                    return;
                }

                _items = value;
                OnPropertyChanged();
            }
        }

        private decimal _total;

        public decimal Total
        {
            get { return _total; }
            set
            {
                if (_total == value)
                {
                    return;
                }

                _total = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> _ingresosMes;

        public ObservableCollection<double> IngresosMes
        {
            get
            {
                return _ingresosMes;
            }
            set
            {
                if (_ingresosMes == value)
                {
                    return;
                }

                _ingresosMes = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<percentageContributionsViewModel> _porcentaje;

        public ObservableCollection<percentageContributionsViewModel> Porcentaje
        {
            get
            {
                return _porcentaje;
            }
            set
            {
                if (_porcentaje == value)
                {
                    return;
                }

                _porcentaje = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Construtores

        public IngresosViewModel()
        {
            DateTime mesActual = DateTime.Now;
            int mes = mesActual.Month;
            bool open = mesInicial(mes);

            var instanceIngresos = ViewIngresos.GetInstance();
            if (open)
            {
                instanceIngresos.storyBoard();
            }

            ingresosPorMes();
            porcentajeAporte();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Carga items registrados de la vista principal de la vista Ingresos 
        /// </summary>
        /// <returns></returns>
        public bool ItemsIngresos()
        {
            double valor = 0;
            DateTime fecha = DateTime.Now;
            string nombre = string.Empty;
            string persona = string.Empty;
            Items = new ObservableCollection<SelectableItemsViewModel>();

            using (var entity = new DBHomePayEntities())
            {
                var queryItems = new ObservableCollection<object>(from c in entity.Ingresos
                                                                  join t in entity.Categoria
                                                                  on c.IdCategoria equals t.IdCategoria
                                                                  where c.Fecha.Month == MesValue
                                                                  select new
                                                                  {
                                                                      Fecha = c.Fecha,
                                                                      Valor = c.Valor,
                                                                      Persona = c.Persona,
                                                                      Nombre = t.Nombre
                                                                  });

                foreach (var item in queryItems)
                {
                    Type v = item.GetType();
                    var var1 = v.GetProperty("Valor").GetValue(item).ToString();
                    valor = Convert.ToDouble(var1);

                    Type p = item.GetType();
                    persona = p.GetProperty("Persona").GetValue(item).ToString();

                    Type n = item.GetType();
                    nombre = n.GetProperty("Nombre").GetValue(item).ToString();

                    Type f = item.GetType();
                    var var2 = f.GetProperty("Fecha").GetValue(item).ToString();
                    fecha = Convert.ToDateTime(var2);

                    
                    Items.Add(new SelectableItemsViewModel { Valor = valor, Persona = persona, Nombre = nombre, Fecha = fecha } );

                }


                var query = (from c in entity.Ingresos
                             where c.Fecha.Month == MesValue
                             select (decimal?)c.Valor).Sum() ?? 0;

                if (query == 0)
                {
                    Titulo = "Información";
                    Mensaje = "El mes de " + Mes + " no tiene registros asociados";
                    Imagen = "M15.998999,17.988002C16.551,17.988002 16.999,18.437009 16.999,18.990018 16.999,19.543027 16.551,19.992034 15.998999,19.992034 15.446999,19.992034 14.998999,19.543027 14.998999,18.990018 14.998999,18.437009 15.446999,17.988002 15.998999,17.988002z M15.998999,8.0630169C16.552,8.0630169,16.999,8.5110159,16.999,9.0650149L16.999,16.167001C16.999,16.721 16.552,17.168998 15.998999,17.168998 15.446,17.168998 14.998999,16.721 14.998999,16.167001L14.998999,9.0650149C14.998999,8.5110159,15.446,8.0630169,15.998999,8.0630169z M15.999277,2.3337629L2.3830507,22.996989 29.615442,22.996989z M15.999513,0C16.50052,-3.8678991E-07,17.001274,0.21826346,17.288297,0.65479128L31.74151,22.585975C32.057495,23.064982 32.084472,23.677966 31.812494,24.184989 31.541494,24.687009 31.02049,24.999999 30.450475,24.999999L1.5480773,24.999999C0.97903845,24.999999 0.45705819,24.687009 0.18703459,24.184989 -0.085979476,23.677966 -0.057963934,23.064982 0.25704309,22.585975L14.709219,0.65479128C14.997247,0.21826346,15.498506,-3.8678991E-07,15.999513,0z";
                    Color = "DarkOrange";
                    Total = 0;
                    return true;
                }
                else
                {
                    Total = query;
                    return false;
                }

            }
        }


        /// <summary>
        /// Carga Grafico de barras Ingresos por Mes
        /// </summary>
        public void ingresosPorMes()
        {
            using (var entity = new DBHomePayEntities())
            {

                var query = new ObservableCollection<object>(from c in entity.Ingresos
                                                             group c by new
                                                             {
                                                                 c.Fecha.Month,
                                                             } into mes
                                                             select new
                                                             {
                                                                 Total = mes.Sum(m => m.Valor)
                                                             });


                IngresosMes = new ObservableCollection<double>();

                foreach (var item in query)
                {
                    Type t = item.GetType();
                    var valor = t.GetProperty("Total").GetValue(item).ToString();
                    IngresosMes.Add(Convert.ToDouble(valor));
                }
            }
        }


        /// <summary>
        /// Carga grafica porcentaje de Aportes por persona
        /// </summary>
        public void porcentajeAporte()
        {
            double parcial = 0;
            double porcentaje = 0;
            Porcentaje = new ObservableCollection<percentageContributionsViewModel>();


            using (var entity = new DBHomePayEntities())
            {

                var query = new ObservableCollection<object>(from c in entity.Ingresos
                                                             where c.Fecha.Month == MesValue
                                                             group c by new
                                                             {
                                                                 c.Persona
                                                             } into mes
                                                             select new
                                                             {
                                                                 Persona = mes.Key.Persona,
                                                                 Total = mes.Sum(m => m.Valor)
                                                             });

                var total = Convert.ToInt32((from c in entity.Ingresos
                                             where c.Fecha.Month == MesValue
                                             select c.Valor).Sum() ?? 0);

                foreach (var item in query)
                {
                    Type p = item.GetType();
                    var valor = p.GetProperty("Total").GetValue(item).ToString();
                    parcial = Convert.ToDouble(valor);

                    Type n = item.GetType();
                    var nombre = n.GetProperty("Persona").GetValue(item).ToString();


                    porcentaje = (parcial * 100) / total;

                    Porcentaje.Add(new percentageContributionsViewModel() { Nombre = nombre, Porcentaje = porcentaje });
                }
            }
        }

        /// <summary>
        /// Asigna el mes al consultar
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private bool mesInicial(int month)
        {
            int mes = month;

            switch (mes)
            {
                case 1:
                    Mes = "Enero";
                    MesValue = 1;
                    break;
                case 2:
                    Mes = "Febrero";
                    MesValue = 2;
                    break;
                case 3:
                    Mes = "Marzo";
                    MesValue = 3;
                    break;
                case 4:
                    Mes = "Abril";
                    MesValue = 4;
                    break;
                case 5:
                    Mes = "Mayo";
                    MesValue = 5;
                    break;
                case 6:
                    Mes = "Junio";
                    MesValue = 6;
                    break;
                case 7:
                    Mes = "Julio";
                    MesValue = 7;
                    break;
                case 8:
                    Mes = "Agosto";
                    MesValue = 8;
                    break;
                case 9:
                    Mes = "Septiembre";
                    MesValue = 9;
                    break;
                case 10:
                    Mes = "Octubre";
                    MesValue = 10;
                    break;
                case 11:
                    Mes = "Noviembre";
                    MesValue = 11;
                    break;
                case 12:
                    Mes = "Diciembre";
                    MesValue = 12;
                    break;
            }

            return ItemsIngresos();
        }

        /// <summary>
        /// Manejador de evento cerrar del Commando seleccionar Mes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventSelectedMonth(object sender, DialogClosingEventArgs eventArgs)
        {
            int parameter = Convert.ToInt32(eventArgs.Parameter);
            bool open = mesInicial(parameter);

            var instanceIngresos = ViewIngresos.GetInstance();

            if (open)
            {
                instanceIngresos.storyBoard();
            }

            porcentajeAporte();
        }


        /// <summary>
        /// Manejador de evento cerrar del Commando Add Ingreso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            bool add = Convert.ToBoolean(eventArgs.Parameter);

            var instanceView = NewIngreso.GetInstance();

            if (add)
            {
                if (IsValid(instanceView.txtPersona) && IsValid(instanceView.txtValor))
                {
                    Ingresos newIngreso = new Ingresos
                    {
                        Valor = Convert.ToDecimal(instanceView.txtValor.Text),
                        IdCategoria = Convert.ToInt32(instanceView.cmbConcepto.SelectedValue),
                        Fecha = Convert.ToDateTime(instanceView.dtdFecha.SelectedDate),
                        Persona = instanceView.txtPersona.Text
                    };

                    using (var entity = new DBHomePayEntities())
                    {
                        entity.Ingresos.Add(newIngreso);
                        entity.SaveChanges();
                    }

                    instanceView.txtValor.Text = string.Empty;
                    instanceView.cmbConcepto.SelectedValue = 1;
                    instanceView.dtdFecha.SelectedDate = DateTime.Now;
                    instanceView.txtPersona.Text = string.Empty;

                    Save();
                }
            }
            else
            {
                instanceView.txtValor.Text = string.Empty;
                instanceView.cmbConcepto.SelectedValue = 1;
                instanceView.dtdFecha.SelectedDate = DateTime.Now;
                instanceView.txtPersona.Text = string.Empty;
            }

        }


        /// <summary>
        /// Recarga la vista Principal y muestra notificación 
        /// </summary>
        public void Save()
        {
            bool open = false;

            porcentajeAporte();
            ingresosPorMes();

            var result = ItemsIngresos();

            if (!result)
            {
                open = true;
                Titulo = "Información";
                Mensaje = "El registro ha sido Exitoso";
                Imagen = "M13.01411,29.23447L13.016379,29.249113C13.105274,29.676908 13.489509,30 13.947996,30 14.406509,30 14.790747,29.676908 14.879642,29.249113L14.881248,29.238749z M19.756001,13.490006L21.165997,14.908007 13.921694,22.115021 12.635012,23.409006 12.628013,23.402042 12.621013,23.409006 11.334296,22.114986 8.3629942,19.158981 9.7730007,17.740973 12.628021,20.581293z M13.657005,2.000001C12.966006,2.0000012,12.382008,2.6720006,12.382008,3.4680023L12.382008,5.4740043 10.600076,5.4740043 10.505037,5.5033259C9.5004377,5.8263736 7.9557505,6.5919971 6.6720004,8.3919954 5.0279999,11.446992 5.1069999,13.92099 5.3980002,17.077986 5.914,22.679979 6.052,25.03698 4.2580004,27.126976L4.1860003,27.202976 4.1740003,27.213976 24.024002,27.258976 23.960001,27.191975C22.165001,25.100979 22.303001,22.74398 22.82,17.141985 23.111002,13.98599 23.190001,11.510991 21.546001,8.4569941 20.133188,6.4769955 18.405588,5.7481189 17.426111,5.4833293L17.390043,5.4740043 15.716,5.4740043 15.716,3.4680023C15.716,2.6720006,15.132002,2.0000012,14.440002,2.000001z M13.657005,0L14.440002,0C16.245998,0,17.715996,1.5560007,17.715996,3.4680023L17.715996,3.4981594 17.810972,3.5212097C19.114923,3.8529267,21.404125,4.7807493,23.215,7.3519959L23.276001,7.4499955C25.214001,11.016992 25.127001,13.91199 24.811001,17.325985 24.299002,22.888981 24.318001,24.50598 25.444,25.849977 26.718,27.030975 28.208,27.277977 28.223,27.280977L28.07,29.268972 16.892689,29.243359 16.883745,29.360153C16.732327,30.840752 15.473318,32 13.947996,32 12.422701,32 11.163695,30.840752 11.012278,29.360153L11.002299,29.22986 0.14299965,29.204973 0,27.214977C0.0089998227,27.212976 1.4990002,26.965975 2.7740002,25.785978 3.9000001,24.44198 3.9190001,22.82498 3.4060001,17.260984 3.0909998,13.84699 3.0039999,10.952992 4.9420004,7.3849969L5.0029998,7.2879968C6.7309065,4.8329668,8.8950758,3.8762174,10.222289,3.5060043L10.382122,3.4633904 10.386281,3.2898312C10.474154,1.4602708,11.907446,0,13.657005,0z";
                Color = "Green";
            }

            if (open)
            {
                var instanceView = ViewIngresos.GetInstance();
                instanceView.storyBoard();
            }
        }


        /// <summary>
        /// Verifica si los campos son validos
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Icommand para boton seleccionar mes
        /// </summary>
        public ICommand RunDialogSelectedMonth => new RelayCommand(ExecuteSelectedMonth);

        private async void ExecuteSelectedMonth(object o)
        {
            var view = new SelectedMonth
            {
                DataContext = this
            };

            //show the dialog
            var result = await DialogHost.Show(view, ClosingEventSelectedMonth);
        }


        /// <summary>
        /// Icommand para botn add ingreso
        /// </summary>
        public ICommand RunDialogAddIngreso => new RelayCommand(ExecuteAddIngreso);

        private async void ExecuteAddIngreso(object o)
        {

            var view = new ContentAdd
            {
                DataContext = this
            };

            //show the dialog
            var result = await DialogHost.Show(view, ClosingEventHandler);

        }

        #endregion
    }
}



