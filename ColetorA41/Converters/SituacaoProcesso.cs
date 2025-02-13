using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Converters;

namespace ColetorA41.Converters
{
    public class SituacaoProcessoColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var situacao = ((string)value);
            
            if (situacao == "E") return new Color(0, 178, 176);
            if (situacao == "S") return new Color(0, 122, 135);
            if (situacao == "R") return new Color(1, 73, 134);
            if (situacao == "B") return new Color(80, 19, 104);
            if (situacao == "L") return new Color(166, 24, 46);

            return new Color(1, 73, 134);

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SituacaoProcessoLabelConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var situacao = ((string)value);
            switch (situacao){
                case "R": return "Reparo";
                case "B": return "Embalagem";
                case "L": return "Resumo Final";
                case "E": return "Entradas";
                case "E1": return "Entradas 1";
                case "E2": return "Entradas 2";
                case "S": return "Saídas";
                default: return string.Empty;
            };
               
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SituacaoProcessoEmbalagemConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var situacao = ((string)value);
            return (situacao != "E" && situacao != "S");

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
