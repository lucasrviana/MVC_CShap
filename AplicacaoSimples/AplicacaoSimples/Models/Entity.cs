using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoSimples.Models
{
    public abstract class Entity
    {

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

    }
}
