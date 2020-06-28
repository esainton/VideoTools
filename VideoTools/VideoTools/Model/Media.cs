// -----------------------------------------------------------------------
// <copyright file="Media.cs" company="(none)">
//   Copyright © 2020 Etienne Sainton.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// <author>Etienne Sainton</author>
// -----------------------------------------------------------------------

namespace VideoTools.Model
{
    /// <summary>
    /// This class represents Media from ffmpeg point of view, they are usually used as input or output for the modifications applied.
    /// </summary>
    internal class Media
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        /// <param name="inputMediaAddress">The address of the media on the hard drive.</param>
        public Media(string inputMediaAddress)
        {
            this.FileAddress = inputMediaAddress;
        }

        /// <summary>
        /// Gets or sets the address of the media file.
        /// </summary>
        public string FileAddress { get; set; }
    }
}
