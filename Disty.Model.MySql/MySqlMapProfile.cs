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

            Mapper.CreateMap<DistributionList, List>();
            Mapper.CreateMap<List, DistributionList>();
        }
    }
}