using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Member service
    /// </summary>
    public class ExecutiveBoardService : IExecutiveBoardService
    {
        private readonly IRepository<ExecutiveBoardMember> _memberRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_memberRepository">Member repository</param>        
        public ExecutiveBoardService(IRepository<ExecutiveBoardMember> memberRepository)
        {
            this._memberRepository = memberRepository;
        }
        
        /// <summary>
        /// Delete member
        /// </summary>
        /// <param name="member">ExecutiveBoardMember</param>
        public virtual void DeleteMember(ExecutiveBoardMember member)
        {
            if (member == null)
                throw new ArgumentNullException("member");                        
            _memberRepository.Delete(member);            
        }
        
        /// <summary>
        /// Get all members
        /// </summary>                        
        /// <returns>ExecutiveBoardMember</returns>
        public virtual List<ExecutiveBoardMember> GetAllMembers()
        {
            var query = _memberRepository.Table;
            query = query.OrderByDescending(c => c.AddedDate);            
                        
            //paging
            return new List<ExecutiveBoardMember>(query.ToList());
        }
                
        /// <summary>
        /// Get member
        /// </summary>
        /// <param name="memberId">Member identifier</param>
        /// <returns>ExecutiveBoardMember</returns>
        public virtual ExecutiveBoardMember GetMemberById(int memberId)
        {
            if (memberId == 0)
                return null;
            return _memberRepository.GetById(memberId);            
        }

        /// <summary>
        /// Insert member
        /// </summary>
        /// <param name="member">ExecutiveBoardMember</param>
        public virtual void InsertMember(ExecutiveBoardMember member)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            _memberRepository.Insert(member);
        }

        /// <summary>
        /// Update member
        /// </summary>
        /// <param name="member">ExecutiveBoardMember</param>
        public virtual void UpdateMember(ExecutiveBoardMember member)
        {
            if (member == null)
                throw new ArgumentNullException("member");            
            _memberRepository.Update(member);
        }
    }
}
