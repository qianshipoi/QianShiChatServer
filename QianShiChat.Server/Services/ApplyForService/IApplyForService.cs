using System.Threading.Tasks;

using QianShiChat.Server.Models.Entities;
using QianShiChat.Server.Models.ViewModels;

namespace QianShiChat.Server.Services.ApplyForService
{
    public interface IApplyForService
    {
        /// <summary>
        /// 添加申请记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddApplyFor(ApplyForInput input);
        /// <summary>
        /// 处理申请记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task HandleApplyFor(int id, ApplyStatus status);

    }
}
