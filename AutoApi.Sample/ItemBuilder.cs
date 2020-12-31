using System;

namespace AutoApi.Sample
{
    public class ItemBuilder
    {
        private readonly Item _item;
        
        public ItemBuilder()
        {
            _item = new Item();
        }

        public ItemBuilder WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            _item.Title = title;
            return this;
        }
        
        public ItemBuilder WithDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            _item.Description = description;
            return this;
        }

        public Item Build() => _item;
    }
}