using HomePay.ViewModels.IngresosViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomePay.Views.ViewsIngresos
{
    /// <summary>
    /// Lógica de interacción para NewIngreso.xaml
    /// </summary>
    public partial class NewIngreso : UserControl
    {
        #region Singleton

        private static NewIngreso instance;

        public static NewIngreso GetInstance()
        {
            if (instance == null)
            {
                instance = new NewIngreso();
            }

            return instance;
        }
        #endregion

        public NewIngreso()
        {
            InitializeComponent();
            instance = this;
            DataContext = NewIngresoViewModel.GetInstance();
        }
    }
}
