using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimeTracker.PresentationLayer
{
    public interface IEntryView : IDisposable
    {
        string Duration { get; set; }
        string Activity { get; }
        string Note { get; }
        bool Visible { get; set; }

        event EventHandler DurationTextChanged;
        event KeyEventHandler KeyDown;

        void Show(string[] taskHistory);
        void Hide();
        void DurationSetFocus();
        void ActivitySetFocus();
        void Clear();

        void SetRecentActivities(IEnumerable<string> taskHistory);
        void SetLastActivity(string lastEntry);
    }
}