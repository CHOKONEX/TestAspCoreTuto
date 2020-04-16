using App.Core.Dto.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    public interface IDapperMovieRepository
    {
        Task<IEnumerable<MovieModel>> GetAllMovies();
        Task<MovieModel> GetMovieById(int id);
        Task<DirectorModel> GetDirectorAndMovies(int id);
        Task<IEnumerable<DirectorModel>> GetAllDirectors();
        Task<IEnumerable<DirectorModel>> GetDirectors();
        Task<IEnumerable<string>> GetDirectorsIdentities();
        Task<int> AddMovie(CreateMovieModel movie);
    }
}