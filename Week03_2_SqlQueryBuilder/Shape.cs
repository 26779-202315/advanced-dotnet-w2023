using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week03_2_SqlQueryBuilder
{
    public abstract class Shape : IShape
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid OwnerId { get; set; }

    }
}
