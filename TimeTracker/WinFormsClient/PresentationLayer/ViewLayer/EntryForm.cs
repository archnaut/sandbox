using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TimeTracking.Properties;

namespace TimeTracking.PresentationLayer.ViewLayer
{
    public partial class EntryForm : Form, IEntryView
    {
        public EntryForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            Icon = Resources.StopWatchIcon;

            activityTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            activityTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        public string Activity
        {
            get { return activityTextBox.Text.Trim(); }
        }

        public string Note
        {
            get { return noteTextBox.Text.Trim(); }
        }

        public event EventHandler DurationTextChanged = delegate { };

        public string Duration
        {
            get { return durationTextBox.Text; }
            set
            {
                durationTextBox.Text = value;
                durationTextBox.SelectionStart = value.Length;
            }
        }

        public void Show(string[] recentActivities)
        {
            activityTextBox.AutoCompleteCustomSource = CreateAutoCompleteCollection(recentActivities);
            Show();
            durationTextBox.Focus();
        }

        public void DurationSetFocus()
        {
            durationTextBox.Focus();
            durationTextBox.SelectionStart = 0;
            durationTextBox.SelectionLength = durationTextBox.Text.Length;
        }

        public void ActivitySetFocus()
        {
            activityTextBox.SelectionStart = 0;
            activityTextBox.SelectionLength = activityTextBox.Text.Length;
        }

        public void Clear()
        {
            durationTextBox.Clear();
            activityTextBox.Clear();
            noteTextBox.Clear();
        }

        public void SetRecentActivities(IEnumerable<string> recentActivities)
        {
            activityTextBox.AutoCompleteCustomSource = CreateAutoCompleteCollection(recentActivities.ToArray());
        }

        public void SetLastActivity(string lastEntry)
        {
            lastActivityValueLabel.Text = lastEntry;
        }

        private void durationTextBox_TextChanged(object sender, EventArgs e)
        {
            DurationTextChanged(this, EventArgs.Empty);
        }

        private static AutoCompleteStringCollection CreateAutoCompleteCollection(string[] taskHistory)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection.AddRange(taskHistory);
            return collection;
        }
    }
}