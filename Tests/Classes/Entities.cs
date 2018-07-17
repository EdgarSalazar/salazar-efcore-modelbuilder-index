using Salazar.EFCore.ModelBuilder.Index;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Classes
{
    [Table("EntityWithoutIndex")]
    public class EntityWithoutIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("EntityWithImplicitNonUniqueIndex")]
    public class EntityWithImplicitNonUniqueIndex
    {
        [Index("IX_Id_Name")]
        public int Id { get; set; }

        [Index("IX_Id_Name")]
        public string Name { get; set; }
    }

    [Table("EntityWithNonUniqueIndex")]
    public class EntityWithNonUniqueIndex
    {
        [Index("IX_Id_Name", false)]
        public int Id { get; set; }

        [Index("IX_Id_Name", false)]
        public string Name { get; set; }
    }

    [Table("EntityWithUniqueIndex")]
    public class EntityWithUniqueIndex
    {
        public int Id { get; set; }

        [Index("IX_Name", true)]
        public string Name { get; set; }
    }
}