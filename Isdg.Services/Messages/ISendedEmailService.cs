using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Sended email service interface
    /// </summary>
    public interface ISendedEmailService
    {
        /// <summary>
        /// Get all sended emails
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <returns>SendedEmail</returns>
        IPagedList<SendedEmail> GetAllEmails(int pageIndex = 0, int pageSize = int.MaxValue);
        
        /// <summary>
        /// Insert sended email
        /// </summary>
        /// <param name="email">sended email</param>
        void InsertEmail(SendedEmail email);
    }
}
