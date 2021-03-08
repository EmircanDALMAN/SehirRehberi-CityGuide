using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGuideWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityGuideWebAPI.DataAccess
{
    public class AppRepository : IAppRepository
    {
        private readonly CityGuideContext _context;

        public AppRepository(CityGuideContext context)
        {
            _context = context;
        }


        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public List<City> GetCities()
        {
            return _context.Cities.Include(c => c.Photos).ToList();
        }

        public List<Photo> GetPhotosByCityId(int cityId)
        {
            return _context.Photos.Where(p => p.CityId == cityId).ToList();
        }

        public City GetCityById(int cityId)
        {
            return _context.Cities.Include(c => c.Photos).FirstOrDefault(c => c.Id == cityId);
        }

        public Photo GetPhotoById(int photoId)
        {
            return _context.Photos.FirstOrDefault(p => p.Id == photoId);
        }
    }
}
