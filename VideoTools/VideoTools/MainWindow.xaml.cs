// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="(none)">
//   Copyright © 2020 Etienne Sainton.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// <author>Etienne Sainton</author>
// -----------------------------------------------------------------------

namespace VideoTools
{
    using System.Windows;
    using VideoTools.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The modifier used in this example. It's declared here as it will be used by multiple UserControl embedded in this window.
        /// </summary>
        private MediaModifier modifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            // This is temporary and should be dynamic.
            this.modifier = new MediaModifier();
            this.modifier.Name = "test";
            this.modifier.InputMedia(@"D:\OBSMovies\2020-05-14 17-37-07.mp4");
            this.modifier.OutputMedia(@"D:\OBSMovies\testCrop.mp4");
            this.modifier.AddFilter("test");

            // Gives access to the usecontrols.
            this.DataContext = this.modifier;
        }
    }
}
