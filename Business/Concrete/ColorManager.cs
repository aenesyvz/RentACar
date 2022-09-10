using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System;
using System.Linq;
using Core.Utilities.Business;
using Business.Constants;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal dal)
        {
            _colorDal = dal;
        }

        //[ValidationAspect(typeof(ColorValidator))]
        //[SecuredOperation("admin")]
        //[CacheRemoveAspect("IColorService.get")]
        public IResult Add(Color color)
        {
            _colorDal.Add(color); ;
            return new SuccessResult(Messages.ColorAdded);
        }

        //[SecuredOperation("admin")]
        //[CacheRemoveAspect("IColorService.get")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        //[CacheAspect()]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            var result = BusinessRules.Run(CheckIfColorExist(colorId));
            if (result != null)
            {
                return new ErrorDataResult<Color>(Messages.ColorNotFound);
            }
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId));
        }

        //[SecuredOperation("admin")]
        //[CacheRemoveAspect("IColorService.get")]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        private IDataResult<int> CheckIfColorExist(int colorId)
        {
            var result = _colorDal.Get(c => c.ColorId == colorId);
            if (result == null)
            {
                return new ErrorDataResult<int>();
            }
            return new SuccessDataResult<int>(colorId);
        }
    }
}
