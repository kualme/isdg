using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// ExecutiveBoardMember service interface
    /// </summary>
    public interface IExecutiveBoardService
    {
        /// <summary>
        /// Delete Member
        /// </summary>
        /// <param name="Member">Member</param>
        void DeleteMember(ExecutiveBoardMember member);

        /// <summary>
        /// Get all Members
        /// </summary>                
        /// <returns>ExecutiveBoardMember</returns>
        List<ExecutiveBoardMember> GetAllMembers();
                
        /// <summary>
        /// Get Member
        /// </summary>
        /// <param name="memberId">Member identifier</param>
        /// <returns>ExecutiveBoardMember</returns>
        ExecutiveBoardMember GetMemberById(int memberId);

        /// <summary>
        /// Insert Member
        /// </summary>
        /// <param name="Member">Member</param>
        void InsertMember(ExecutiveBoardMember member);

        /// <summary>
        /// Update Member
        /// </summary>
        /// <param name="Member">Member</param>
        void UpdateMember(ExecutiveBoardMember member);                
    }
}
