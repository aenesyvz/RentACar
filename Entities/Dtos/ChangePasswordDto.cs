using Core.Entities;

namespace Entities.Dtos
{
    public class ChangePasswordDto : IDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
