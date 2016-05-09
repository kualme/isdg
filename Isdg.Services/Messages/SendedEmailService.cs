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
    public class SendedEmailService : ISendedEmailService
    {
        private readonly IRepository<SendedEmail> sendedEmailRepository;
                
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sendedEmailRepository">Sended email repository</param>        
        public SendedEmailService(IRepository<SendedEmail> sendedEmailRepository)
        {
            this.sendedEmailRepository = sendedEmailRepository;
        }
                
        /// <summary>
        /// Get all sended emails
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>SendedEmail</returns>
        public virtual IPagedList<SendedEmail> GetAllEmails(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = sendedEmailRepository.Table;
            query = query.OrderByDescending(c => c.AddedDate);            
                        
            //paging
            return new PagedList<SendedEmail>(query.ToList(), pageIndex, pageSize);
        }
        
        /// <summary>
        /// Insert sended email
        /// </summary>
        /// <param name="email">email</param>
        public virtual void InsertEmail(SendedEmail email)
        {
            if (email == null)
                throw new ArgumentNullException("email");
            sendedEmailRepository.Insert(email);
        }
    }
}
