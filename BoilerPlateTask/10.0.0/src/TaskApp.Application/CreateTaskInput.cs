using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp
{
    [AutoMapTo(typeof(TaskModel))]
    public class CreateTaskInput
    {
        [Required]
        [StringLength(TaskModel.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(TaskModel.MaxDescriptionLength)]
        public string Description { get; set; }

        public Guid? AssignedPersonId { get; set; }
    }
}
