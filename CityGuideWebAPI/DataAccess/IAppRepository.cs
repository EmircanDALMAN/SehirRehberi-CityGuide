using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGuideWebAPI.Entities;

namespace CityGuideWebAPI.DataAccess
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        List<City> GetCities();
        List<Photo> GetPhotosByCityId(int cityId);
        City GetCityById(int cityId);
        Photo GetPhotoById(int photoId);
    }
}
