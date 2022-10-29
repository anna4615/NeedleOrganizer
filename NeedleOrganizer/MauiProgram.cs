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

		builder.Services.AddScoped<INeedleService, NeedleService>();
		builder.Services.AddScoped<NeedlesViewModel>();
		builder.Services.AddScoped<MainPage>();

        return builder.Build();
	}
}
