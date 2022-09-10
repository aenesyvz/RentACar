using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDto> GetCarDetails(Expression<Func<CarDto, bool>> filter = null)
        {
            using (var context = new RentACarContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 CarName = c.CarName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 Findeks = c.Findeks,
                                 
                                 IsRentable = !context.Rentals.Any(r => r.CarId == c.Id && (r.ReturnDate == null || (r.ReturnDate.HasValue && r.ReturnDate > DateTime.Now))),
                                 ImagePath = (from ci in context.CarImages where c.Id == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
