using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QianShiChat.Server.Models.ViewModels
{
    public class ApplyForInput : IValidatableObject
    {
        public int UserId { get; set; }

        public long GroupId { get; set; }

        [MaxLength(512, ErrorMessage = "备注长度限制再512个字符")]
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserId == 0 && GroupId == 0) yield return new ValidationResult("用户编号或群组编号选择一个填写", new[] { nameof(UserId), nameof(GroupId) });
        }
    }
}
