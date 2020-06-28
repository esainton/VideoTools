// -----------------------------------------------------------------------
// <copyright file="MediaModification.cs" company="(none)">
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
    /// The sets of modifications applied by ffmpeg.
    /// </summary>
    internal class MediaModification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModification"/> class.
        /// </summary>
        public MediaModification()
        {
            this.Filters = new List<Filter>();
        }

        /// <summary>
        /// Gets or sets the description of this modification.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the filters applied during this modification.
        /// </summary>
        public List<Filter> Filters { get; set; }

        /// <summary>
        /// Gets or sets the media used as a source by this modification.
        /// </summary>
        public Media InputMedia { get; set; }

        /// <summary>
        /// Gets or sets the name of this modification.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media created by this modification.
        /// </summary>
        public Media OutputMedia { get; set; }
    }
}
