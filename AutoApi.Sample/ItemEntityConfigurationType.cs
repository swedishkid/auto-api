using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoApi.Sample
{
    public class ItemEntityConfigurationType : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", "dbo");
            builder.HasKey(x => x.Id).HasName("PrimaryKey_Items_Id");

            builder.Property("Title")
                .HasColumnName("Title")
                .HasField("title")
                .HasMaxLength(200);

            builder.Property("Description")
                .HasField("description")
                .HasColumnName("Description")
                .HasMaxLength(1000);
        }
    }
}