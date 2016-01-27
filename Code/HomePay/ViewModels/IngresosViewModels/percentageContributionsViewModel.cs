using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePay.ViewModels.IngresosViewModels
{
    public class percentageContributionsViewModel: INotifyBase
    {
        public string Nombre { get; set; }

        private double _porcentaje = 0;

        public double Porcentaje
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
    }
}
