using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IBrandService _brandService;
        private IColorService _colorService;

        public CarManager(ICarDal dal, IBrandService brandService, IColorService colorService)
        {
            _carDal = dal;
            _brandService = brandService;
            _colorService = colorService;
        }

        //[CacheRemoveAspect("ICarService.get")]
        //[SecuredOperation("admin")]
        //[ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        //[CacheRemoveAspect("ICarService.get")]
        //[SecuredOperation("admin")]
        public IResult Update(Car car)
        {
            var result = BusinessRules.Run(IsCarExist(car.Id));
            if (result != null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        //[CacheRemoveAspect("ICarService.get")]
        //[SecuredOperation("admin")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<Car> GetByCarId(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        //[CacheAspect]
        public IDataResult<List<CarDto>> GetCarsWithDetails()
        {
            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails(), Messages.CarDetailBrought);
        }

        public IDataResult<List<CarDto>> GetCarDetails(int id)
        {
            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails(c => c.Id == id), Messages.CarDetailBrought);
        }

        //[CacheAspect]
        public IDataResult<List<CarDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId), Messages.CarDetailBrought);
        }

        //[CacheAspect]
        public IDataResult<List<CarDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId), Messages.CarDetailBrought);
        }

        //[CacheAspect]
        public IDataResult<List<CarDto>> GetCarDetailsByBrandAndColor(int brandId, int colorId)
        {
            var result = BusinessRules.Run(IsColorExists(colorId), IsBrandExists(brandId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDto>>(Messages.CarsCouldntListed);
            }


            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<int> GetCarFindeks(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);
            return new SuccessDataResult<int>(result.Findeks);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.CarsListed);
        }

        //Business Codes

        private IResult IsBrandExists(int brandId)
        {
            var result = _brandService.GetById(brandId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult IsColorExists(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult IsCarExist(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}
