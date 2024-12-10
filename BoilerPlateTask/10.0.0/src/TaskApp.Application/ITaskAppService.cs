using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp
{
    public interface ITaskAppService : IApplicationService
    {
        Task<List<TaskListDto>> GetAll(GetAllTasksInput input);
        Task<TaskModel> Create(CreateTaskInput input);
    }
}
