using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Weather;

namespace ContainerPrac
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cityName.Visibility = Visibility.Hidden;
            card1.Visibility = card2.Visibility = card3.Visibility = card4.Visibility = card5.Visibility = Visibility.Hidden;
            img1.Visibility = img2.Visibility = img3.Visibility = img4.Visibility = img5.Visibility = Visibility.Hidden;
            date1.Visibility = date2.Visibility = date3.Visibility = date4.Visibility = date5.Visibility = Visibility.Hidden;
            avgTempDescription.Visibility = avgTempDescription2.Visibility = avgTempDescription3.Visibility = avgTempDescription4.Visibility = avgTempDescription5.Visibility = Visibility.Hidden;
            Temp.Visibility = Temp2.Visibility = Temp3.Visibility = Temp4.Visibility = Temp5.Visibility = Visibility.Hidden;
            avgHumidity.Visibility = avgHumidity2.Visibility = avgHumidity3.Visibility = avgHumidity4.Visibility = avgHumidity5.Visibility = Visibility.Hidden;
            avgWindSpeed.Visibility = avgWindSpeed2.Visibility = avgWindSpeed3.Visibility = avgWindSpeed4.Visibility = avgWindSpeed5.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window();
            cityName.Visibility = Visibility.Visible;
            resultStack.Children.Clear();

            if (cityName.Text != "No results found.")
            {
                card1.Visibility = card2.Visibility = card3.Visibility = card4.Visibility = card5.Visibility = Visibility.Visible;
                img1.Visibility = img2.Visibility = img3.Visibility = img4.Visibility = img5.Visibility = Visibility.Visible;
                date1.Visibility = date2.Visibility = date3.Visibility = date4.Visibility = date5.Visibility = Visibility.Visible;
                avgTempDescription.Visibility = avgTempDescription2.Visibility = avgTempDescription3.Visibility = avgTempDescription4.Visibility = avgTempDescription5.Visibility = Visibility.Visible;
                Temp.Visibility = Temp2.Visibility = Temp3.Visibility = Temp4.Visibility = Temp5.Visibility = Visibility.Visible;
                avgHumidity.Visibility = avgHumidity2.Visibility = avgHumidity3.Visibility = avgHumidity4.Visibility = avgHumidity5.Visibility = Visibility.Visible;
                avgWindSpeed.Visibility = avgWindSpeed2.Visibility = avgWindSpeed3.Visibility = avgWindSpeed4.Visibility = avgWindSpeed5.Visibility = Visibility.Visible;
            }
        }



        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var data = Model.GetData();

            string query = (sender as TextBox).Text;

            resultStack.Children.Clear();

            foreach (var obj in data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
            }
        }

        private void addItem(string text)
        {
            cityName.Visibility = Visibility.Hidden;

            TextBlock block = new TextBlock();

            block.Text = text;

            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            block.MouseLeftButtonUp += (sender, e) =>
            {
                citySearch.Text = (sender as TextBlock).Text;
                resultStack.Children.Clear();
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.MediumPurple;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            resultStack.Children.Add(block);
        }



        private void Window()
        {

            var input = citySearch.Text;

            string data;
            WebClient client = new WebClient();

            string apiCall = "http://api.apixu.com/v1/forecast.json?key=d0e03472b60b4f3d92b135909191102&q=" + input + "&days=5";

            try
            {
                data = client.DownloadString(apiCall);

                var weather = Welcome.FromJson(data);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("http:" + weather.Forecast.Forecastday[0].Day.Condition.Icon);
                image.EndInit();
                img1.Source = image;

                cityName.Text = weather.Location.Name + "\n" + weather.Location.Country;
                date1.Text = weather.Forecast.Forecastday[0].Date.Date.ToString("dd.MM.yy");
                avgTempDescription.Text = weather.Forecast.Forecastday[0].Day.AvgtempC + " °C " + weather.Forecast.Forecastday[0].Day.Condition.Text;
                Temp.Text = "Min temp: \t Max temp: \n" + weather.Forecast.Forecastday[0].Day.MintempC + " °C \t\t" + weather.Forecast.Forecastday[0].Day.MaxtempC + " °C ";
                avgHumidity.Text = "Humidity: " + weather.Forecast.Forecastday[0].Day.Avghumidity + " %";
                avgWindSpeed.Text = "Wind speed: " + weather.Forecast.Forecastday[0].Day.MaxwindKph + " k/h";

                BitmapImage image2 = new BitmapImage();
                image2.BeginInit();
                image2.UriSource = new Uri("http:" + weather.Forecast.Forecastday[1].Day.Condition.Icon);
                image2.EndInit();
                img2.Source = image2;

                date2.Text = weather.Forecast.Forecastday[1].Date.Date.ToString("dd.MM.yy");
                avgTempDescription2.Text = weather.Forecast.Forecastday[1].Day.AvgtempC + " °C " + weather.Forecast.Forecastday[1].Day.Condition.Text;
                Temp2.Text = "Min temp: \t Max temp: \n" + weather.Forecast.Forecastday[1].Day.MintempC + " °C \t\t" + weather.Forecast.Forecastday[1].Day.MaxtempC + " °C ";
                avgHumidity2.Text = "Humidity: " + weather.Forecast.Forecastday[1].Day.Avghumidity + " %";
                avgWindSpeed2.Text = "Wind speed: " + weather.Forecast.Forecastday[1].Day.MaxwindKph + " k/h";

                BitmapImage image3 = new BitmapImage();
                image3.BeginInit();
                image3.UriSource = new Uri("http:" + weather.Forecast.Forecastday[2].Day.Condition.Icon);
                image3.EndInit();
                img3.Source = image3;

                date3.Text = weather.Forecast.Forecastday[2].Date.Date.ToString("dd.MM.yy");
                avgTempDescription3.Text = weather.Forecast.Forecastday[2].Day.AvgtempC + " °C " + weather.Forecast.Forecastday[2].Day.Condition.Text;
                Temp3.Text = "Min temp: \t Max temp: \n" + weather.Forecast.Forecastday[2].Day.MintempC + " °C \t\t" + weather.Forecast.Forecastday[2].Day.MaxtempC + " °C ";
                avgHumidity3.Text = "Humidity: " + weather.Forecast.Forecastday[2].Day.Avghumidity + " %";
                avgWindSpeed3.Text = "Wind speed: " + weather.Forecast.Forecastday[2].Day.MaxwindKph + " k/h";

                BitmapImage image4 = new BitmapImage();
                image4.BeginInit();
                image4.UriSource = new Uri("http:" + weather.Forecast.Forecastday[3].Day.Condition.Icon);
                image4.EndInit();
                img4.Source = image4;

                date4.Text = weather.Forecast.Forecastday[3].Date.Date.ToString("dd.MM.yy");
                avgTempDescription4.Text = weather.Forecast.Forecastday[3].Day.AvgtempC + " °C " + weather.Forecast.Forecastday[3].Day.Condition.Text;
                Temp4.Text = "Min temp: \t Max temp: \n" + weather.Forecast.Forecastday[3].Day.MintempC + " °C \t\t" + weather.Forecast.Forecastday[3].Day.MaxtempC + " °C ";
                avgHumidity4.Text = "Humidity: " + weather.Forecast.Forecastday[3].Day.Avghumidity + " %";
                avgWindSpeed4.Text = "Wind speed: " + weather.Forecast.Forecastday[3].Day.MaxwindKph + " k/h";

                BitmapImage image5 = new BitmapImage();
                image5.BeginInit();
                image5.UriSource = new Uri("http:" + weather.Forecast.Forecastday[4].Day.Condition.Icon);
                image5.EndInit();
                img5.Source = image5;

                date5.Text = weather.Forecast.Forecastday[4].Date.Date.ToString("dd.MM.yy");
                avgTempDescription5.Text = weather.Forecast.Forecastday[4].Day.AvgtempC + " °C " + weather.Forecast.Forecastday[4].Day.Condition.Text;
                Temp5.Text = "Min temp: \t Max temp: \n" + weather.Forecast.Forecastday[4].Day.MintempC + " °C \t\t" + weather.Forecast.Forecastday[4].Day.MaxtempC + " °C ";
                avgHumidity5.Text = "Humidity: " + weather.Forecast.Forecastday[4].Day.Avghumidity + " %";
                avgWindSpeed5.Text = "Wind speed: " + weather.Forecast.Forecastday[4].Day.MaxwindKph + " k/h";
            }
            catch
            {
                cityName.Text = "No results found.";
            }
        }

        private void CitySearch_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            citySearch.Clear();
        }
    }
}
