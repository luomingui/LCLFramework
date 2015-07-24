using System;
namespace LCL
{
    public class AppInitEventArgs : EventArgs
    {
        #region Public Properties
        public IObjectContainer ObjectContainer { get; private set; }
        #endregion
        #region Ctor
        public AppInitEventArgs()
            : this(null)
        {

        }

        public AppInitEventArgs(IObjectContainer objectContainer)
        {
            this.ObjectContainer = objectContainer;
        }
        #endregion
    }
}
