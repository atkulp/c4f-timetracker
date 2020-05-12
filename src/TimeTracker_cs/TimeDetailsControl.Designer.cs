namespace TimeTracker
{
    partial class TimeDetailsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showZeroHourCheckBox = new System.Windows.Forms.CheckBox();
            this.computeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.projectsTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showZeroHourCheckBox);
            this.groupBox1.Controls.Add(this.computeLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.updateButton);
            this.groupBox1.Controls.Add(this.startDateTimePicker);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.endDateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 97);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Options";
            // 
            // showZeroHourCheckBox
            // 
            this.showZeroHourCheckBox.AutoSize = true;
            this.showZeroHourCheckBox.Location = new System.Drawing.Point(157, 21);
            this.showZeroHourCheckBox.Name = "showZeroHourCheckBox";
            this.showZeroHourCheckBox.Size = new System.Drawing.Size(105, 17);
            this.showZeroHourCheckBox.TabIndex = 11;
            this.showZeroHourCheckBox.Text = "Show &zero-hours";
            this.showZeroHourCheckBox.UseVisualStyleBackColor = true;
            // 
            // computeLabel
            // 
            this.computeLabel.AutoSize = true;
            this.computeLabel.Location = new System.Drawing.Point(12, 81);
            this.computeLabel.Name = "computeLabel";
            this.computeLabel.Size = new System.Drawing.Size(0, 13);
            this.computeLabel.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(187, 44);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "&Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDateTimePicker.Location = new System.Drawing.Point(53, 21);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(84, 20);
            this.startDateTimePicker.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End";
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateTimePicker.Location = new System.Drawing.Point(53, 47);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(84, 20);
            this.endDateTimePicker.TabIndex = 6;
            // 
            // projectsTreeView
            // 
            this.projectsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectsTreeView.Location = new System.Drawing.Point(0, 104);
            this.projectsTreeView.Name = "projectsTreeView";
            this.projectsTreeView.Size = new System.Drawing.Size(271, 112);
            this.projectsTreeView.TabIndex = 9;
            // 
            // TimeDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.projectsTreeView);
            this.Name = "TimeDetailsControl";
            this.Size = new System.Drawing.Size(271, 216);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.TreeView projectsTreeView;
        private System.Windows.Forms.Label computeLabel;
        private System.Windows.Forms.CheckBox showZeroHourCheckBox;
    }
}
