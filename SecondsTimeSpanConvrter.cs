using System.Windows.Data;
using System;
using System.Globalization;


namespace MediaPlayer
{

    [ValueConversion(typeof(double), typeof(TimeSpan))]
    class SecondsTimeSpanConvrter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double time = (double)value;

            // convert time type and set a new time format in string
            return TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
