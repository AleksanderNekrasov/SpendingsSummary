using AutoMapper;
using ImportedReports.Model;
using SpendingSummary.Common.Models;

namespace ImportedReports.Application.Mapper
{
    public class TransactionModelProfile : Profile
    {
        public TransactionModelProfile()
        {
            CreateMap<TransactionModel, TransactionDto>();
        }
    }
}
