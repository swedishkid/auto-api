using System;

namespace AutoApi.Sample.Models
{
    [Entity]
    public interface ICategory
    {
        Guid Id { get; }
        
        string Name { get; }
        
        ICategory Category { get; }
    }
}