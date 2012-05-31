using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data; 
using Microsoft.Practices.EnterpriseLibrary.Data.Sql; 
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    class MSSQLAdapter : BaseDisposable, IRelationalDBAdapter
    {
        public MSSQLAdapter()
        {
            m_DB = EnterpriseLibraryContainer.Current.GetInstance<Database>() as SqlDatabase;
        }

        #region IRelationalDBAdapter implementation

        public bool FollowUser(string requesterName, string userToFollow)
        {
            bool result = false;

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = m_DB.GetStoredProcCommand("cranechat_FollowUser");
            m_DB.AddInParameter(cmd, "UserName", DbType.String, requesterName);
            m_DB.AddInParameter(cmd, "UserNameToFollow", DbType.String, userToFollow);

            // Execute the query and check if one row was updated.
            if (m_DB.ExecuteNonQuery(cmd) == 1)
            {
                result = true;
            }

            return result;
        }

        public bool UnFollowUser(string requesterName, string userToUnFollow)
        {
            bool result = false;

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = m_DB.GetStoredProcCommand("cranechat_UnfollowUser");
            m_DB.AddInParameter(cmd, "UserName", DbType.String, requesterName);
            m_DB.AddInParameter(cmd, "UserNameToUnfollow", DbType.String, userToUnFollow);

            // Execute the query and check if one row was updated.
            if (m_DB.ExecuteNonQuery(cmd) == 1)
            {
                result = true;
            }

            return result;
        }

        public IEnumerable<string> GetFollowers(string userName)
        {
            List<string> result = new List<string>();

            using (IDataReader reader = m_DB.ExecuteReader("cranechat_GetFollowers", new object[] { userName }))
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }

        #endregion

        private SqlDatabase m_DB = null;
    }
}
