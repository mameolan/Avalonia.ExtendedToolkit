using System.Collections.ObjectModel;
using ReactiveUI;

namespace Avalonia.ExampleApp.Model
{
    public class Artist: ReactiveObject
    {
        private int _artistId;
        private string _name;
        private ObservableCollection<Album> _albums;

        public int ArtistId
        {
            get => this._artistId;
            set => this.RaiseAndSetIfChanged(ref this._artistId, value);
        }

        public string Name
        {
            get => this._name;
            set => this.RaiseAndSetIfChanged(ref this._name, value);
        }

        public ObservableCollection<Album> Albums
        {
            get => this._albums;
            set => this.RaiseAndSetIfChanged(ref this._albums, value);
        }
    }
}
