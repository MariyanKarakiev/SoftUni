﻿namespace Streamy.Core.Models
{
    public class AlbumModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        public short GenreId { get; set; }
        public GenreModel Genre { get; set; }

        public Guid ArtistId { get; set; }
        public ArtistModel Artist { get; set; }

        public List<SongModel>  Songs { get; set; }
    }
}