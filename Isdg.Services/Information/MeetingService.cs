using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Meeting service
    /// </summary>
    public class MeetingService : IMeetingService
    {
        private readonly IRepository<Meeting> _meetingRepository;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_meetingRepository">Meeting repository</param>        
        public MeetingService(IRepository<Meeting> meetingRepository)
        {
            this._meetingRepository = meetingRepository;
        }
        
        /// <summary>
        /// Delete meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        public virtual void DeleteMeeting(Meeting meeting)
        {
            if (meeting == null)
                throw new ArgumentNullException("meeting");
            _meetingRepository.Delete(meeting);            
        }
        
        /// <summary>
        /// Get all meetings
        /// </summary>        
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>        
        /// <param name="type">Meeting type</param>        
        /// <param name="isIsdg">Is ISDG meeting</param>        
        /// <returns>Meeting</returns>
        public virtual IPagedList<Meeting> GetAllMeetings(int pageIndex = 0, int pageSize = int.MaxValue, MeetingType type = MeetingType.Unknown, bool isIsdg = true)
        {
            var query = _meetingRepository.Table;
            query = query.OrderByDescending(c => c.ModifiedDate);
            
            query = query.Where(c => c.MeetingType == type);            

            query = query.Where(c => c.IsIsdgMeeting == isIsdg);
            
            return new PagedList<Meeting>(query.ToList(), pageIndex, pageSize);
        }
                
        /// <summary>
        /// Get meeting
        /// </summary>
        /// <param name="meetingId">Meeting identifier</param>
        /// <returns>Meeting</returns>
        public virtual Meeting GetMeetingById(int meetingId)
        {
            if (meetingId == 0)
                return null;
            return _meetingRepository.GetById(meetingId);
        }

        /// <summary>
        /// Insert meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        public virtual void InsertMeeting(Meeting meeting)
        {
            if (meeting == null)
                throw new ArgumentNullException("meeting");
            _meetingRepository.Insert(meeting);
        }

        /// <summary>
        /// Update meeting
        /// </summary>
        /// <param name="meeting">Meeting</param>
        public virtual void UpdateMeeting(Meeting meeting)
        {
            if (meeting == null)
                throw new ArgumentNullException("meeting");
            _meetingRepository.Update(meeting);
        }
    }
}
