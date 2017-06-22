namespace LCL.Domain.Specifications
{
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T>
    {
        #region Private Fields
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;
        #endregion

        #region Ctor
        public CompositeSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        #endregion

        #region ICompositeSpecification Members
        public ISpecification<T> Left
        {
            get { return this.left; }
        }
        public ISpecification<T> Right
        {
            get { return this.right; }
        }
        #endregion
    }
}
