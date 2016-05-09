using System;
using System.Collections.Generic;
using System.Linq;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Information
{
    /// <summary>
    /// Text service
    /// </summary>
    public class TextService : ITextService
    {
        private readonly IRepository<Text> textRepository;        
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="textRepository">Text repository</param>        
        public TextService(IRepository<Text> textRepository)
        {
            this.textRepository = textRepository;            
        }

        /// <summary>
        /// Get text by id
        /// </summary>
        /// <param name="textId">textId</param>
        /// <returns></returns>
        public Text GetTextById(int textId)
        {
            if (textId == 0)
                return null;
            return textRepository.GetById(textId);
        }

        /// <summary>
        /// Get text by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Text GetTextByKey(string key)
        {
            if (String.IsNullOrEmpty(key))
                return null;
            return textRepository.Table.FirstOrDefault(x => x.Key == key);            
        }

        /// <summary>
        /// Insert text
        /// </summary>
        /// <param name="text">Text</param>
        public virtual void InsertText(Text text)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            textRepository.Insert(text);
        }

        /// <summary>
        /// Update text
        /// </summary>
        /// <param name="text">Text</param>
        public virtual void UpdateText(Text text)
        {
            if (text == null)
                throw new ArgumentNullException("text");            
            textRepository.Update(text);
        }        
    }
}
