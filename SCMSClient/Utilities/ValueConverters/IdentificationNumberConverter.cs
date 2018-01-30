using SCMSClient.Models;
using System;
using System.Globalization;

namespace SCMSClient.Utilities
{
    /// <summary>
    /// Gets a <see cref="Cardholder"/> object and formats the <see cref="Cardholder.IdentificationNo"/>
    /// based on the <see cref="Cardholder.IdentificationType"/>
    /// </summary>
    public class IdentificationNumberConverter : BaseValueConverter<IdentificationNumberConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idType = (IdentificationType)value?.GetType().GetProperty("IdentificationType").GetValue(value, null);
            var id = (string)value?.GetType().GetProperty("IdentificationNo").GetValue(value, null);

            if (idType == IdentificationType.NRIC)
            {
                return $"National ID {id}";
            }
            if (idType == IdentificationType.Passport)
            {
                return $"Passport No: {id}";
            }

            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
