using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Newtonsoft.Json;
using ReactiveUI;
using Avalonia.Themes.Fluent;
using Avalonia.Themes;
using Avalonia.Themes.Simple;

//.origusing XamlColorSchemeGenerator;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// theme manager
    /// </summary>
    public partial class ThemeManager : ReactiveObject
    {
        private static object myLockObject = new object();

        /// <summary>
        /// parameter filename for generating the themes
        /// </summary>
        private const string GeneratedParameterFile = "GeneratorParameters.json";

        /// <summary>
        /// Gets the name for the light base color.
        /// </summary>
        public const string BaseColorLight = "Light";

        /// <summary>
        /// Gets the name for the dark base color.
        /// </summary>
        public const string BaseColorDark = "Dark";

        private List<WindowBase> registeredWindows = new List<WindowBase>();

        private readonly ObservableCollection<Theme> themesInternal;
        private readonly ReadOnlyObservableCollection<Theme> themes;

        private readonly ObservableCollection<string> baseColorsInternal;
        private readonly ReadOnlyObservableCollection<string> baseColors;

        private readonly ObservableCollection<ColorScheme> colorSchemesInternal;
        private readonly ReadOnlyObservableCollection<ColorScheme> colorSchemes;

        private static ThemeManager myInstance;

        /// <summary>
        /// singelton instance
        /// </summary>
        public static ThemeManager Instance
        {
            get
            {
                lock (myLockObject)
                {
                    if (myInstance == null)
                    {
                        myInstance = new ThemeManager();
                        myInstance.EnsureThemes();
                    }

                    return myInstance;
                }
            }
        }

        /// <summary>
        /// initialize the lists
        /// </summary>
        private ThemeManager()
        {
            {
                themesInternal = new ObservableCollection<Theme>();
                themes = new ReadOnlyObservableCollection<Theme>(themesInternal);

                //var collectionView = CollectionViewSource.GetDefaultView(themes);
                //collectionView.SortDescriptions.Add(new SortDescription(nameof(Theme.DisplayName), ListSortDirection.Ascending));

                themesInternal.CollectionChanged += ThemesInternalCollectionChanged;
            }

            {
                baseColorsInternal = new ObservableCollection<string>();
                baseColors = new ReadOnlyObservableCollection<string>(baseColorsInternal);

                //var collectionView = CollectionViewSource.GetDefaultView(baseColors);
                //collectionView.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Ascending));
            }

            {
                colorSchemesInternal = new ObservableCollection<ColorScheme>();
                colorSchemes = new ReadOnlyObservableCollection<ColorScheme>(colorSchemesInternal);

                //var collectionView = CollectionViewSource.GetDefaultView(colorSchemes);
                //collectionView.SortDescriptions.Add(new SortDescription(nameof(ColorScheme.Name), ListSortDirection.Ascending));
            }
        }

        private Theme _selectedTheme;

        /// <summary>
        /// current selected theme
        /// </summary>
        public Theme SelectedTheme
        {
            get
            {
                if (_selectedTheme == null)
                {
                    _selectedTheme = Themes.FirstOrDefault();
                }

                return _selectedTheme;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTheme, value);
                OnThemeChanged(value);
            }
        }

        private string mySelectedBaseColor;

        /// <summary>
        /// current selected base color
        /// </summary>
        public string SelectedBaseColor
        {
            get { return mySelectedBaseColor; }
            set { this.RaiseAndSetIfChanged(ref mySelectedBaseColor, value); }
        }

        /// <summary>
        /// This event fires if the theme was changed
        /// this should be using the weak event pattern, but for now it's enough
        /// </summary>
        public event EventHandler<OnThemeChangedEventArgs> IsThemeChanged;

        private void OnThemeChanged(Theme newTheme)
        {
            IsThemeChanged?.Invoke(Application.Current, new OnThemeChangedEventArgs(newTheme));
        }

        /// <summary>
        /// Gets a list of all themes.
        /// </summary>
        public ReadOnlyObservableCollection<Theme> Themes
        {
            get
            {
                EnsureThemes();

                return themes;
            }
        }

        /// <summary>
        /// Gets a list of all available base colors.
        /// </summary>
        public ReadOnlyObservableCollection<string> BaseColors
        {
            get
            {
                EnsureThemes();

                return baseColors;
            }
        }

        /// <summary>
        /// Gets a list of all available color schemes.
        /// </summary>
        public ReadOnlyObservableCollection<ColorScheme> ColorSchemes
        {
            get
            {
                EnsureThemes();

                return colorSchemes;
            }
        }

        /// <summary>
        /// check if the current theme is the dark color
        /// </summary>
        /// <param name="windowBase"></param>
        /// <returns></returns>
        public bool GetIsBaseColorDark(WindowBase windowBase)
        {
            return windowBase.Styles.OfType<StyleInclude>()
          .FirstOrDefault(x => x.Source.AbsoluteUri.Contains(BaseColorDark)) != null;
        }

        /// <summary>
        /// returns the styles base color index
        /// </summary>
        /// <param name="windowBase"></param>
        /// <returns></returns>
        public int GetBaseColorIndex(WindowBase windowBase)
        {
            if (GetIsBaseColorDark(windowBase))
            {
                var item = windowBase.Styles.OfType<StyleInclude>()
           .FirstOrDefault(x => x.Source.AbsoluteUri.Contains(BaseColorDark));

                return windowBase.Styles.OfType<StyleInclude>().ToList().IndexOf(item);
            }
            else
            {
                var item = windowBase.Styles.OfType<StyleInclude>()
           .FirstOrDefault(x => x.Source.AbsoluteUri.Contains(BaseColorLight));
                return windowBase.Styles.OfType<StyleInclude>().ToList().IndexOf(item);
            }
        }

        /// <summary>
        /// enables the theme for the window
        /// </summary>
        /// <param name="window"></param>
        public void EnableTheme(Window window)
        {
            IDisposable disposableForSelectedTheme = null;
            IDisposable disposableForSelectedBaseColor = null;

            window.Opened += (o, e) =>
            {

                if (SelectedTheme == null)
                {
                    SelectedTheme = Themes.FirstOrDefault();
                }

                //ObserveOn(RxApp.MainThreadScheduler)
                disposableForSelectedTheme = this.WhenAnyValue(x => x.SelectedTheme).Where(x => x != null)
                .Subscribe(x =>
                {
                    var item = window.Styles.GetThemeStyle();

                    int index = window.Styles.GetThemeStyleIndex(item);
                    var result = window.CheckAccess();
                    if (index == -1)
                    {
                        try
                        {
                            window.Styles.Add(x.ThemeStyle);
                        }
                        catch (InvalidOperationException)
                        {
                            //HACK: just reload the theme
                            StyleInclude themeStyleInclude = x.ThemeStyle as StyleInclude;

                            var theme = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ExtendedToolkit"))
                            {
                                Source = themeStyleInclude.Source
                            };
                            window.Styles.Add(theme);
                        }
                        catch
                        {


                        }


                    }
                    else
                    {


                        window.Styles.Remove(item);
                        window.Styles.Add(x.ThemeStyle);
                    }
                });

                //ObserveOn(RxApp.MainThreadScheduler)

                disposableForSelectedBaseColor = this.WhenAnyValue(x => x.SelectedBaseColor).Where(x => x != null).Subscribe(selectedBaseColor =>
                {
                    var colorScheme = SelectedTheme?.ColorScheme ?? Themes.Select(x => x.ColorScheme)
                                                                      .FirstOrDefault();

                    SelectedTheme = Themes.FirstOrDefault(x => x.ColorScheme == colorScheme
                                                              && x.BaseColorScheme == selectedBaseColor);
                });

                if (registeredWindows.Contains(window) == false)
                {
                    registeredWindows.Add(window);
                }
            };

            window.Closing += (o, e) =>
            {
                disposableForSelectedTheme?.Dispose();
                disposableForSelectedBaseColor?.Dispose();
                registeredWindows.Remove(window);
            };
        }

        /// <summary>
        /// fills the theme through the generation file
        /// </summary>
        private void EnsureThemes()
        {
            if (themes.Count > 0)
            {
                return;
            }

            try
            {
                var assembly = typeof(ThemeManager).Assembly;
                string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(GeneratedParameterFile));
                //GeneratorParameters generatorParameters = null;
                //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                //{
                //    using (StreamReader sr = new StreamReader(stream))
                //    {
                //        generatorParameters = JsonConvert.DeserializeObject<GeneratorParameters>(sr.ReadToEnd());
                //    }
                //}

                //string nameSpace = typeof(ThemeManager).Namespace;
                //string basePath = resourceName.Replace(GeneratedParameterFile, string.Empty)
                //                    .Replace(nameSpace, string.Empty)
                //                    .Replace(".", "/");
                //basePath = nameSpace + basePath;

                List<string> availableXamlThemes = new List<string>();
                //foreach (var colorScheme in generatorParameters.ColorSchemes.Select(x => x.Name))
                //{
                //    foreach (var baseColorScheme in generatorParameters.BaseColorSchemes.Select(x => x.Name))
                //    {
                //        string themeName = $"{baseColorScheme}.{colorScheme}";

                //        string xamlPathName = basePath + $"{themeName}.axaml";
                //        availableXamlThemes.Add(xamlPathName);
                //    }
                //}
                //availableXamlThemes = availableXamlThemes.OrderBy(x => x).ToList();

                foreach (string xamlFile in availableXamlThemes)
                {
                    string tempXamlPath = xamlFile.Replace("/", ".");

                    var theme = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ExtendedToolkit"))
                    {
                        //resm:Avalonia.Controls.DataGrid.Themes.Default.axaml?assembly=Avalonia.Controls.DataGrid
                        Source = new Uri($"avares://{xamlFile}")
                        //Source = new Uri($"resm:{tempXamlPath}?assembly=Avalonia.ExtendedToolkit")
                    };
                    themesInternal.Add(new Theme(theme));
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        internal void ApplyThemeResourcesFromTheme(Styles styles, Theme theme)
        {
            var item = styles.GetThemeStyle();

            int index = styles.GetThemeStyleIndex(item);
            if (index == -1)
            {
                styles.Add(theme.ThemeStyle);
            }
            else
            {
                styles.Remove(item);
                styles.Add(item);
            }
        }

        /// <summary>
        /// fills the baseColorsInternal and colorSchemesInternal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemesInternalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems.OfType<Theme>())
                    {
                        if (baseColorsInternal.Contains(newItem.BaseColorScheme) == false)
                        {
                            baseColorsInternal.Add(newItem.BaseColorScheme);
                        }

                        if (colorSchemesInternal.Any(x => x.Name == newItem.ColorScheme) == false)
                        {
                            colorSchemesInternal.Add(new ColorScheme(newItem));
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var newItem in e.OldItems.OfType<Theme>())
                    {
                        baseColorsInternal.Remove(newItem.BaseColorScheme);

                        var colorScheme = colorSchemesInternal.FirstOrDefault(x => x.Name == newItem.ColorScheme);
                        if (colorScheme != null)
                        {
                            colorSchemesInternal.Remove(colorScheme);
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    baseColorsInternal.Clear();
                    colorSchemesInternal.Clear();
                    break;
            }
        }

        /// <summary>
        /// Adds an theme.
        /// </summary>
        /// <returns>true if the app theme does not exists and can be added.</returns>
        public bool AddTheme(Uri resourceAddress)
        {
            var theme = new Theme(resourceAddress);

            var themeExists = GetTheme(theme.Name) != null;
            if (themeExists)
            {
                return false;
            }

            themesInternal.Add(theme);
            return true;
        }

        /// <summary>
        /// Adds an theme.
        /// </summary>
        /// <param name="resourceDictionary">The ResourceDictionary of the theme.</param>
        /// <returns>true if the app theme does not exists and can be added.</returns>
        public bool AddTheme(IStyle resourceDictionary)
        {
            var theme = new Theme(resourceDictionary);

            var themeExists = GetTheme(theme.Name) != null;
            if (themeExists)
            {
                return false;
            }

            themesInternal.Add(theme);
            return true;
        }

        /// <summary>
        /// Gets the <see cref="Theme"/> with the given name.
        /// </summary>
        /// <returns>The <see cref="Theme"/> or <c>null</c>, if the theme wasn't found</returns>
        public Theme GetTheme(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Themes.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// returns the theme if found 
        /// or null
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Theme DetectTheme(Window window)
        {
            var item = window.Styles.GetThemeStyle();

            if (item != null)
            {
                return themes.FirstOrDefault(x => x.ThemeStyle == item);
            }

            return null;
        }

        internal Theme DetectTheme(Application current)
        {
            var item = current.Styles.GetThemeStyle();

            if (item != null)
            {
                return themes.FirstOrDefault(x => x.ThemeStyle == item);
            }

            return null;
        }

        /// <summary>
        /// changes the theme through the base color
        /// </summary>
        /// <param name="baseColor"></param>
        public void ChangeBaseColor(string baseColor)
        {
            if (BaseColors.Contains(baseColor) == false)
            {
                throw new ArgumentException($"Wrong basecolor: {baseColor}");
            }

            SelectedBaseColor = baseColor;
        }

        /// <summary>
        /// Gets the inverse <see cref="Theme" /> of the given <see cref="Theme"/>.
        /// This method relies on the "Dark" or "Light" affix to be present.
        /// </summary>
        /// <param name="theme">The app theme.</param>
        /// <returns>The inverse <see cref="Theme"/> or <c>null</c> if it couldn't be found.</returns>
        /// <remarks>
        /// Returns BaseLight, if BaseDark is given or vice versa.
        /// Custom Themes must end with "Dark" or "Light" for this to work, for example "CustomDark" and "CustomLight".
        /// </remarks>
        public Theme GetInverseTheme(Theme theme)
        {
            if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            if (theme.Name.StartsWith("dark.", StringComparison.OrdinalIgnoreCase))
            {
                return GetTheme("Light." + theme.Name.Substring("dark.".Length));
            }

            if (theme.Name.StartsWith("light.", StringComparison.OrdinalIgnoreCase))
            {
                return GetTheme("Dark." + theme.Name.Substring("light.".Length));
            }

            return null;
        }

        /// <summary>
        /// changes the color scheme of the current theme
        /// </summary>
        /// <param name="colorScheme"></param>
        public void ChangeColorScheme(ColorScheme colorScheme)
        {
            SelectedTheme = this.Themes.FirstOrDefault(x => x.ColorScheme == colorScheme.Name
                                                        && x.BaseColorScheme == SelectedBaseColor);
        }
    }
}
