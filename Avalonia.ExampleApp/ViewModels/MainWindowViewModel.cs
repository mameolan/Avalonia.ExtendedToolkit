using Avalonia.ExampleApp.Model;
using Avalonia.ExtendedToolkit;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Avalonia.ExampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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
                this.RaiseAndSetIfChanged(ref mySelectedTheme, value); ;
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


            if (ThemeManager.Instance.SelectedTheme == null)
            {
                ThemeManager.Instance.SelectedTheme = ThemeManager.Instance.Themes.FirstOrDefault();
            }

            if (ThemeManager.Instance.SelectedBaseColor == null)
            {
                ThemeManager.Instance.SelectedBaseColor = ThemeManager.Instance.BaseColors.FirstOrDefault();
            }

            ChangeColorSchemeCommand = ReactiveCommand.Create<object>(x => ExecuteChangeColorSchemeCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            ChangeBaseColorsCommand = ReactiveCommand.Create<string>(x => ExecuteChangeBaseColorsCommand(x), outputScheduler: RxApp.MainThreadScheduler);

            var canExecute = this.WhenAny(x => x, (test) => true == false);

            ArtistsDropDownCommand = ReactiveCommand.Create<object>(x => ExecuteArtistsDropDownCommand(x), canExecute, outputScheduler: RxApp.MainThreadScheduler);
            GenreDropDownMenuItemCommand = ReactiveCommand.Create<object>(x => ExecuteGenreDropDownMenuItemCommand(x), outputScheduler: RxApp.MainThreadScheduler);


            SelectedColorScheme = ColorSchemes.FirstOrDefault(x => x.Name == ThemeManager.Instance.SelectedTheme.ColorScheme);
            SelectedBaseColor = ThemeManager.Instance.SelectedBaseColor;
            SelectedTheme = ThemeManager.Instance.SelectedTheme;
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


    }
}
