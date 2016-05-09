using System.Collections.Generic;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Text service interface
    /// </summary>
    public interface ITextService
    {
        /// <summary>
        /// Get text
        /// </summary>
        /// <param name="textId">textId</param>
        /// <returns></returns>
        Text GetTextById(int textId);
     
        /// <summary>
        /// Get text by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        Text GetTextByKey(string key);

        /// <summary>
        /// Insert Text
        /// </summary>
        /// <param name="text">Text</param>
        void InsertText(Text text);

        /// <summary>
        /// Update text
        /// </summary>
        /// <param name="text">text</param>
        void UpdateText(Text text);        
    }
}
