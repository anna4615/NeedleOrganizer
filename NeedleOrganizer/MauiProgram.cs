using NeedleOrganizer.Interfaces;
using NeedleOrganizer.Services;
using NeedleOrganizer.View;
using NeedleOrganizer.ViewModel;

namespace NeedleOrganizer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<INeedleService, NeedleService>();
		builder.Services.AddSingleton<NeedlesViewModel>();
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<NeedleDetailsViewModel>();
		builder.Services.AddTransient<DetailsPage>();

		builder.Services.AddTransient<AdminViewModel>();
		builder.Services.AddTransient<AdminPage>();


		return builder.Build();
	}
}
