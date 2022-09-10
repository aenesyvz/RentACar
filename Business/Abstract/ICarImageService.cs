using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetCarImagesById(int carId);
        IDataResult<CarImage> GetById(int imageId);
        IResult Add(Image image, CarImage carImage);
        IResult Update(CarImage carImage, Image file);
        IResult Delete(CarImage carImage);
        IResult DeleteAllImagesOfCarByCarId(int carId);
    }
}
