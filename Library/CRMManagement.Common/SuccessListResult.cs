//-----------------------------------------------------------------------
// <copyright file="SuccessListResult.cs" company="Dexoc Solutions">
//     Copyright Dexoc Solutions. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRMManagement.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class success result
    /// </summary>
    /// <typeparam name="T">The Base Model Type.</typeparam>
    public class SuccessListResult<T> : AbstractResult where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessListResult{T}"/> class.
        /// </summary>
        public SuccessListResult()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessListResult{T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public SuccessListResult(ICollection<T> items)
        {
            this.Items = items;
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ICollection<T> Items { get; set; }
    }
}
