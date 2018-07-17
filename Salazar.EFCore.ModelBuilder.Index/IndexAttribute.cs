using System;

namespace Salazar.EFCore.ModelBuilder.Index
{
    public class IndexAttribute : Attribute
    {
        public IndexAttribute()
        {

        }

        public IndexAttribute(string name) : this()
        {
            Name = name;
        }

        public IndexAttribute(string name, bool unique) : this(name)
        {
            Unique = unique;
        }

        public string Name { get; set; }
        public bool Unique { get; set; }
    }
}
