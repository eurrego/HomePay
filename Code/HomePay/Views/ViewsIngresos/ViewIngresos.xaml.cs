using System.Windows.Controls;
using HomePay.ViewModels.IngresosViewModels;
using System.Windows.Media.Animation;


namespace HomePay.Views.ViewsIngresos
{
    /// <summary>
    /// Lógica de interacción para Ingresos.xaml
    /// </summary>
    public partial class ViewIngresos : UserControl
    {
        #region Singleton

        private static ViewIngresos instance;

        public static ViewIngresos GetInstance()
        {
            if (instance == null)
            {
                instance = new ViewIngresos();
            }

            return instance;
        }
        #endregion

        public ViewIngresos()
        {
            InitializeComponent();
            instance = this;
            DataContext = IngresosViewModel.GetInstance();
            Chart.PrimaryAxis.LabelFormatter = x => "$ " + x;
        }

        public void storyBoard()
        {
            Storyboard sb = Resources["ShowMessageNotification"] as Storyboard;
            sb.Begin(messageNotification);
        }
    }
}
