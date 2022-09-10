using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
        IDataResult<Car> GetByCarId(int id);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<List<CarDto>> GetCarsWithDetails();
        IDataResult<List<CarDto>> GetCarDetails(int id);
        IDataResult<List<CarDto>> GetCarDetailsByBrandId(int brandId);
        IDataResult<List<CarDto>> GetCarDetailsByColorId(int colorId);
        IDataResult<List<CarDto>> GetCarDetailsByBrandAndColor(int brandId, int colorId);
        IDataResult<int> GetCarFindeks(int carId);


    }
}
