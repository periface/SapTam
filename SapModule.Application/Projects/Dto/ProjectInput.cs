using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SapModule.Core.Projects.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SapModule.Application.Projects.Dto
{
    [AutoMap(typeof(Project))]
    public class ProjectInput : EntityDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? InitialBudget { get; set; }
    }
}
