﻿using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Update(Payment payment);
        IDataResult<List<Payment>> GetAll();
        IDataResult<Payment> Get(Payment payment);
        IResult Delete(Payment payment);
        IResult Add(Payment payment);
        IDataResult<Payment> GetByPaymentId(int paymentId);
    }
}
