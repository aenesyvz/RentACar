using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
        IResult ChangeUserPassword(ChangePasswordDto changePasswordDto);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByUserId(int userId);
        IDataResult<List<OperationClaim>> GetUserClaims(User user);
        IDataResult<List<OperationClaim>> GetClaimsById(int userId);
        IDataResult<int> GetUserFindeks(int userId);
        IResult AddFindeks(int userId, int findeks);
    }
}
