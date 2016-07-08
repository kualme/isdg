using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Award service interface
    /// </summary>
    public interface IAwardService
    {
        /// <summary>
        /// Delete award
        /// </summary>
        /// <param name="Award">Award</param>
        void DeleteAward(Award award);

        /// <summary>
        /// Get all award
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Award</returns>
        IPagedList<Award> GetAllAwards(int pageIndex = 0, int pageSize = int.MaxValue);
                
        /// <summary>
        /// Get award
        /// </summary>
        /// <param name="awardId">Award identifier</param>
        /// <returns>Award</returns>
        Award GetAwardById(int awardId);

        /// <summary>
        /// Insert award
        /// </summary>
        /// <param name="award">Award</param>
        void InsertAward(Award award);

        /// <summary>
        /// Update award
        /// </summary>
        /// <param name="award">Award</param>
        void UpdateAward(Award award);                
    }
}
