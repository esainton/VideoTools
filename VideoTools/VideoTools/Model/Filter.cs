// -----------------------------------------------------------------------
// <copyright file="Filter.cs" company="(none)">
//   Copyright © 2020 Etienne Sainton.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// <author>Etienne Sainton</author>
// -----------------------------------------------------------------------

namespace VideoTools.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// FFmpeg filter, with the command and the options if needed.
    /// </summary>
    internal class Filter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        public Filter()
        {
            this.Options = new List<string>();
            this.Name = "Undefined";
            this.Command = "Unidefined";
        }

        /// <summary>
        /// Gets or sets the ffmpeg command.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the list of options used for this filter.
        /// </summary>
        public List<string> Options { get; set; }

        /// <summary>
        /// Gets or sets the name of this filter.
        /// </summary>
        public string Name { get; set; }
    }
}
