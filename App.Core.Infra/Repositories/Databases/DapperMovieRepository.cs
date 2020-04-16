using App.Core.Dto.Tests;
using App.Core.Infra.Database;
using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Attributes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    [Singleton]
    public class DapperMovieRepository : IDapperMovieRepository
    {
        private readonly IDatabaseReader _databaseReader;
        private readonly ISqlFileQueryReader _sqlFileQueryReader;
        private readonly IDatabaseExecutor _databaseExecutor;

        public DapperMovieRepository(ISqlFileQueryReader sqlFileQueryReader, IDatabaseReader databaseReader, IDatabaseExecutor databaseExecutor)
        {
            _databaseReader = databaseReader ?? throw new ArgumentNullException(nameof(databaseReader)); ;
            _sqlFileQueryReader = sqlFileQueryReader ?? throw new ArgumentNullException(nameof(sqlFileQueryReader));
            _databaseExecutor = databaseExecutor ?? throw new ArgumentNullException(nameof(databaseExecutor));
        }

        public async Task<IEnumerable<MovieModel>> GetAllMovies()
        {
            string sql = _sqlFileQueryReader.GetQuery("GetAllMovies.sql");
            return await _databaseReader.ReadManyAsync<MovieModel>(sql);
        }

        public async Task<MovieModel> GetMovieById(int id)
        {
            string sql = _sqlFileQueryReader.GetQuery("GetMovieById.sql");
            object param = new { Id = id };

            return await _databaseReader.QueryFirstOrDefaultAsync<MovieModel>(sql, param);
        }

        public async Task<IEnumerable<DirectorModel>> GetAllDirectors()
        {
            string sql = _sqlFileQueryReader.GetQuery("GetAllDirectors.sql");
            object param = null;

            var directorDictionary = new Dictionary<int, DirectorModel>();
            var result = await _databaseReader.ReadOneToManyAsync<DirectorModel, DirectorMovie, DirectorModel>(sql,
                (dir, mov) => MapToDirector(dir, mov, directorDictionary),
                param, "DirectorId");
            return result.Distinct();
        }

        public async Task<DirectorModel> GetDirectorAndMovies(int id)
        {
            string sql = _sqlFileQueryReader.GetQuery("GetDirectorAndHisMovies.sql");
            object param = new { Id = id };
            return await _databaseReader.QueryMultipleAsync(sql, MapMultiple, param);
        }

        private DirectorModel MapMultiple(SqlMapper.GridReader results)
        {
            var director = results.ReadSingle<DirectorModel>();
            director.Movies = results.Read<DirectorMovie>().ToList();
            return director;
        }

        private DirectorModel MapToDirector(DirectorModel dir, DirectorMovie mov, Dictionary<int, DirectorModel> directorDictionary)
        {
            if (!directorDictionary.TryGetValue(dir.Id, out DirectorModel director))
            {
                director = dir;
                director.Movies = new List<DirectorMovie>();
                directorDictionary.Add(director.Id, director);
            }
            director.Movies.Add(mov);
            return director;
        }

        public async Task<IEnumerable<DirectorModel>> GetDirectors()
        {
            string sql = _sqlFileQueryReader.GetQuery("GetDirectors.sql");
            return await _databaseReader.ExecuteReaderAsync(sql, this.MapDirectorsFromIDataReader);
        }

        private IEnumerable<DirectorModel> MapDirectorsFromIDataReader(IDataReader reader)
        {
            var directorDictionary = new Dictionary<int, DirectorModel>();
            while (reader.Read())
            {
                int dirId = (int)reader["Id"];
                if (!directorDictionary.TryGetValue(dirId, out DirectorModel director))
                {
                    director = new DirectorModel();
                    director.Id = (int)reader["Id"];
                    director.Name = (string)reader["Name"];
                    directorDictionary.Add(director.Id, director);
                }
            }
            return directorDictionary.Select(x => x.Value);
        }

        public async Task<IEnumerable<string>> GetDirectorsIdentities()
        {
            string sql = _sqlFileQueryReader.GetQuery("GetDirectors.sql");
            return await _databaseReader.ExecuteReaderAsync(sql, this.MapDirectorsFromDataReaderDynamic);
        }

        private IEnumerable<string> MapDirectorsFromDataReaderDynamic(IDataReader reader)
        {
            var directors = new List<string>();

            Dictionary<int, Parser> columnsParsers = new Dictionary<int, Parser>();
            for (int index = 0; index < reader.FieldCount; index++)
            {
                columnsParsers.Add(index, new Parser(reader));
            }

            while (reader.Read())
            {
                StringBuilder sb = new StringBuilder();
                for (int index = 0; index < columnsParsers.Count; index++)
                {
                    Parser columnParser = columnsParsers[index];
                    sb.Append(columnParser.Parse(index)).Append(" | ");
                }

                directors.Add(sb.ToString());
            }
            return directors;
        }

        public async Task<int> AddMovie(CreateMovieModel movie)
        {
            string sql = _sqlFileQueryReader.GetQuery("AddMovie.sql");
            object param = new { movie.Name, movie.DirectorId, movie.ReleaseYear };

            return await _databaseExecutor.ExecuteAsync(sql, param);
        }

        public class Parser
        {
            private readonly IDataReader _reader;

            public Parser(IDataReader reader)
            {
                _reader = reader;
            }

            public string Parse(int index)
            {
                if (_reader.IsDBNull(index))
                {
                    return string.Empty;
                }

                if (_reader.GetFieldType(index) == typeof(int))
                {
                    return _reader.GetName(index) + " (int) = " + _reader.GetInt32(index);
                }

                if (_reader.GetFieldType(index) == typeof(string))
                {
                    return _reader.GetName(index) + " (string) = " + _reader.GetString(index);
                }

                return string.Empty;
            }
        }
    }
}