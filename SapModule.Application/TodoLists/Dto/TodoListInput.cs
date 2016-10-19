using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace SapModule.Application.TodoLists.Dto
{
    public class TodoListInput : EntityDto
    {
        public TodoListInput()
        {
            AllowTodoSelection = false;
        }
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Observations { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public string TaskConfig { get; set; }
        public bool AllowTodoSelection { get; set; }
    }
}
