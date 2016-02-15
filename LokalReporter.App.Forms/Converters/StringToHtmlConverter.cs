using System;
using System.Globalization;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Converters
{

    public class StringToHtmlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return value;
            }

            var s = (string)value;

            return $@"<html><body>
    <h1>Xamarin.Forms</h1>
    <p>{s}</p>
    </body></html>";
            //                $@"<html><head>
            //    <link rel='stylesheet' id='dashicons-css'  href='http://lokalreporter.idvl.de/wp-includes/css/dashicons.min.css?ver=4.4.2' type='text/css' media='all' />
            //<link rel='stylesheet' id='core-css'  href='http://lokalreporter.idvl.de/wp-content/plugins/cmms/assets/css/frontend/core.css?ver=2.3.2' type='text/css' media='all' />
            //<link rel='stylesheet' id='font-awesome-css'  href='http://lokalreporter.idvl.de/wp-content/plugins/cmms/assets/css/font-awesome.min.css?ver=3.1.1' type='text/css' media='all' />
            //<link rel='stylesheet' id='lightbox-css'  href='http://lokalreporter.idvl.de/wp-content/plugins/cmms/assets/css/lightbox.css?ver=4.4.2' type='text/css' media='all' />
            //<link rel='stylesheet' id='idvl-editor-public-css'  href='http://lokalreporter.idvl.de/wp-content/plugins/idvl-editor/assets/css/idvl-editor-public.css?ver=2.0.26.0' type='text/css' media='all' />
            //<link rel='stylesheet' id='cmms-royalslider-css-css'  href='http://lokalreporter.idvl.de/wp-content/plugins/cmms/assets/royalslider/royalslider.css?ver=9.5.7-a' type='text/css' media='all' />
            //<link rel='stylesheet' id='theme-css-css'  href='http://lokalreporter.idvl.de/wp-content/themes/lokalreporter/css/layout.css?ver=1.0.3' type='text/css' media='all' />
            //<link rel='stylesheet' id='idvl-editor-css'  href='http://lokalreporter.idvl.de/wp-content/themes/lokalreporter/idvl-editor/css/idvl-editor.css?ver=1.0.3' type='text/css' media='all' /></head><body>{s}</body></html>";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}