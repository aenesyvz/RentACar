﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager:IPaymentService
    {
        IPaymentDal _paymentDal;
        ICardService _cardService;

        public PaymentManager(IPaymentDal paymentDal, ICardService cardService)
        {
            _paymentDal = paymentDal;
            _cardService = cardService;

        }

        public IResult Add(Payment payment)
        {
          /*  var result = BusinessRules.Run(CheckCardExist(payment.CreditCardNumber, payment.ExpirationDate,
                payment.SecurityCode));
            if (result != null)
            {
                return new ErrorResult();
            }
          */
            _paymentDal.Add(payment);
            return new SuccessResult();
        }

       // [SecuredOperation("admin")]
        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IDataResult<Payment> Get(Payment payment)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(x => x.PaymentId == payment.PaymentId));
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }


        public IDataResult<Payment> GetByPaymentId(int paymentId)
        {
            var result = _paymentDal.Get(x => x.PaymentId == paymentId);
            if (result == null)
            {
                return new ErrorDataResult<Payment>();
            }

            return new SuccessDataResult<Payment>(result);
        }

        //[SecuredOperation("admin")]
        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult();
        }


        private IResult CheckCardExist(string cardNumber, string expiration, string securityCode)
        {
            var result = _cardService.GetbyCardNumber(cardNumber);
            if (result.Data.ExpirationDate == expiration && result.Data.SecurityCode == securityCode)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.cardNotFound);
        }
    }
}
