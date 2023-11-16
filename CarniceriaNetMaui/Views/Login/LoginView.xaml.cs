using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Viewmodels.Login;

namespace CarniceriaNetMaui.Views.Login;

public partial class LoginView : ContentPage
{
    LoginViewModel viewModel;
    CobradoresRepository cobradoresRepository = new CobradoresRepository();
    public LoginView()
	{   
        var viewmodel = new LoginViewModel();
		InitializeComponent();
        BindingContext = viewmodel;
	}

    private void btnShowPassword_Clicked(object sender, EventArgs e)
    {
        if (EntryPassword.IsPassword == false)
        {
            EntryPassword.IsPassword = true;
        }
        else
        {
            EntryPassword.IsPassword = false;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
}