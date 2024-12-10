using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;

namespace TaskApp
{
    public class TaskAppService : TaskAppAppServiceBase, ITaskAppService
    {
        private readonly IRepository<TaskModel> _taskRepository;

        public TaskAppService(IRepository<TaskModel> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> Create(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<TaskModel>(input);
            await _taskRepository.InsertAsync(task);
            return task;
        }

        public async Task<List<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t => t.AssignedPerson)
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new List<TaskListDto>(ObjectMapper.Map<List<TaskListDto>>(tasks));
        }
    }
}
