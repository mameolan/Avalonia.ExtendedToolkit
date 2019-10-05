using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Avalonia.ExampleApp.Model
{
    public class Genre: ReactiveObject
    {
        private int _genreId;
        private string _name;
        private string _description;
        private ObservableCollection<Album> _albums;

        public int GenreId
        {
            get => this._genreId;
            set => this.RaiseAndSetIfChanged(ref this._genreId, value);
        }

        public string Name
        {
            get => this._name;
            set => this.RaiseAndSetIfChanged(ref this._name, value);
        }

        public string Description
        {
            get => this._description;
            set => this.RaiseAndSetIfChanged(ref this._description, value);
        }

        public ObservableCollection<Album> Albums
        {
            get => this._albums;
            set => this.RaiseAndSetIfChanged(ref this._albums, value);
        }
    }
}