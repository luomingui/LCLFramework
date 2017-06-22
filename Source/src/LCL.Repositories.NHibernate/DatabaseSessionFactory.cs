using NHibernate;
using NHibernate.Cfg;

namespace LCL.Repositories.NHibernate
{
    /// <summary>
    /// Represents the factory singleton for database session.
    /// </summary>
    internal sealed class DatabaseSessionFactory
    {
        #region Private Fields
        /// <summary>
        /// The session factory instance.
        /// </summary>
        private readonly ISessionFactory sessionFactory = null;
        /// <summary>
        /// The session instance.
        /// </summary>
        private ISession session = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <c>DatabaseSessionFactory</c> class.
        /// </summary>
        internal DatabaseSessionFactory()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }
        /// <summary>
        /// Initializes a new instance of <c>DatabaseSessionFactory</c> class.
        /// </summary>
        /// <param name="nhibernateConfig">The <see cref="Configuration"/> instance used for initializing.</param>
        internal DatabaseSessionFactory(Configuration nhibernateConfig)
        {
            sessionFactory = nhibernateConfig.BuildSessionFactory();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the singleton instance of the session. If the session has not been
        /// initialized or opened, it will return a newly opened session from the session factory.
        /// </summary>
        public ISession Session
        {
            get
            {
                ISession result = session;
                if (result != null && result.IsOpen)
                    return result;
                return OpenSession();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Always opens a new session from the session factory.
        /// </summary>
        /// <returns>The newly opened session.</returns>
        public ISession OpenSession()
        {
            this.session = sessionFactory.OpenSession();
            return this.session;
        }
        #endregion

    }
}
