using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeLabXF_1.Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCard : Grid
    {
        public UserCard()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = BindingContext as Models.User;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"Checkout {item.login} profile on GitHub: \n\n {item.html_url}",
                Title = "GitHub Profile"
            });
        }
    }
}