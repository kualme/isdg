using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Sent email service interface
    /// </summary>
    public interface ISentEmailService
    {
        /// <summary>
        /// Get all sent emails
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>SentEmail</returns>
        IPagedList<SentEmail> GetAllEmails(int pageIndex = 0, int pageSize = int.MaxValue);
        
        /// <summary>
        /// Insert sent email
        /// </summary>
        /// <param name="email">sent email</param>
        void InsertEmail(SentEmail email);
    }
}
