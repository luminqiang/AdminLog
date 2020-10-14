using AutoMapper;
using CT.TcyAppAdmLog.Framework.Mapper;

namespace CT.TcyAppAdmLog.Framework.AutoMapperAdapter
{
    public class MapperAdapter : IMapProvider
    {
        private readonly IMapper _mapper;

        public MapperAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination MapTo<TDestination>(object source)
        {
            if (source == null)
            {
                return default(TDestination);
            }
            return _mapper.Map<TDestination>(source);
        }
    }
}