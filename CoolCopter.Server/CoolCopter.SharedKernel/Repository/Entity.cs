using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CoolCopter.SharedKernel.Repository
{

    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public abstract class Entity<TId> : BaseEntity
    {
        [Key]
        public virtual TId Id { get; set; }

        // EF requires an empty constructor
        protected Entity()
        {
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class EntityWithNoId : BaseEntity
    {
        // EF requires an empty constructor
        protected EntityWithNoId()
        {
        }
    }
}