using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Helpers
{
    public class MapHelper
    {
        internal static IMapper mapper;

        static MapHelper()
        {
            var config = new MapperConfiguration(x => {
                x.CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
                x.CreateMap<Persona, PersonaViewModel>().ReverseMap();
                x.CreateMap<Movie, MovieViewModel>().ReverseMap();
                x.CreateMap<Movie, ThumbnailMovieViewModel>().ReverseMap();
                x.CreateMap<Media, MediaViewModel>().ReverseMap();
                x.CreateMap<Favorito, FavoritoViewModel>().ReverseMap();
                x.CreateMap<Serie, SerieViewModel>().ReverseMap();
                x.CreateMap<Serie, DetalladoSerieViewModel>().ReverseMap();
                x.CreateMap<Serie, NuevoSerieViewModel>().ReverseMap();
                x.CreateMap<Serie, ModificadoSerieViewModel>().ReverseMap();
                x.CreateMap<Serie, EliminadoSerieViewModel>().ReverseMap();
                x.CreateMap<Serie, ThumbnailSerieViewModel>().ReverseMap();
                x.CreateMap<Serie, CompletoSerieViewModel>().ReverseMap();
                x.CreateMap<Opinion, OpinionViewModel>().ReverseMap();

                x.CreateMap<Episodio, EpisodioViewModel>().ReverseMap();
                x.CreateMap<Episodio, NuevoEpisodioViewModel>().ReverseMap();
                x.CreateMap<Episodio, ModificadoEpisodioViewModel>().ReverseMap();
                x.CreateMap<Episodio, EpisodioCarrucelViewModel>().ReverseMap();
                x.CreateMap<Genero, GeneroViewModel>().ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public static T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }
    }
}
