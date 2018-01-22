using AutoMapper;

namespace Orders.Infrastructure.Services
{
    public abstract class Service
    {
        protected readonly IMapper _mapper;
        protected Service(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}