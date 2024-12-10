using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Xunit;
using System.Linq;
using Abp.Runtime.Validation;

namespace TaskApp.Tests.Tasks
{
    public class TaskAppService_Tests : TaskAppTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            _taskAppService = Resolve<ITaskAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Tasks()
        {
            //Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput());

            //Assert
            output.Count.ShouldBe(2);
            output.Count(t => t.AssignedPersonName != null).ShouldBe(1);
        }

        [Fact]
        public async Task Should_Get_Filtered_Tasks()
        {
            //Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput { State = TaskState.Open });

            //Assert
            output.ShouldAllBe(t => t.State == TaskState.Open);
        }
    }
}
