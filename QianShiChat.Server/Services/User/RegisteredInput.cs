using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;

using QianShiChat.Server.Extensions;

namespace QianShiChat.Server.Services.User
{
    public class RegisteredInput : IValidatableObject
    {

        [Required, MaxLength(32)]
        public string UserName { get; set; }

        [Required, MaxLength(64)]
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }

        private string[] _allowExts = new string[] { ".jpg", ".jpeg", ".bmp", ".png" };

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Avatar != null)
            {
                if (Avatar.Length > 1024 * 1024) yield return new ValidationResult("头像大小限制为1M", new[] { nameof(Avatar) });
                if (!_allowExts.Any(x => x.Equals(Path.GetExtension(Avatar.FileName))))
                {
                    yield return new ValidationResult($"头像类型限制为[{_allowExts.Join()}]", new[] { nameof(Avatar) });
                }
            }
        }
    }
}
