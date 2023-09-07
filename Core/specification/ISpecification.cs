using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get;}
        List<Expression<Func<T,Object>>> Includes {get;}
    }
}