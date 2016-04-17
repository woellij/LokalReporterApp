using System;

using LokalReporter.Common;

namespace LokalReporter.App.FormsApp.ViewModels.Messages
{
    public class FeedSubscribedChangedMessage
    {

        public bool IsSubscribed { get; set; }
        public FilterPreset Preset { get; set; }

        public FeedSubscribedChangedMessage(bool isSubscribed, FilterPreset preset)
        {
            this.IsSubscribed = isSubscribed;
            this.Preset = preset;
            this.Preset.Title = preset.ExtendedTitle ?? preset.Title;
        }

    }
}