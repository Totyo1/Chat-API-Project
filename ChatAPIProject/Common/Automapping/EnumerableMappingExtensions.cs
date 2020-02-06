using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Common.Automapping
{
    public static class EnumerableMappingExtensions
    {
        public static IEnumerable<TDestination> To<TDestination>(this IEnumerable source, IMapper mapper)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var src in source)
            {
                yield return mapper.Map<TDestination>(src);
            }
        }
    }
}