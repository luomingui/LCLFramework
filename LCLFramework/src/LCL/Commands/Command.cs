using System;

namespace LCL.Commands
{
    /// <summary>
    /// Represents the base class of the commands.
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <c>Command</c> class.
        /// </summary>
        public Command()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <c>Command</c> class.
        /// </summary>
        /// <param name="id">The identifier which identifies a single command instance.</param>
        public Command(Guid id)
        {
            this.ID = id;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the hash code for current command object.
        /// </summary>
        /// <returns>The calculated hash code for the current command object.</returns>
        public override int GetHashCode()
        {
            return Utility.GetHashCode(this.ID.GetHashCode());
        }
        /// <summary>
        /// Returns a <see cref="System.Boolean"/> value indicating whether this instance is equal to a specified
        /// object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>True if obj is an instance of the <see cref="Apworks.Commands.ICommand"/> type and equals the value of this
        /// instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            Command other = obj as Command;
            if ((object)other == (object)null)
                return false;
            return this.ID == other.ID;
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// Gets or sets the identifier of the command instance.
        /// </summary>
        public virtual Guid ID
        {
            get;
            set;
        }

        #endregion

    }
}
