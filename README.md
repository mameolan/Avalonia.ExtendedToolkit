

# Avalonia.ExtendedToolkit


![alt text](github/Images/Avalonia.ExampleApp-Overview.gif "Main application")   



Avalonia.ExtendedTool wants to port some controls from the WPF to Avalonia.

Most styles / ideas where taken from:

- [MahApps Toolkit](https://github.com/MahApps/MahApps.Metro) 
- [Extended WPF Toolkit](https://github.com/xceedsoftware/wpftoolkit)
- [WPF Toolkit (Microsoft)](https://github.com/dotnet/wpf)

etc.

Please have a look at the example app or in the [wiki](https://github.com/mameolan/Avalonia.ExtendedToolkit/wiki) on how to use the controls.

### Solution Structure

------

- Avalonia.Controlz: library which can be merge to the Avalonia Project 
- Avalonia.ExampleApp: Example Application for the Extended Toolkit
- Avalonia.ExtendedToolkit: library which have special controls and styles



### How to add the styles to your project

------

```xml	
<Application.Styles>
	<StyleInclude Source="avares://Avalonia.Themes.Default/DefaultTheme.xaml"/>
	<StyleInclude Source="resm:Avalonia.Controls.DataGrid.Themes.Default.xaml?assembly=Avalonia.Controls.DataGrid" />
	<StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Generic.xaml"/>
</Application.Styles>

```

Set Color in App.xaml Styles like this i.e.:

```xml
<StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Themes/Dark.Blue.xaml"/>
```

or use the ThemeManager (App.xaml.cs):

```cs
public override void OnFrameworkInitializationCompleted()
{
   if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
   {
        var window=new MainWindow();
		ThemeManager.Instance.EnableTheme(window);
        desktop.MainWindow = window;
   }

   base.OnFrameworkInitializationCompleted();
}
```

