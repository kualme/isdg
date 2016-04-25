using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Meeting service interface
    /// </summary>
    public interface IMeetingService
    {
        /// <summary>
        /// Delete meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        void DeleteMeeting(Meeting meeting);

        /// <summary>
        /// Get all meetings
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="type">Meeting type</param>
        /// <param name="isIsdg">Is Isdg meeting</param>
        /// <returns>News</returns>
        IPagedList<Meeting> GetAllMeetings(int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, MeetingType type = MeetingType.Unknown, bool isIsdg = true);
                
        /// <summary>
        /// Get meeting
        /// </summary>
        /// <param name="meetingId">Meeting identifier</param>
        /// <returns>Meeting</returns>
        Meeting GetMeetingById(int meetingId);

        /// <summary>
        /// Insert meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        void InsertMeeting(Meeting meeting);

        /// <summary>
        /// Update meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        void UpdateMeeting(Meeting meeting);                
    }
}
