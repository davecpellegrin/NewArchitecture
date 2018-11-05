using SandboxCustomers.Api.Messages;
using SandboxCustomers.Api.Models;

namespace SandboxCustomers.Api
{
    public partial class Startup
    {
        private void Configure_AutoMapper()
        {
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Customer, CustomerItem>().ReverseMap();
                mapper.CreateMap<Customer, CustomerItemAdd>().ReverseMap();
                mapper.CreateMap<Customer, CustomerItemUpdate>().ReverseMap();
            });
        }
    }
}
