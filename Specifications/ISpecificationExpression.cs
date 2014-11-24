using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Specifications
{
    public interface ISpecificationExpression<T>:ISpecification<T>
    {
        /// <summary>
        /// Allows multiple specifications to be joined together
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecificationExpression<T> And(ISpecification<T> specification);
        
    
        /// <summary>
        /// Checks whether the specification 
        /// is more general than a given specification.
        /// </summary>
        /// <param name="specification">
        /// The specification to test
        /// </param>
        /// <returns>
        /// Whether the current specification is a generalisation of the supplied specification.
        /// </returns>
        bool IsGeneralizationOf(ISpecificationExpression<T> specification);

        /// <summary>
        /// Checks whether the specification is 
        /// more specific than a given specification.
        /// </summary>
        /// <param name="specification">
        /// The specification to test
        /// </param>
        /// <returns>
        /// Whether the current specification is a special case version of the supplied specification.
        /// </returns>
        bool IsSpecialCaseOf(ISpecificationExpression<T> specification);

        /// <summary>
        /// Allows multiple specifications to be joined together, the specification must return false
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecificationExpression<T> Not(ISpecificationExpression<T> specification);

        /// <summary>
        /// Allows multiple specifications to be joined together, the specification can return true
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecificationExpression<T> Or(ISpecificationExpression<T> specification);

        /// <summary>
        /// Returns the a specification representing 
        /// the criteria that are not met by the candidate object.
        /// </summary>
        /// <param name="item">
        /// The item to test.
        /// </param>
        /// <returns>
        /// Whether the specification is unsatisfied by the specified item.
        /// </returns>
        ISpecificationExpression<T> RemainderUnsatisfiedBy(T item);
    }
}
    