﻿namespace IcoBet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        public Category()
        {
            this.Events = new HashSet<Event>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
