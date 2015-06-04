using AutoMapper;
using Disty.Common.Contract.Distributions;

namespace Disty.Model.MySql
{
    public class MySqlMapProfile : Profile
    {
        public override string ProfileName
        {
            get { return GetType().Name; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<DistributionDept, Dept>();
            Mapper.CreateMap<Dept, DistributionDept>();

            Mapper.CreateMap<DistributionList, List>()
                .ForMember(i => i.Dept, opt => opt.Ignore());
            Mapper.CreateMap<List, DistributionList>()
                .ForMember(d => d.Dept, opt => opt.Ignore());

            Mapper.CreateMap<Email, EmailAddress>();
            Mapper.CreateMap<EmailAddress, Email>();
        }
    }
}