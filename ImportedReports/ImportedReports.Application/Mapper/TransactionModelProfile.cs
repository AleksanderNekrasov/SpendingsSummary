using AutoMapper;
using ImportedReports.Model;
using SpendingSummary.Common.Models;

namespace ImportedReports.Application.Mapper
{
    public sealed class TransactionModelProfile : Profile
    {
        public TransactionModelProfile() =>
            CreateMap<TransactionModel, TransactionDto>();
        
    }
}
