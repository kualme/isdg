using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Award service
    /// </summary>
    public class AwardService : IAwardService
    {
        private readonly IRepository<Award> _awardRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_awardRepository">Award repository</param>        
        public AwardService(IRepository<Award> awardRepository)
        {
            this._awardRepository = awardRepository;
        }
        
        /// <summary>
        /// Delete award
        /// </summary>
        /// <param name="award">Award</param>
        public virtual void DeleteAward(Award award)
        {
            if (award == null)
                throw new ArgumentNullException("award");                        
            _awardRepository.Delete(award);            
        }
        
        /// <summary>
        /// Get all award
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>Award</returns>
        public virtual IPagedList<Award> GetAllAwards(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _awardRepository.Table;
            query = query.OrderByDescending(c => c.AddedDate);            
                        
            //paging
            return new PagedList<Award>(query.ToList(), pageIndex, pageSize);
        }
                
        /// <summary>
        /// Get award
        /// </summary>
        /// <param name="awardId">Award identifier</param>
        /// <returns>Award</returns>
        public virtual Award GetAwardById(int awardId)
        {
            if (awardId == 0)
                return null;
            return _awardRepository.GetById(awardId);            
        }

        /// <summary>
        /// Insert award
        /// </summary>
        /// <param name="award">Award</param>
        public virtual void InsertAward(Award award)
        {
            if (award == null)
                throw new ArgumentNullException("award");
            _awardRepository.Insert(award);
        }

        /// <summary>
        /// Update award
        /// </summary>
        /// <param name="award">Award</param>
        public virtual void UpdateAward(Award award)
        {
            if (award == null)
                throw new ArgumentNullException("award");            
            _awardRepository.Update(award);
        }
    }
}
