using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// menage all the skins
    /// </summary>
    public class SkinManager : ReactiveObject
    {
        private const string OfficeBlueStylePrefix = "Blue";
        private const string OfficeSilverStylePrefix = "Silver";
        private const string OfficeBlackStylePrefix = "Black";
        private const string Windows7StylePrefix = "Win7";
        private const string MahAppsStylePrefix = "MahApps";

        private const string FormatedStyleResource = "avares://Avalonia.ExtendedToolkit/Styles/Skins/{0}Skin.xaml";
        
        private static object _lockObject = new object();

        private List<WindowBase> registeredWindows = new List<WindowBase>();
        private readonly ObservableCollection<Skin> skinsInternal;
        private readonly ReadOnlyObservableCollection<Skin> skins;

        /// <summary>
        /// a readonly collection of available skins
        /// </summary>
        public ReadOnlyObservableCollection<Skin> Skins
        {
            get
            {
                EnsureSkins();

                return skins;
            }
        }

        private Skin _selectedSkin;
        /// <summary>
        /// Current selected skin
        /// </summary>
        public Skin SelectedSkin
        {
            get { return _selectedSkin; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSkin, value);
                OnChangeSkin(value);
            }
        }

        /// <summary>
        /// event which is fired if a skin has changed
        /// </summary>
        public event EventHandler<OnSkinChangedEventArgs> IsSkinChanged;

        /// <summary>
        /// executes the IsSkinChanged event if registered
        /// </summary>
        /// <param name="skin"></param>
        private void OnChangeSkin(Skin skin)
        {
            IsSkinChanged?.Invoke(this, new OnSkinChangedEventArgs(skin));
        }

        private static SkinManager _instance;
        /// <summary>
        /// instance of the skin manager
        /// </summary>
        public static SkinManager Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new SkinManager();
                        _instance.EnsureSkins();
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// initialise the lists
        /// </summary>
        private SkinManager()
        {
            skinsInternal = new ObservableCollection<Skin>();
            skins = new ReadOnlyObservableCollection<Skin>(skinsInternal);

            ThemeManager.Instance.IsThemeChanged += ThemeManager_ThemeChanged;

        }

        private void ThemeManager_ThemeChanged(object sender, OnThemeChangedEventArgs e)
        {
            var lastSkin = SelectedSkin;
            SelectedSkin = null;
            SelectedSkin = lastSkin;
        }

        /// <summary>
        /// enables the skin for the given window
        /// </summary>
        /// <param name="window"></param>
        public void EnableSkin(Window window)
        {
            IDisposable disposableForSelectedSkin = null;
            window.Opened += (o, e) =>
            {
                disposableForSelectedSkin = this.WhenAnyValue(x => x.SelectedSkin).Where(x => x != null)
                  .Subscribe(x =>
                  {
                      var item = window.Styles.GetSkinStyle();

                      IStyle style = x.SkinStyle;

                      if (item == null)
                      {
                          window.Styles.Add(style);
                      }
                      else
                      {
                          window.Styles.Remove(item);
                          window.Styles.Add(style);
                      }
                  });

                if (registeredWindows.Contains(window) == false)
                {
                    registeredWindows.Add(window);
                }
            };

            window.Closing += (o, e) =>
            {
                disposableForSelectedSkin?.Dispose();
                registeredWindows.Remove(window);
            };
        }

        /// <summary>
        /// loads the skins and sets the selected skin to the first item.
        /// </summary>
        private void EnsureSkins()
        {
            if (skins.Count > 0)
                return;

            foreach (SkinType item in Enum.GetValues(typeof(SkinType)))
            {
                string xamlFile = string.Empty;
                switch (item)
                {
                    case SkinType.OfficeBlack:
                        xamlFile = string.Format(FormatedStyleResource, OfficeBlackStylePrefix);
                        break;

                    case SkinType.OfficeBlue:
                        xamlFile = string.Format(FormatedStyleResource, OfficeBlueStylePrefix);
                        break;

                    case SkinType.OfficeSilver:
                        xamlFile = string.Format(FormatedStyleResource, OfficeSilverStylePrefix);
                        break;

                    case SkinType.Windows7:
                        xamlFile = string.Format(FormatedStyleResource, Windows7StylePrefix);
                        break;

                    case SkinType.MahApps:
                        xamlFile = string.Format(FormatedStyleResource, MahAppsStylePrefix);
                        break;
                }

                var skinStyle = new StyleInclude(new Uri($"resm:Styles?assembly={this.GetType().Namespace}"))
                {
                    Source = new Uri(xamlFile)
                };

                skinsInternal.Add(new Skin(item, skinStyle));
            }

            SelectedSkin = skinsInternal.FirstOrDefault(x=> x.SkinType== SkinType.MahApps);
        }
    }
}