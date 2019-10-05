using ReactiveUI;
using System.ComponentModel;

namespace Avalonia.ExampleApp.Model
{
    public class Album: ReactiveObject
    {
        private int _albumId;
        private int _genreId;
        private int _artistId;
        private string _title;
        private decimal _price;
        private Genre _genre;
        private Artist _artist;
        private bool isSelected;

        public int AlbumId
        {
            get => this._albumId;
            set => this.RaiseAndSetIfChanged(ref this._albumId, value);
        }

        [DisplayName("Genre")]
        public int GenreId
        {
            get => this._genreId;
            set => this.RaiseAndSetIfChanged(ref this._genreId, value);
        }

        [DisplayName("Artist")]
        public int ArtistId
        {
            get => this._artistId;
            set => this.RaiseAndSetIfChanged(ref this._artistId, value);
        }

        public bool IsSelected
        {
            get => this.isSelected;
            set => this.RaiseAndSetIfChanged(ref this.isSelected, value);
        }

        public string Title
        {
            get => this._title;
            set => this.RaiseAndSetIfChanged(ref this._title, value);
        }

        public decimal Price
        {
            get => this._price;
            set => this.RaiseAndSetIfChanged(ref this._price, value);
        }

        [DisplayName("Album Art URL")]
        public string AlbumArtUrl { get; set; }

        public virtual Genre Genre
        {
            get => this._genre;
            set => this.RaiseAndSetIfChanged(ref this._genre, value);
        }

        public virtual Artist Artist
        {
            get => this._artist;
            set => this.RaiseAndSetIfChanged(ref this._artist, value);
        }
    }
}