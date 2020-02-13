using AutoMapper;

using System;

namespace ChatAPIProject.Common.Automapping
{
    public static class ObjectMappingExtensions
    {
        public static  T To<T>(this object origin, IMapper mapper)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            return mapper.Map<T>(origin);
        }

        public static  TDestination To<TSource, TDestination>(this TSource origin, TDestination destination, IMapper mapper)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            return mapper.Map(source: origin, destination: destination);
        }
    }
}