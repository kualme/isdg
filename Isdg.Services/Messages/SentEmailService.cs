using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Sended email service
    /// </summary>
    public class SentEmailService : ISentEmailService
    {
        private readonly IRepository<SentEmail> sentEmailRepository;
                
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sendedEmailRepository">Sent email repository</param>        
        public SentEmailService(IRepository<SentEmail> sentEmailRepository)
        {
            this.sentEmailRepository = sentEmailRepository;
        }
                
        /// <summary>
        /// Get all sent emails
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>SentEmail</returns>
        public virtual IPagedList<SentEmail> GetAllEmails(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = sentEmailRepository.Table;
            query = query.OrderByDescending(c => c.AddedDate);            
                        
            //paging
            return new PagedList<SentEmail>(query.ToList(), pageIndex, pageSize);
        }
        
        /// <summary>
        /// Insert sent email
        /// </summary>
        /// <param name="email">email</param>
        public virtual void InsertEmail(SentEmail email)
        {
            if (email == null)
                throw new ArgumentNullException("email");
            sentEmailRepository.Insert(email);
        }
    }
}
