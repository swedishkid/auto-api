using System.Collections.Generic;

namespace AutoApi.Sample
{
    public class GetItemsResult
    {
        public List<Item> Items { get; }

        public GetItemsResult(List<Item> items)
        {
            Items = items;
        }
    }
}