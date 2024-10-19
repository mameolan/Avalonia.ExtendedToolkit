using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ExampleApp.Model;
using Avalonia.ExampleApp.Views;
using Avalonia.ExampleApp.Window;
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
using System.Diagnostics;
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

        private DataGridCollectionView _groupedAlbums;
        public DataGridCollectionView GroupedAlbums
        {
            get { return _groupedAlbums; }
            set
            {
                this.RaiseAndSetIfChanged(ref _groupedAlbums, value);
            }
        }

        private ObservableCollection<Artist> _artists;
        public ObservableCollection<Artist> Artists
        {
            get { return _artists; }
            set { this.RaiseAndSetIfChanged(ref _artists, value); }
        }

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get { return _selectedArtist; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedArtist, value);
            }
        }


        public ICommand GenreDropDownMenuItemCommand { get; }

        public ICommand ChangeColorSchemeCommand { get; }

        public ICommand ArtistsDropDownCommand { get; }

        public ICommand ChangeBaseColorsCommand { get; }

        public ICommand ShowBusyIndicatorCommand { get; }

        public ICommand ShowFlyoutDemoCommand { get; }

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

        private ObservableCollection<Dock> _tabStripPlacementValues;
        public ObservableCollection<Dock> TabStripPlacementValues
        {
            get { return _tabStripPlacementValues; }
            set
            {
                this.RaiseAndSetIfChanged(ref _tabStripPlacementValues, value);
            }
        }

        private Dock _selectedTabStripPlacement;
        public Dock SelectedTabStripPlacement
        {
            get { return _selectedTabStripPlacement; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTabStripPlacement, value);
            }
        }

        private ObservableCollection<UnderlinedType> _underlineValues;
        public ObservableCollection<UnderlinedType> UnderlineValues
        {
            get { return _underlineValues; }
            set
            {
                this.RaiseAndSetIfChanged(ref _underlineValues, value);
            }
        }


        private UnderlinedType _selectedUnderline;
        public UnderlinedType SelectedUnderline
        {
            get { return _selectedUnderline; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedUnderline, value);
            }
        }

        private ObservableCollection<Dock?> _underlinedPlacementValues;
        public ObservableCollection<Dock?> UnderlinedPlacementValues
        {
            get { return _underlinedPlacementValues; }
            set
            {
                this.RaiseAndSetIfChanged(ref _underlinedPlacementValues, value);
            }
        }

        private Dock? _selectedUnderlinedPlacemet;
        public Dock? SelectedUnderlinedPlacement
        {
            get { return _selectedUnderlinedPlacemet; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedUnderlinedPlacemet, value);
            }
        }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set
            {
                this.RaiseAndSetIfChanged(ref _categories, value);
            }
        }

        private ObservableCollection<string> _suggestedTags;
        public ObservableCollection<string> SuggestedTags

        {
            get { return _suggestedTags; }
            set
            {
                this.RaiseAndSetIfChanged(ref _suggestedTags, value);
            }
        }


        private string _selectedItem;
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);
            }
        }

        private int _currentPageIndex = 1;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentPageIndex, value);
            }
        }

        private ObservableCollection<Album> _albumsPagnitation = new ObservableCollection<Album>();
        public ObservableCollection<Album> AlbumsPagnitation
        {
            get { return _albumsPagnitation; }
            set { this.RaiseAndSetIfChanged(ref _albumsPagnitation, value); }
        }

        public ICommand PageChangedCommand { get; set; }



        public MainWindowViewModel()
        {
            //ThemeManager.Instance.PropertyChanged += ThemeManager_PropertyChanged;

            SampleData.Seed();

            this.Albums = SampleData.Albums;
            this.Artists = SampleData.Artists;

            SelectedArtist=Artists.FirstOrDefault();

            var collectionView = new DataGridCollectionView(SampleData.Albums);
            collectionView.GroupDescriptions.Add(new DataGridPathGroupDescription(nameof(Album.Artist.Name)));
            GroupedAlbums = collectionView;


            ColorSchemes = ThemeManager.Instance.ColorSchemes;
            BaseColors = ThemeManager.Instance.BaseColors;

            //ThemeManager.Instance.IsThemeChanged += (o, e) =>
            //      {
            //          BrushResources = FindBrushResources();
            //      };


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


            ShowBusyIndicatorCommand = ReactiveCommand.Create<object>(x => ExecuteShowBusyIndicator(x), outputScheduler: RxApp.MainThreadScheduler);
            OpenWizardCommand = ReactiveCommand.Create<object>(x => ExecuteOpenWizardCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            ShowFlyoutDemoCommand = ReactiveCommand.Create<object>(x => ExecuteShowFlyoutDemo(x), outputScheduler: RxApp.MainThreadScheduler);


            SelectedColorScheme = ColorSchemes.FirstOrDefault(x => x.Name == ThemeManager.Instance.SelectedTheme.ColorScheme);
            SelectedBaseColor = ThemeManager.Instance.SelectedBaseColor;
            SelectedTheme = ThemeManager.Instance.SelectedTheme;


            CurrentHamburgerMenuContent = new HamburgerMenuHomeView();
            HamburgerMenuHomeCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuHomeCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuSearchCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuSearchCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuLikesCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuLikesCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuListsCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuListsCommand(x), outputScheduler: RxApp.MainThreadScheduler);
            HamburgerMenuProfileCommand = ReactiveCommand.Create<object>(x => ExecuteHamburgerMenuProfileCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            UnderlinedPlacementValues = new ObservableCollection<Dock?>();
            UnderlinedPlacementValues.Add(null);
            foreach (Dock entry in Enum.GetValues(typeof(Dock)))
            {
                UnderlinedPlacementValues.Add(entry);
            }
            SelectedUnderlinedPlacement = UnderlinedPlacementValues.FirstOrDefault();

            TabStripPlacementValues = new ObservableCollection<Dock>();
            foreach (Dock entry in Enum.GetValues(typeof(Dock)))
            {
                TabStripPlacementValues.Add(entry);
            }
            SelectedTabStripPlacement = TabStripPlacementValues.FirstOrDefault();

            UnderlineValues = new ObservableCollection<UnderlinedType>();
            foreach (UnderlinedType entry in Enum.GetValues(typeof(UnderlinedType)))
            {
                UnderlineValues.Add(entry);
            }
            SelectedUnderline = UnderlineValues.FirstOrDefault();

            Categories = new ObservableCollection<string>();
            Categories.Add("TagItem");

            SuggestedTags = ["c#", "dotnet", "c++"];

            PageChangedCommand = ReactiveCommand.Create<int>(x => UpdatePaginationItems(x), outputScheduler: RxApp.MainThreadScheduler);

            SampleData.Albums.Take(10).ToList().ForEach(item => AlbumsPagnitation.Add(item));

        }

        private void UpdatePaginationItems(int val)
        {
            AlbumsPagnitation = new ObservableCollection<Album>(SampleData.Albums.Skip((val - 1) * 10).Take(10).ToList());
        }

        private void ExecuteShowFlyoutDemo(object x)
        {
            FlyoutDemoWindow window = new FlyoutDemoWindow();
            window.ShowDialog((Application.Current.
                ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow).ConfigureAwait(false);
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
