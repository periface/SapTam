using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SapModule.Core.Projects.Entities;
using System;
using System.Collections.Generic;

namespace SapModule.Application.Projects.Dto
{
    public class ProjectTableOutput
    {
        public IEnumerable<ProjectDto> Projects { get; set; }
    }
    [AutoMapFrom(typeof(Project))]
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FinshedPercentage { get; set; }
    }
}
