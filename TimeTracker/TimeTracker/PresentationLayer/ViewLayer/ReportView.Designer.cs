/*
 * Created by SharpDevelop.
 * User: Paul
 * Date: 2/11/2012
 * Time: 4:14 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TimeTracker.PresentationLayer.ViewLayer
{
	partial class ReportView
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.reportGrid = new System.Windows.Forms.DataGridView();
			this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.reportGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// reportGrid
			// 
			this.reportGrid.AllowUserToAddRows = false;
			this.reportGrid.AllowUserToDeleteRows = false;
			this.reportGrid.AutoGenerateColumns = false;
			this.reportGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.reportGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Date,
									this.Duration,
									this.Activity,
									this.Notes});
			this.reportGrid.DataSource = this.gridBindingSource;
			this.reportGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.reportGrid.Location = new System.Drawing.Point(0, 0);
			this.reportGrid.Name = "reportGrid";
			this.reportGrid.ReadOnly = true;
			this.reportGrid.RowTemplate.Height = 24;
			this.reportGrid.Size = new System.Drawing.Size(282, 255);
			this.reportGrid.TabIndex = 0;
			// 
			// Date
			// 
			this.Date.DataPropertyName = "Date";
			dataGridViewCellStyle1.Format = "d";
			dataGridViewCellStyle1.NullValue = null;
			this.Date.DefaultCellStyle = dataGridViewCellStyle1;
			this.Date.HeaderText = "Date";
			this.Date.Name = "Date";
			this.Date.ReadOnly = true;
			// 
			// Duration
			// 
			this.Duration.DataPropertyName = "Duration";
			this.Duration.HeaderText = "Duration";
			this.Duration.Name = "Duration";
			this.Duration.ReadOnly = true;
			// 
			// Activity
			// 
			this.Activity.DataPropertyName = "Activity";
			this.Activity.HeaderText = "Activity";
			this.Activity.Name = "Activity";
			this.Activity.ReadOnly = true;
			// 
			// Notes
			// 
			this.Notes.DataPropertyName = "Notes";
			this.Notes.HeaderText = "Notes";
			this.Notes.Name = "Notes";
			this.Notes.ReadOnly = true;
			// 
			// ReportView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 255);
			this.Controls.Add(this.reportGrid);
			this.Name = "ReportView";
			this.Text = "ReportView";
			((System.ComponentModel.ISupportInitialize)(this.gridBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.reportGrid)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
		private System.Windows.Forms.DataGridViewTextBoxColumn Date;
		private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
		private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
		private System.Windows.Forms.DataGridView reportGrid;
		private System.Windows.Forms.BindingSource gridBindingSource;
	}
}
