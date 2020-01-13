using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLabXF_1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Models.User> _usersList;
        public ObservableCollection<Models.User> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                RaisePropertyChanged();
            }
        }

        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

        public async Task<Models.ApiResponse> GetUsers()
        {
            IsLoading = true;

            Models.ApiResponse response = null;

            try
            {
                var httpclient = new HttpClient();

                httpclient.BaseAddress = new Uri("https://api.github.com/");
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Add("Accept", " application/json");

                var httpresponse = await httpclient.GetAsync("search/users?q=location%3Akenya");
                string content = await httpresponse.Content.ReadAsStringAsync();
                //Stream stream = await httpresponse.Content.ReadAsStreamAsync();
                //string content = await StreamToStringAsync(stream);

                Debug.WriteLine(content);

                response = new Models.ApiResponse
                {
                    Content = content,
                    StatusCode = (int)httpresponse.StatusCode
                };

                UsersList = new ObservableCollection<Models.User>(StringToJObject(response.Content).ToObject<Models.UserRootObject>().items);

                httpclient.Dispose();

                IsLoading = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return response;
        }

        public static JObject StringToJObject(string jsonString)
        {
            JObject data = null;

            try
            {
                data = JObject.Parse(jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return data;
        }

        static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }


        public MainViewModel()
        {
            UsersList = new ObservableCollection<Models.User>();
            _ = GetUsers();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
