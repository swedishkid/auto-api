using System;

namespace AutoApi.Sample.Models
{
    [Entity]
    public interface IItem
    {
        Guid Id { get; }
        
        string Title { get; set; }
        
        string Description { get; set; }
    }
}