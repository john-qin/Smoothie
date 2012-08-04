using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoothie.Services
{
    public class MappingService : IMappingService
    {
        public TDest Map<TSrc, TDest>(TSrc source) where TDest : class
        {
            return AutoMapper.Mapper.Map<TSrc, TDest>(source);
        }
    }
}
