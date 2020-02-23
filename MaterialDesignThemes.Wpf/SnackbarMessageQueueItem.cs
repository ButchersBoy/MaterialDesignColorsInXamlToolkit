﻿using System;
using System.Collections.Generic;

namespace MaterialDesignThemes.Wpf
{
    internal class SnackbarMessageQueueItem
    {
        public SnackbarMessageQueueItem(object content, TimeSpan duration, object actionContent = null, Action<object> actionHandler = null, object actionArgument = null, 
                                        bool isPromoted = false, bool alwaysShow = false)
        {
            Content = content;
            Duration = duration;
            ActionContent = actionContent;
            ActionHandler = actionHandler;
            ActionArgument = actionArgument;
            IsPromoted = isPromoted;
            AlwaysShow = alwaysShow;
        }

        /// <summary>
        /// The content to be displayed
        /// </summary>
        public object Content { get; }

        /// <summary>
        /// Message show duration.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// The content for the action button on the snackbar
        /// </summary>
        public object ActionContent { get; }

        /// <summary>
        /// Handler to be invoked when the action button is clicked
        /// </summary>
        public Action<object> ActionHandler { get; }

        /// <summary>
        /// The argument to pass to the <see cref="ActionHandler"/> delegate.
        /// </summary>
        public object ActionArgument { get; }

        /// <summary>
        /// Promote the message, pushing it in front of any message that is not promoted.
        /// </summary>
        public bool IsPromoted { get; }

        /// <summary>
        /// Always show this message, even if it's a duplicate
        /// </summary>
        public bool AlwaysShow { get; }

        /// <summary>
        /// Last time this message was shown
        /// </summary>
        public DateTime LastShownAt { get; set; }

        public bool MessageExpired()
        {
            return LastShownAt <= DateTime.Now.Subtract(Duration);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SnackbarMessageQueueItem message))
                return false;

            return EqualityComparer<object>.Default.Equals(Content, message.Content)
                   && EqualityComparer<object>.Default.Equals(ActionContent, message.ActionContent);
        }

        public override int GetHashCode()
        {
            return (Content, ActionContent).GetHashCode();
        }
    }
}
