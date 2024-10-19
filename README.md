

# Avalonia.ExtendedToolkit


![alt text](github/Images/Avalonia.ExampleApp-Overview.gif "Main application")   

Fork of original ExtendedToolkit from mameolan, porting to Avalonia 11 and Dotnet 8. 
Original blocks of code, commented during rewriting are labeled by ".orig".
Dangerous changes are labeled by ".editing".
Not working Views code (XML) labeled by "bad view".

Things to do:
1. Fix themes management (replace "XamlColorSchemeGenerator" dependency) and re-implement themes changes.
2. Controls Extensions re-implementation (KeyExt etc).
3. Fixing View code - what to do with "ItemTemplate" removed in Avalonia 11 (not removed, changed? https://github.com/AvaloniaUI/Avalonia/pull/11281)
4. Controls fix - fix ItemContainer methods (https://github.com/AvaloniaUI/Avalonia/pull/9677 ?), removed in Avalonia 11 and other staff.

Original code taken from @maeolan and some blocks from @Nekohim3, but heavily rewritten

Avalonia.ExtendedTool wants to port some controls from the WPF to Avalonia.

Most styles / ideas where taken from:

- [MahApps Toolkit](https://github.com/MahApps/MahApps.Metro) 
- [Extended WPF Toolkit](https://github.com/xceedsoftware/wpftoolkit)
- [WPF Toolkit (Microsoft)](https://github.com/dotnet/wpf)

etc.

**Contributor are welcome.**

### Nugets
------

- [Avalonia.Controlz](https://www.nuget.org/packages/mameolan.Avalonia.Controlz/)
- [Avalonia.ExtendedToolkit](https://www.nuget.org/packages/mameolan.Avalonia.ExtendedToolkit/)


### Usage
------
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
	<StyleInclude Source="avares://Avalonia.Themes.Default/DefaultTheme.axaml"/>
	<StyleInclude Source="resm:Avalonia.Controls.DataGrid.Themes.Default.xaml?assembly=Avalonia.Controls.DataGrid" />
	<!-- without datagrid -->
    <StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Generic.axaml"/>
    <!-- includes datagrid seperately: -->
    <StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Default.Datagrid.axaml"/>
    
    <!-- with datagrid -->
    <!-- <StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Generic.All.axaml"/> -->
</Application.Styles>

```

Set Color in App.axaml Styles like this i.e.:

```xml
<StyleInclude Source="avares://Avalonia.ExtendedToolkit/Styles/Themes/Dark.Blue.axaml"/>
```

or use the ThemeManager (App.axaml.cs):

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

### AppBuilder Setup:

```cs
appBuilder
.UsePlatformDetect()
...
.UseSkia()
.UseReactiveUI() //required
.UseAvaloniaExtended() //for i.e. dialog service
...
```

