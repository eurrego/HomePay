using MaterialDesignThemes.Wpf;
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
using HomePay.ViewModels;
using System.Windows.Media.Animation;

namespace HomePay.Views.ViewsIngresos
{
    /// <summary>
    /// Lógica de interacción para NewIngreso.xaml
    /// </summary>
    public partial class ContentAdd : UserControl
    {
        #region Singleton

        private static ContentAdd instance;

        public static ContentAdd GetInstance()
        {
            if (instance == null)
            {
                instance = new ContentAdd();
            }

            return instance;
        }
        #endregion

        public ContentAdd()
        {
            InitializeComponent();
            instance = this;
            ContentArea.Content = NewIngreso.GetInstance();
        }
    }
}
