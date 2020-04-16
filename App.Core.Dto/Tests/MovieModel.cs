using System.Collections.Generic;

namespace App.Core.Dto.Tests
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public short ReleaseYear { get; set; }
    }

    public class DirectorMovie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public short ReleaseYear { get; set; }
    }

    public class DirectorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<DirectorMovie> Movies { get; set; }
    }

    public class CreateMovieModel
    {
        public string Name { get; set; }
        public int DirectorId { get; set; }
        public short ReleaseYear { get; set; }
    }
}
