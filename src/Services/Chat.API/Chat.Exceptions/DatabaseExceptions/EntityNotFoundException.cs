using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Exceptions.DatabaseExceptions
{
    public class EntityNotFoundException : Exception 
    {
        public EntityNotFoundException(string id) : base($"Entity with id {id} is not found")
        {

        }

        public EntityNotFoundException(Expression predicate) : base($"Entities by expression {predicate.ToString()} is not found")
        {

        }
    }
}
