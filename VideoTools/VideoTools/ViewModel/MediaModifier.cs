// -----------------------------------------------------------------------
// <copyright file="MediaModifier.cs" company="(none)">
//   Copyright © 2020 Etienne Sainton.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// <author>Etienne Sainton</author>
// -----------------------------------------------------------------------

namespace VideoTools.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows.Input;
    using VideoTools.Model;

    /// <summary>
    /// MediaModifier is the ViewModel in charge of applying a list of modifications from one media to a new media.
    /// </summary>
    internal class MediaModifier : INotifyPropertyChanged
    {
        /// <summary>
        /// Can execute ffmpeg.
        /// </summary>
        private bool canExecute = true;

        /// <summary>
        /// The log from ffmpeg of the execution.
        /// </summary>
        private string log;

        /// <summary>
        /// The command available to run the execiution of the MediaModifier form a view.
        /// </summary>
        private ICommand runModificationCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModifier"/> class, will instanciate the Model <see cref="MediaModification"/> associated.
        /// </summary>
        public MediaModifier()
        {
            this.MediaModification = new MediaModification();
        }

        /// <summary>
        /// Occurs when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets a value indicating whether the command can be executed or not.
        /// </summary>
        public bool CanExecute => this.canExecute;

        /// <summary>
        /// Gets or sets last logs from the ffmpeg execution.
        /// If you set them this will add a new line, and remove too old lines.
        /// </summary>
        public string ExecutionLog
        {
            get => this.log;
            set
            {
                this.log += "\n" + value;
                this.NotifyPropertyChanged(nameof(this.ExecutionLog));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="MediaModification" /> used by this modifier.
        /// </summary>
        public MediaModification MediaModification { get; set; }

        /// <summary>
        /// Gets or sets the name of the modification.
        /// </summary>
        public string Name
        {
            get => this.MediaModification.Name;
            set => this.MediaModification.Name = value;
        }

        /// <summary>
        /// Gets the command to run ffmpeg with this set of actions.
        /// </summary>
        public ICommand RunModificationCommand => this.runModificationCommand ?? (this.runModificationCommand = new CommandHandler(() => this.RunMediaModifier(), () => this.CanExecute));

        /// <summary>
        /// Adds a filter to the list of modifications applied.
        /// </summary>
        /// <param name="filterName">string: name of the filter.</param>
        /// <returns>Filter if found from the name, null if the name doesn't match any filter.</returns>
        public Filter AddFilter(string filterName)
        {
            // Pick a filter from the library of filters:
            Filter tmpFilter = new Filter
            {
                Command = "-filter:v \"crop=500:250:50:100\"",
                Name = "Crop",
            };

            this.MediaModification.Filters.Add(tmpFilter);

            return tmpFilter;
        }

        /// <summary>
        /// Executes ffmpeg.
        /// </summary>
        public void FfmpegAction()
        {
            this.canExecute = false;

            // Create process
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            // strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = "ffmpeg";

            // strCommandParameters are parameters to pass to program
            pProcess.StartInfo.Arguments = "-i \"" + this.MediaModification.InputMedia.FileAddress + "\" " + this.MediaModification.Filters[0].Command + " " + this.MediaModification.OutputMedia.FileAddress;

            pProcess.StartInfo.UseShellExecute = false;

            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            // Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;

            pProcess.StartInfo.RedirectStandardError = true;

            pProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { this.ExecutionLog = e.Data; });

            // Start the process
            pProcess.Start();

            pProcess.BeginErrorReadLine();

            // Get program output
            string strOutput = pProcess.StandardOutput.ReadToEnd();

            // Wait for process to finish
            pProcess.WaitForExit();

            this.ExecutionLog = strOutput;

            pProcess.Dispose();

            this.canExecute = true;
        }

        /// <summary>
        /// Sets the media used as a source by ffmpeg, it will create the <see cref="Media"/> from the file address on the hard drive.
        /// </summary>
        /// <param name="inputMediaAddress">string: file address of the source media on the hard drive.</param>
        /// <returns>Returns the media if the address is correct, return null if the address if not correct.</returns>
        public Media InputMedia(string inputMediaAddress)
        {
            if (File.Exists(inputMediaAddress))
            {
                return this.MediaModification.InputMedia = new Media(inputMediaAddress);
            }
            else
            {
                // TODO: add error in logs
                return null;
            }
        }

        /// <summary>
        /// Sets the media used as an output by ffmpeg.
        /// </summary>
        /// <param name="outputMediaAddress">string: file address on the hard drive.</param>
        /// <returns>the Media created.</returns>
        public Media OutputMedia(string outputMediaAddress)
        {
            return this.MediaModification.OutputMedia = new Media(outputMediaAddress);
        }

        /// <summary>
        /// Removes the filter from the list of filters applied.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns>Return the name of the filter if it exists and is in the list of modification, null if else.</returns>
        public Filter RemoveFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts a thread to exectute the ffmpeg command.
        /// </summary>
        public void RunMediaModifier()
        {
            Thread thread = new Thread(this.FfmpegAction);
            thread.Start();
        }

        /// <summary>
        /// Notifies when a propertye changed to allow binding from the views.
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}