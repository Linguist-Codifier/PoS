using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Order.Domain.Filters.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TableMapping : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public TableMapping(String name)
        {
            this.Name = name;
        }
    }
}