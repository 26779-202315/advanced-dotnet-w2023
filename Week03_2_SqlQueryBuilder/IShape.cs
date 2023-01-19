using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week03_2_SqlQueryBuilder
{
    public interface IShape : IDatabaseTable
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
