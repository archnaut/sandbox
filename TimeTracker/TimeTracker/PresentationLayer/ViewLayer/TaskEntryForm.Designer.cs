namespace TimeTracker.PresentationLayer.ViewLayer
{
    partial class TaskEntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hoursLabel = new System.Windows.Forms.Label();
            this.durationTextBox = new System.Windows.Forms.TextBox();
            this.taskLabel = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.noteLabel = new System.Windows.Forms.Label();
            this.activityTextBox = new System.Windows.Forms.TextBox();
            this.lastEntryLabel = new System.Windows.Forms.Label();
            this.lastActivityValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoursLabel.Location = new System.Drawing.Point(12, 15);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(40, 13);
            this.hoursLabel.TabIndex = 3;
            this.hoursLabel.Text = "Hours";
            // 
            // durationTextBox
            // 
            this.durationTextBox.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationTextBox.Location = new System.Drawing.Point(58, 12);
            this.durationTextBox.Name = "durationTextBox";
            this.durationTextBox.Size = new System.Drawing.Size(50, 20);
            this.durationTextBox.TabIndex = 0;
            this.durationTextBox.TextChanged += new System.EventHandler(this.durationTextBox_TextChanged);
            // 
            // taskLabel
            // 
            this.taskLabel.AutoSize = true;
            this.taskLabel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskLabel.Location = new System.Drawing.Point(3, 41);
            this.taskLabel.Name = "taskLabel";
            this.taskLabel.Size = new System.Drawing.Size(49, 13);
            this.taskLabel.TabIndex = 4;
            this.taskLabel.Text = "Activity";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteTextBox.Location = new System.Drawing.Point(58, 64);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(235, 20);
            this.noteTextBox.TabIndex = 2;
            // 
            // noteLabel
            // 
            this.noteLabel.AutoSize = true;
            this.noteLabel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLabel.Location = new System.Drawing.Point(19, 67);
            this.noteLabel.Name = "noteLabel";
            this.noteLabel.Size = new System.Drawing.Size(33, 13);
            this.noteLabel.TabIndex = 5;
            this.noteLabel.Text = "Note";
            // 
            // activityTextBox
            // 
            this.activityTextBox.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activityTextBox.Location = new System.Drawing.Point(58, 38);
            this.activityTextBox.Name = "activityTextBox";
            this.activityTextBox.Size = new System.Drawing.Size(235, 20);
            this.activityTextBox.TabIndex = 1;
            // 
            // lastEntryLabel
            // 
            this.lastEntryLabel.AutoSize = true;
            this.lastEntryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastEntryLabel.Location = new System.Drawing.Point(114, 15);
            this.lastEntryLabel.Name = "lastEntryLabel";
            this.lastEntryLabel.Size = new System.Drawing.Size(64, 13);
            this.lastEntryLabel.TabIndex = 6;
            this.lastEntryLabel.Text = "Last Entry";
            // 
            // lastActivityValueLabel
            // 
            this.lastActivityValueLabel.AutoSize = true;
            this.lastActivityValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastActivityValueLabel.Location = new System.Drawing.Point(174, 15);
            this.lastActivityValueLabel.Name = "lastActivityValueLabel";
            this.lastActivityValueLabel.Size = new System.Drawing.Size(60, 13);
            this.lastActivityValueLabel.TabIndex = 7;
            this.lastActivityValueLabel.Text = "Unknown";
            // 
            // TaskEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 99);
            this.Controls.Add(this.lastActivityValueLabel);
            this.Controls.Add(this.lastEntryLabel);
            this.Controls.Add(this.activityTextBox);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.taskLabel);
            this.Controls.Add(this.durationTextBox);
            this.Controls.Add(this.hoursLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "TaskEntryForm";
            this.ShowInTaskbar = false;
            this.Text = "Time Tracker";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hoursLabel;
        private System.Windows.Forms.TextBox durationTextBox;
        private System.Windows.Forms.Label taskLabel;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.TextBox activityTextBox;
        private System.Windows.Forms.Label lastEntryLabel;
        private System.Windows.Forms.Label lastActivityValueLabel;

    }
}