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
    public class CardManager:ICardService
    {
        ICardDal _cardDal;
        private IUserService _userService;
        public CardManager(ICardDal cardDal, IUserService userService)
        {
            _cardDal = cardDal;
            _userService = userService;
        }

        public IResult Add(Card card)
        {
            var result = BusinessRules.Run(CheckIsCreditCardExist(card.CreditCardNumber, card.ExpirationDate, card.SecurityCode));

            if (result != null)
            {
                return new ErrorResult();
            }
            _cardDal.Add(card);
            return new SuccessResult(Messages.CardAdded);
        }

        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult(Messages.CardDeleted);
        }

        public IDataResult<List<Card>> GetAllCards()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(), Messages.CardListed);
        }


        public IDataResult<Card> GetByCustomerId(int userId)
        {
            var result = BusinessRules.Run(IsCustomerExist(userId));
            if (result == null)
            {
                return new ErrorDataResult<Card>();
            }

            return new SuccessDataResult<Card>(_cardDal.Get(c => c.UserId == userId));
        }


        public IDataResult<List<Card>> GetCardListByCustomerId(int userId)
        {
            var result = BusinessRules.Run(IsCustomerExist(userId));
            if (result != null)
            {
                return new ErrorDataResult<List<Card>>();
            }
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(x => x.UserId == userId));

        }

        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult(Messages.CardUpdated);
        }

        public IDataResult<Card> GetbyCardNumber(string cardNumber)
        {
            var getCardNumber = _cardDal.Get(u => u.CreditCardNumber == cardNumber);
            return new SuccessDataResult<Card>(getCardNumber);
        }

        //Business Rules

        private IResult CheckIsCreditCardExist(string cardNumber, string expirationDate, string securityCode)
        {
            var result = _cardDal.Get(c =>
                c.CreditCardNumber == cardNumber && c.ExpirationDate == expirationDate &&
                c.SecurityCode == securityCode);

            if (result != null)
            {
                return new ErrorResult("Kredi Kartı Mevcut");

            }
            return new SuccessResult();

        }

        private IResult IsCustomerExist(int userId)
        {
            var result = _userService.GetByUserId(userId);
            if (result == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
