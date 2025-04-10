using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColetorA41.Converters
{
    public class TipoFicha : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Geral": return "Total Geral";
                case "Renovar": return "Renovações";
                case "SemSaldo": return "Sem Saldo";
                case "ET": return "Extrakit";
                case "SoEntrada": return "Somente Entrada";
                case "Pagar": return "Pagamentos";
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
