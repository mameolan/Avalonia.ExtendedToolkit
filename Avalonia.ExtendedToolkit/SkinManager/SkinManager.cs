using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Avalonia.ExtendedToolkit
{
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

        public ReadOnlyObservableCollection<Skin> Skins
        {
            get
            {
                EnsureSkins();

                return skins;
            }
        }

        private Skin _selectedSkin;

        public Skin SelectedSkin
        {
            get { return _selectedSkin; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSkin, value);
                OnChangeSkin(value);
            }
        }

        public event EventHandler<OnSkinChangedEventArgs> IsSkinChanged;

        private void OnChangeSkin(Skin skin)
        {
            IsSkinChanged?.Invoke(this, new OnSkinChangedEventArgs(skin));
        }

        private static SkinManager _instance;

        public static SkinManager Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new SkinManager();
                    }
                    return _instance;
                }
            }
        }

        private SkinManager()
        {
            skinsInternal = new ObservableCollection<Skin>();
            skins = new ReadOnlyObservableCollection<Skin>(skinsInternal);
        }

        public void EnableSkin(Window window)
        {
            IDisposable disposableForSelectedSkin = null;
            window.Opened += (o, e) =>
            {
                disposableForSelectedSkin = this.WhenAnyValue(x => x.SelectedSkin).Where(x => x != null)
                  .Subscribe(x =>
                  {
                      var item = window.Styles.GetSkinStyle();

                      //if (x.SkinType == SkinType.MahApps)
                      //{
                      //    var res = Application.Current.Styles.OfType<StyleInclude>()
                      //       .FirstOrDefault(x => x.Source.AbsoluteUri.
                      //       StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Themes/"));

                      //    //Application.Current.Styles.Remove(res);

                      //    IResourceDictionary resource = (res.Loaded as Style).Resources;

                      //    StyleInclude styleIncluded = (x.SkinStyle as StyleInclude);
                      //    Styles styles = styleIncluded.Loaded as Styles;

                      //    int counter = 0;
                      //    bool found = false;
                      //    List<IStyle> stylesToAdd = new List<IStyle>();
                      //    do
                      //    {
                      //        found = false;

                      //        StyleInclude styleInclude = styles[counter] as StyleInclude;
                      //        Styles resourceStyles = styleInclude.Loaded as Styles;

                      //        if (resourceStyles.Resources.ContainsKey("HighlightButtonSolidColor"))
                      //        {
                      //            Color color = Colors.GreenYellow; //(Color)resource["MahApps.Colors.Highlight"];

                      //            //resourceStyles.Resources.Remove("HighlightButtonSolidColor");
                      //            //resourceStyles.Resources.Add(new KeyValuePair<object, object>("HighlightButtonSolidColor", color));
                      //            //resourceStyles.Resources["HighlightButtonSolidColor"] = null;
                      //            resourceStyles.Resources["HighlightButtonSolidColor"] = color;

                      //            found = true;
                      //            counter = 0;
                      //            stylesToAdd.Add(styleInclude);
                      //            styles.Remove(styleInclude);
                      //        }

                      //        if(found==false)
                      //        {
                      //            counter++;
                      //        }

                      //    } while (counter < styles.Count);

                      //    styles.AddRange(stylesToAdd);

                      //    //foreach (StyleInclude style in styles)
                      //    //{
                      //    //    //Color color = (Color)style.FindResource("HighlightButtonSolidColor");
                      //    //    Styles resourceStyles = style.Loaded as Styles;

                      //    //    if(resourceStyles.Resources.ContainsKey("HighlightButtonSolidColor"))
                      //    //    {
                      //    //        Color color = Colors.GreenYellow; //(Color)resource["MahApps.Colors.Highlight"];

                      //    //        //resourceStyles.Resources.Remove("HighlightButtonSolidColor");
                      //    //        //resourceStyles.Resources.Add(new KeyValuePair<object, object>("HighlightButtonSolidColor", color));
                      //    //        //resourceStyles.Resources["HighlightButtonSolidColor"] = null;
                      //    //        resourceStyles.Resources["HighlightButtonSolidColor"]=color;

                      //    //    }

                      //    //}

                      //   // Application.Current.Styles.Add(res);

                      //}

                      if (item == null)
                      {
                          window.Styles.Add(x.SkinStyle);
                      }
                      else
                      {
                          window.Styles.Remove(item);
                          window.Styles.Add(x.SkinStyle);
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

            SelectedSkin = skinsInternal.FirstOrDefault();
        }
    }
}