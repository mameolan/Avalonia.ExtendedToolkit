using Avalonia.Collections;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExampleApp.Model;
using Avalonia.ExampleApp.Views;
using Avalonia.ExtendedToolkit;
using Avalonia.ExtendedToolkit.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private object _currentHamburgerMenuContent;

        public object CurrentHamburgerMenuContent
        {
            get { return _currentHamburgerMenuContent; }
            set { this.RaiseAndSetIfChanged(ref _currentHamburgerMenuContent, value); }
        }

        private ObservableCollection<Album> _albums;
        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set { this.RaiseAndSetIfChanged(ref _albums, value); }
        }

        private ObservableCollection<Artist> _artists;
        public ObservableCollection<Artist> Artists
        {
            get { return _artists; }
            set { this.RaiseAndSetIfChanged(ref _artists, value); }
        }

        public ICommand GenreDropDownMenuItemCommand { get; }

        public ICommand ChangeColorSchemeCommand { get; }

        public ICommand ArtistsDropDownCommand { get; }

        public ICommand ChangeBaseColorsCommand { get; }

        public ICommand ShowBusyIndicator { get; }

        public ICommand OpenWizardCommand { get; }

        public ICommand HamburgerMenuHomeCommand { get; }

        public ICommand HamburgerMenuSearchCommand { get; }

        public ICommand HamburgerMenuLikesCommand { get; }

        public ICommand HamburgerMenuListsCommand { get; }

        public ICommand HamburgerMenuProfileCommand { get; }

        private bool myIsBusy;

        public bool IsBusy
        {
            get { return myIsBusy; }
            set { this.RaiseAndSetIfChanged(ref myIsBusy, value); }
        }

        private bool myIsToggleSwitchVisible;

        public bool IsToggleSwitchVisible
        {
            get { return myIsToggleSwitchVisible; }
            set { this.RaiseAndSetIfChanged(ref myIsToggleSwitchVisible, value); }
        }

        private AvaloniaList<BrushResource> _brushResources = new AvaloniaList<BrushResource>();
        public AvaloniaList<BrushResource> BrushResources
        {
            get
            {
                return _brushResources;
            }
            private set
            {
                this.RaiseAndSetIfChanged(ref _brushResources, value);
            }
        }



        private ReadOnlyObservableCollection<ColorScheme> myColorSchemes;
        public ReadOnlyObservableCollection<ColorScheme> ColorSchemes
        {
            get
            {
                return myColorSchemes;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref myColorSchemes, value);
            }

        }


        private ColorScheme mySelectedColorScheme;
        public ColorScheme SelectedColorScheme
        {
            get { return mySelectedColorScheme; }
            set
            {
                if (mySelectedColorScheme != value)
                {
                    this.RaiseAndSetIfChanged(ref mySelectedColorScheme, value);
                    ExecuteChangeColorSchemeCommand(mySelectedColorScheme);

                }
            }
        }



        private ReadOnlyObservableCollection<string> myBaseColors;
        public ReadOnlyObservableCollection<string> BaseColors
        {
            get
            {
                return myBaseColors;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref myBaseColors, value);
            }
        }

        public string mySelectedBaseColor;
        public string SelectedBaseColor
        {
            get
            {
                return mySelectedBaseColor;
            }
            set
            {
                if (mySelectedBaseColor != value)
                {
                    this.RaiseAndSetIfChanged(ref mySelectedBaseColor, value);

                    ExecuteChangeBaseColorsCommand(mySelectedBaseColor);
                }
            }
        }


        public Theme mySelectedTheme;
        public Theme SelectedTheme
        {
            get { return mySelectedTheme; }
            set
            {
                this.RaiseAndSetIfChanged(ref mySelectedTheme, value);
                ;
            }
        }

        private Skin _selectedSkin;

        public Skin SelectedSkin
        {
            get { return _selectedSkin; }
            set
            {

                this.RaiseAndSetIfChanged(ref _selectedSkin, value);
            }
        }

        private ReadOnlyObservableCollection<Skin> _skins;

        public ReadOnlyObservableCollection<Skin> Skins
        {
            get { return _skins; }
            private set
            {
                this.RaiseAndSetIfChanged(ref _skins, value);
            }
        }

        private FolderItem _folder;

        public FolderItem Folder
        {
            get { return _folder; }
            set
            {
                this.RaiseAndSetIfChanged(ref _folder, value);
            }
        }





        public MainWindowViewModel()
        {
            //ThemeManager.Instance.PropertyChanged += ThemeManager_PropertyChanged;

            SampleData.Seed();
            this.Albums = SampleData.Albums;
            this.Artists = SampleData.Artists;


            ColorSchemes = ThemeManager.Instance.ColorSchemes;
            BaseColors = ThemeManager.Instance.BaseColors;

            ThemeManager.Instance.IsThemeChanged += (o, e) =>
                  {
                      BrushResources = FindBrushResources();
                  };


            if (ThemeManager.Instance.SelectedTheme == null)
            {
                ThemeManager.Instance.SelectedTheme = ThemeManager.Instance.Themes.FirstOrDefault();
            }

            if (ThemeManager.Instance.SelectedBaseColor == null)
            {
                ThemeManager.Instance.SelectedBaseColor = ThemeManager.Instance.BaseColors.FirstOrDefault();
            }

            Skins = SkinManager.Instance.Skins;

            if (SkinManager.Instance.SelectedSkin == null)
            {
                SkinManager.Instance.SelectedSkin = SkinManager.Instance.Skins.FirstOrDefault();
            }

            SelectedSkin = SkinManager.Instance.SelectedSkin;

            this.WhenAnyValue(x => x.SelectedSkin).Where(x => x != null).Subscribe(x =>
               {
                   SkinManager.Instance.SelectedSkin = x;
               });

            ChangeColorSchemeCommand = ReactiveCommand.Create<object>(x => ExecuteChangeColorSchemeCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            ChangeBaseColorsCommand = ReactiveCommand.Create<string>(x => ExecuteChangeBaseColorsCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            var canExecute = this.WhenAny(x => x, (test) => true == false);

            ArtistsDropDownCommand = ReactiveCommand.Create<object>(x => ExecuteArtistsDropDownCommand(x), canExecute, outputScheduler: RxApp.MainThreadScheduler);
            GenreDropDownMenuItemCommand = ReactiveCommand.Create<object>(x => ExecuteGenreDropDownMenuItemCommand(x), outputScheduler: RxApp.MainThreadScheduler);


            ShowBusyIndicator = ReactiveCommand.Create<object>(x => ExecuteShowBusyIndicator(x), outputScheduler: RxApp.MainThreadScheduler);
            OpenWizardCommand = ReactiveCommand.Create<object>(x => ExecuteOpenWizardCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            SelectedColorScheme = ColorSchemes.FirstOrDefault(x => x.Name == ThemeManager.Instance.SelectedTheme.ColorScheme);
            SelectedBaseColor = ThemeManager.Instance.SelectedBaseColor;
            SelectedTheme = ThemeManager.Instance.SelectedTheme;


            CurrentHamburgerMenuContent = new HamburgerMenuHomeView();
            HamburgerMenuHomeCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuHomeCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuSearchCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuSearchCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuLikesCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuLikesCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuListsCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuListsCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuProfileCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuProfileCommand(x), outputScheduler: RxApp.MainThreadScheduler);




        }



        private void ExecuteHamburgerMenuProfileCommand(object x)
        {
            CurrentHamburgerMenuContent = new HamburgerMenuProfileView();
        }

        private void ExecuteHamburgerMenuListsCommand(object x)
        {
            CurrentHamburgerMenuContent = new HamburgerMenuListView();
        }

        private void ExecuteHamburgerMenuLikesCommand(object x)
        {
            CurrentHamburgerMenuContent = new HamburgerMenuLikeView();
        }

        private void ExecuteHamburgerMenuSearchCommand(object x)
        {
            CurrentHamburgerMenuContent = new HamburgerMenuSearchView();
        }

        private void ExecuteHamburgerMenuHomeCommand(object x)
        {
            CurrentHamburgerMenuContent = new HamburgerMenuHomeView();
        }

        private void ExecuteOpenWizardCommand(object x)
        {
            MetroWindow metroWindow = new MetroWindow();
            ThemeManager.Instance.EnableTheme(metroWindow);
            metroWindow.Content = new WizardWithCloseView();


            metroWindow.ShowDialog((Application.Current.
                ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow).ConfigureAwait(false);


        }

        private void ExecuteShowBusyIndicator(object x)
        {
            IsBusy = !IsBusy;
            if (IsBusy)
            {
                DispatcherTimer.RunOnce(() =>
                {
                    IsBusy = false;

                }, TimeSpan.FromSeconds(30));
            }
        }


        private void ExecuteGenreDropDownMenuItemCommand(object x)
        {
            //MessageBox
        }

        private void ExecuteArtistsDropDownCommand(object x)
        {

        }

        private void ThemeManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedColorScheme):
                    SelectedColorScheme = ColorSchemes.FirstOrDefault(x => x.Name == ThemeManager.Instance.SelectedTheme.ColorScheme);
                    break;
                case nameof(SelectedBaseColor):
                    SelectedBaseColor = ThemeManager.Instance.SelectedBaseColor;
                    break;
                case nameof(SelectedTheme):
                    SelectedTheme = ThemeManager.Instance.SelectedTheme;
                    break;
            }

            if (BrushResources.Count == 0)
            {
                BrushResources = FindBrushResources();
            }


        }

        private void ExecuteChangeBaseColorsCommand(object item)
        {
            string baseColor = item as string;
            ThemeManager.Instance.ChangeBaseColor(baseColor);
            SelectedBaseColor = baseColor;
        }

        private void ExecuteChangeColorSchemeCommand(object item)
        {
            ColorScheme colorScheme = item as ColorScheme;
            ThemeManager.Instance.ChangeColorScheme(colorScheme);
            SelectedColorScheme = colorScheme;
        }

        private AvaloniaList<BrushResource> FindBrushResources()
        {
            IClassicDesktopStyleApplicationLifetime desktopLifetime =
                Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (desktopLifetime != null
                && desktopLifetime.MainWindow != null
                )
            {
                var theme = ThemeManager.Instance.DetectTheme(desktopLifetime.MainWindow);
                Style style = ((theme.ThemeStyle as StyleInclude).Loaded as Style);

                var resources = style.Resources.Keys.Cast<object>()
                                     .Where(key => style.Resources[key] is SolidColorBrush)
                                     .Select(key => new BrushResource { Key = key.ToString(), Brush = style.Resources[key] as SolidColorBrush })
                                     .OrderBy(s => s.Key)
                                     .ToList();

                return new AvaloniaList<BrushResource>(resources);
            }

            return new AvaloniaList<BrushResource>();
        }


    }
}
