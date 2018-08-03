using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Helpers
{
    public class OperationResult
    {
        private bool _success;
        /// <summary>
        /// Reflects whether the operation was successful or not.
        /// </summary>
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        private List<string> _messageList;
        /// <summary>
        /// Provides a list of messages that can be informational or for debug purposes.
        /// </summary>
        public List<string> MessageList
        {
            get { return _messageList; }
            set { _messageList = value; }
        }

        /// <summary>
        /// Creates a new instance of the OperationResult class, Success is set to true by default.
        /// </summary>
        public OperationResult()
        {
            _success = true;
            _messageList = new List<string>();
        }

        /// <summary>
        /// Adds a string to the message list collection.
        /// </summary>
        /// <param name="message">String of the message to be added.</param>
        public void AddMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _messageList.Add(message);
        }

        /// <summary>
        /// Adds a string collection to the message list.
        /// </summary>
        /// <param name="messages">List of strings to be added to the message list collection.</param>
        public void AddMessageList(List<string> messages)
        {
            _messageList.AddRange(messages);
        }

        /// <summary>
        /// Adds a message with optional debug details.
        /// </summary>
        /// <param name="message">Message to be added to the list.</param>
        /// <param name="callingMethod">Method where the context of the message applies.</param>
        /// <param name="callingClass">Class where the context of the message applies.</param>
        public void AddDebugMessage(string message, [CallerMemberName]string callingMethod = "", [CallerFilePath]string callingClass = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                _messageList.Add($"[{callingClass.Split('\\').Last()}]::{callingMethod} - {message}");
            }
        }

        /// <summary>
        /// Adds the string to the message list and sets the success property to false.
        /// </summary>
        /// <param name="message">Message to be added to the list.</param>
        public void AddFailureMessage(string message)
        {
            AddMessage(message);
            _success = false;
        }

        /// <summary>
        /// Converts the message list to a single string.
        /// </summary>
        /// <returns>String representation of each item, separated by newline, in the message list collection.</returns>
        public string MessageListToString()
        {
            if (_messageList.Count == 0) return "";

            string output = "";
            string last = _messageList.Last();

            foreach (string msg in _messageList)
            {
                if (msg == last)
                {
                    output += $"{msg}";
                }
                else
                {
                    output += $"{msg}{Environment.NewLine}";
                }
            }

            return output;
        }
    }
}
