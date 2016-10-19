using Abp.Application.Services.Dto;

namespace SapModule.Application.Projects.Dto
{
    public class BudgetInputDto : EntityDto
    {
        public decimal Initial { get; set; }
        public int IdProject { get; set; }
    }
}
