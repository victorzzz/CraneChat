using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.CoreLibrary
{
    public class BaseDisposable : IDisposable
    {
        #region Disposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseDisposable()
        {
            Dispose(false);
        }

        public bool Disposed 
        {
            get
            {
                return m_Disposed;
            }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    SafeManagedResourcesDisposing();
                }

                SafeUnmanagedResourcesDisposing();

                m_Disposed = true;
            }
        }

        protected virtual void SafeManagedResourcesDisposing()
        {
        }

        protected virtual void SafeUnmanagedResourcesDisposing()
        {
        }

        private bool m_Disposed = false;
    }
}
