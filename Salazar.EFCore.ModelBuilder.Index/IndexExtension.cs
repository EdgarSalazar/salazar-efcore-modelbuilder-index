using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Salazar.EFCore.ModelBuilder.Index;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.EntityFrameworkCore
{
    public static class IndexExtension
    {
        public static void SetIndexOnEntitiesByAttribute(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                var props = entity.GetProperties().Where(x => x.PropertyInfo.GetCustomAttributes(false).Any(a => a is IndexAttribute))
                    .Select(x => new
                    {
                        x.Name,
                        Opts = x.PropertyInfo.GetCustomAttributes(false).FirstOrDefault(a => a is IndexAttribute) as IndexAttribute
                    })
                    .GroupBy(x => x.Opts.Name)
                    .Select(x => new
                    {
                        Names = x.Select(y => y.Name),
                        x.FirstOrDefault().Opts
                    })
                    .ToList();

                foreach (var prop in props)
                {
                    var name = prop.Opts?.Name ?? $"IX_{entity.DisplayName()}_{string.Join("_", prop.Names)}";

                    var index = modelBuilder.Entity(entity.Name)
                        .HasIndex(prop.Names.ToArray());

                    if (prop.Opts?.Unique == true)
                    {
                        index.IsUnique();
                    }

                    index.HasName(name);
                }
            }
        }
    }
}