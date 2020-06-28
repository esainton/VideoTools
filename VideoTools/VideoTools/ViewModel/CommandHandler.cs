// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="(none)">
//   Copyright © 2020 Etienne Sainton.  All Rights Reserved.
//   This source is subject to the MIT license.
//   Please see license.md for more information.
// </copyright>
// <author>Etienne Sainton</author>
// -----------------------------------------------------------------------

namespace VideoTools.ViewModel
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Helps implement faster commands in viewmodels.
    /// </summary>
    public class CommandHandler : ICommand
    {
        private Action action;
        private Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="newAction">Action to be executed by the command.</param>
        /// <param name="newCanExecute">A bolean property to containing current permissions to execute the command.</param>
        public CommandHandler(Action newAction, Func<bool> newCanExecute)
        {
            this.action = newAction;
            this.canExecute = newCanExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forces checking if execute is allowed.
        /// </summary>
        /// <param name="parameter">Parameter to check.</param>
        /// <returns>Confirm if we can execute or not.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute.Invoke();
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The source object ICommand to execute.</param>
        public void Execute(object parameter)
        {
            this.action();
        }
    }
}