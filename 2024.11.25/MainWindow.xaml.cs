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
using System.Net.Http;
using Newtonsoft.Json;

namespace _2024._11._25
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateTextBlock();
        }
        async void CreateTextBlock()
        {
            almak.Children.Clear();
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:4444/alma";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string stringResponse = await response.Content.ReadAsStringAsync();
                List<almaclass> almalist = JsonConvert.DeserializeObject<List<almaclass>>(stringResponse);
                foreach (almaclass item in almalist)
                {
                    TextBlock oneblock = new TextBlock();
                    oneblock.Text = $"Alma neve: {item.type}, alma ára: {item.price}";
                    almak.Children.Add(oneblock);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        async void Addapple(object s, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://127.1.1.1:4444/alma";
            try
            {
                var jsonObject = new
                {
                    type = nev.Text,
                    price = ar.Text
                };

                string jsonData = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                CreateTextBlock();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
            //MessageBox.Show($"Alma neve: {nev.Text} , Alma ára: {ar.Text}");
        }
    }
}
