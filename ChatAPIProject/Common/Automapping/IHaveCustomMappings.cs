using AutoMapper;

namespace ChatAPIProject.Common.Automapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
