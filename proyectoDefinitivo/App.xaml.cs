// =========================================================================
// ARRANQUE DE LA APLICACIÓN CON INYECCIÓN DE DEPENDENCIAS (APP.XAML.CS)
// =========================================================================
// QUÉ HACE ESTE ARCHIVO:
//   1. Configura el contenedor de servicios (ServiceCollection) registrando
//      PantallaLogin y MainWindow como Transient.
//   2. En OnStartup, obtiene PantallaLogin del contenedor y la abre como
//      primera ventana de la aplicación (sin usar StartupUri en App.xaml).
// =========================================================================

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using proyectoDefinitivo.Vista.Dialogo;

namespace proyectoDefinitivo
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

public App()
{
    ServiceCollection services = new();
    ConfigureServices(services);
    _serviceProvider = services.BuildServiceProvider();
}

private static void ConfigureServices(IServiceCollection services)
{
   

    services.AddTransient<PantallaLogin>();
    services.AddTransient<registro>();
    services.AddTransient<olvidarContraseña>();
    services.AddTransient<MainWindow>();

    services.AddTransient<Func<PantallaLogin>>(sp => () => sp.GetRequiredService<PantallaLogin>());
    services.AddTransient<Func<registro>>(sp => () => sp.GetRequiredService<registro>());
    services.AddTransient<Func<olvidarContraseña>>(sp => () => sp.GetRequiredService<olvidarContraseña>());
    services.AddTransient<Func<MainWindow>>(sp => () => sp.GetRequiredService<MainWindow>());
}

protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    PantallaLogin login = _serviceProvider.GetRequiredService<PantallaLogin>();
    MainWindow = login;
    ShutdownMode = ShutdownMode.OnMainWindowClose;
    login.Show();
}
    }
}
