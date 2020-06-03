namespace TimeTracker
{
    partial class ProjectsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsForm));
            this.timeTrackerNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.punchInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.punchOutAtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewTimeDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeButton = new System.Windows.Forms.Button();
            this.timeTrackerDataSet = new TimeTracker.TimeTrackerDataSet();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.timeDetailsControl1 = new TimeTracker.TimeDetailsControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.projectsListControl1 = new TimeTracker.ProjectsListControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.entryIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.projectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeEntriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.locateXmlFilebutton = new System.Windows.Forms.LinkLabel();
            this.notifyIconMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeTrackerDataSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEntriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // timeTrackerNotifyIcon
            // 
            this.timeTrackerNotifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.timeTrackerNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("timeTrackerNotifyIcon.Icon")));
            this.timeTrackerNotifyIcon.Text = "Time Tracker";
            this.timeTrackerNotifyIcon.Visible = true;
            this.timeTrackerNotifyIcon.DoubleClick += new System.EventHandler(this.punchInOutToolStripMenuItem_Click);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeProjectToolStripMenuItem,
            this.punchInOutToolStripMenuItem,
            this.punchOutAtToolStripMenuItem,
            this.toolStripSeparator1,
            this.viewTimeDetailsToolStripMenuItem,
            this.toolStripMenuExport,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(251, 232);
            // 
            // changeProjectToolStripMenuItem
            // 
            this.changeProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProjectToolStripMenuItem});
            this.changeProjectToolStripMenuItem.Name = "changeProjectToolStripMenuItem";
            this.changeProjectToolStripMenuItem.Size = new System.Drawing.Size(250, 36);
            this.changeProjectToolStripMenuItem.Text = "Change Project";
            // 
            // addProjectToolStripMenuItem
            // 
            this.addProjectToolStripMenuItem.Name = "addProjectToolStripMenuItem";
            this.addProjectToolStripMenuItem.Size = new System.Drawing.Size(267, 40);
            this.addProjectToolStripMenuItem.Text = "<Add Project>";
            this.addProjectToolStripMenuItem.Click += new System.EventHandler(this.addProjectToolStripMenuItem_Click);
            // 
            // punchInOutToolStripMenuItem
            // 
            this.punchInOutToolStripMenuItem.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.punchInOutToolStripMenuItem.Name = "punchInOutToolStripMenuItem";
            this.punchInOutToolStripMenuItem.Size = new System.Drawing.Size(250, 36);
            this.punchInOutToolStripMenuItem.Text = "Punch In";
            this.punchInOutToolStripMenuItem.Click += new System.EventHandler(this.punchInOutToolStripMenuItem_Click);
            // 
            // punchOutAtToolStripMenuItem
            // 
            this.punchOutAtToolStripMenuItem.Enabled = false;
            this.punchOutAtToolStripMenuItem.Name = "punchOutAtToolStripMenuItem";
            this.punchOutAtToolStripMenuItem.Size = new System.Drawing.Size(250, 36);
            this.punchOutAtToolStripMenuItem.Text = "Punch Out At...";
            this.punchOutAtToolStripMenuItem.Click += new System.EventHandler(this.punchOutAtToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(247, 6);
            // 
            // viewTimeDetailsToolStripMenuItem
            // 
            this.viewTimeDetailsToolStripMenuItem.Name = "viewTimeDetailsToolStripMenuItem";
            this.viewTimeDetailsToolStripMenuItem.Size = new System.Drawing.Size(250, 36);
            this.viewTimeDetailsToolStripMenuItem.Text = "View Time Details";
            this.viewTimeDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewTimeDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuExport
            // 
            this.toolStripMenuExport.Name = "toolStripMenuExport";
            this.toolStripMenuExport.Size = new System.Drawing.Size(250, 36);
            this.toolStripMenuExport.Text = "Export";
            this.toolStripMenuExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(247, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(250, 36);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.closeButton.Location = new System.Drawing.Point(328, 629);
            this.closeButton.Margin = new System.Windows.Forms.Padding(6);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(138, 42);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // timeTrackerDataSet
            // 
            this.timeTrackerDataSet.DataSetName = "TimeTrackerDataSet";
            this.timeTrackerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 601);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.timeDetailsControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage1.Size = new System.Drawing.Size(786, 564);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Time Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // timeDetailsControl1
            // 
            this.timeDetailsControl1.Dataset = null;
            this.timeDetailsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeDetailsControl1.Location = new System.Drawing.Point(6, 6);
            this.timeDetailsControl1.Margin = new System.Windows.Forms.Padding(11);
            this.timeDetailsControl1.Name = "timeDetailsControl1";
            this.timeDetailsControl1.Size = new System.Drawing.Size(774, 552);
            this.timeDetailsControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.projectsListControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage2.Size = new System.Drawing.Size(786, 564);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Projects";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // projectsListControl1
            // 
            this.projectsListControl1.AutoSize = true;
            this.projectsListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectsListControl1.Location = new System.Drawing.Point(6, 6);
            this.projectsListControl1.Margin = new System.Windows.Forms.Padding(11);
            this.projectsListControl1.Name = "projectsListControl1";
            this.projectsListControl1.Size = new System.Drawing.Size(774, 552);
            this.projectsListControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(786, 564);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Maintenance";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entryIDDataGridViewTextBoxColumn,
            this.projectIDDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.timeEntriesBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.Size = new System.Drawing.Size(786, 564);
            this.dataGridView1.TabIndex = 0;
            // 
            // entryIDDataGridViewTextBoxColumn
            // 
            this.entryIDDataGridViewTextBoxColumn.DataPropertyName = "EntryID";
            this.entryIDDataGridViewTextBoxColumn.HeaderText = "EntryID";
            this.entryIDDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.entryIDDataGridViewTextBoxColumn.Name = "entryIDDataGridViewTextBoxColumn";
            this.entryIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.entryIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.entryIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // projectIDDataGridViewTextBoxColumn
            // 
            this.projectIDDataGridViewTextBoxColumn.DataPropertyName = "ProjectID";
            this.projectIDDataGridViewTextBoxColumn.DataSource = this.projectsBindingSource;
            this.projectIDDataGridViewTextBoxColumn.DisplayMember = "ProjectName";
            this.projectIDDataGridViewTextBoxColumn.HeaderText = "ProjectID";
            this.projectIDDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.projectIDDataGridViewTextBoxColumn.Name = "projectIDDataGridViewTextBoxColumn";
            this.projectIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.projectIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.projectIDDataGridViewTextBoxColumn.ValueMember = "ProjectID";
            this.projectIDDataGridViewTextBoxColumn.Width = 175;
            // 
            // projectsBindingSource
            // 
            this.projectsBindingSource.DataMember = "Projects";
            this.projectsBindingSource.DataSource = this.timeTrackerDataSet;
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.startTimeDataGridViewTextBoxColumn.Width = 175;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.MinimumWidth = 9;
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.Width = 175;
            // 
            // timeEntriesBindingSource
            // 
            this.timeEntriesBindingSource.DataMember = "TimeEntries";
            this.timeEntriesBindingSource.DataSource = this.timeTrackerDataSet;
            // 
            // locateXmlFilebutton
            // 
            this.locateXmlFilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.locateXmlFilebutton.AutoSize = true;
            this.locateXmlFilebutton.Location = new System.Drawing.Point(584, 638);
            this.locateXmlFilebutton.Name = "locateXmlFilebutton";
            this.locateXmlFilebutton.Size = new System.Drawing.Size(200, 25);
            this.locateXmlFilebutton.TabIndex = 10;
            this.locateXmlFilebutton.TabStop = true;
            this.locateXmlFilebutton.Text = "Locate XML Data File";
            this.locateXmlFilebutton.Click += new System.EventHandler(this.locateXmlFilebutton_Click);
            // 
            // ProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 684);
            this.ControlBox = false;
            this.Controls.Add(this.locateXmlFilebutton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.closeButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::TimeTracker.Properties.Settings.Default, "ProjectsFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::TimeTracker.Properties.Settings.Default.ProjectsFormLocation;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ProjectsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Time Entries";
            this.VisibleChanged += new System.EventHandler(this.ProjectsForm_VisibleChanged);
            this.notifyIconMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeTrackerDataSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEntriesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon timeTrackerNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem changeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem punchInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem punchOutAtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem viewTimeDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button closeButton;
        private TimeTrackerDataSet timeTrackerDataSet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private ProjectsListControl projectsListControl1;
        private TimeDetailsControl timeDetailsControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource timeEntriesBindingSource;
        private System.Windows.Forms.BindingSource projectsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn projectIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuExport;
        private System.Windows.Forms.LinkLabel locateXmlFilebutton;
    }
}

