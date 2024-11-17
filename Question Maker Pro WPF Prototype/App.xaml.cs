using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Firebase.Auth;
using Firebase.Auth.Providers;


namespace Question_Maker_Pro_WPF_Prototype
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private bool _isLoggedIn = false;
        private FirebaseAuthClient? _currentFirebaseClient = null;
        //private FirebaseUser? _currentFirebaseUser = null;

        public FirebaseAuthClient? firebaseAuthClient
        {
            get => _currentFirebaseClient; set => _currentFirebaseClient = value;
        }

        public bool isLoggedIn { get => _isLoggedIn; set => _isLoggedIn = value; }

        public App()
        {
            
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
                {
                    string firebaseAPIKey = context.Configuration.GetValue<string>("FIREBASE_WEB_API_KEY");
                    //MessageBox.Show(firebaseAPIKey);
                    Console.WriteLine(firebaseAPIKey);

                    var config = new FirebaseAuthConfig
                    {
                        ApiKey = firebaseAPIKey!,
                        AuthDomain = "schoolquestiontester.firebaseapp.com",
                        Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                        {
                            new EmailProvider()
                        }

                    };


                    service.AddSingleton(new FirebaseAuthClient(config));
                    service.AddSingleton<MainWindow>((service) => new MainWindow());

                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            _currentFirebaseClient = _host.Services.GetRequiredService<FirebaseAuthClient>();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            base.OnExit(e);
        }

        public FirebaseAuthClient firebaseAuthProvider()
        {
            return _currentFirebaseClient;
        }

    }
}
