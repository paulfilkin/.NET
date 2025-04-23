namespace XmlTransformerApp
{
    partial class MainForm
    {
        private System.Windows.Forms.Label xmlFileLabel;
        private System.Windows.Forms.Label xslFileLabel;
        private System.Windows.Forms.Button selectXmlButton;
        private System.Windows.Forms.Button selectXslButton;
        private System.Windows.Forms.Button transformButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button viewSourceButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label dropLabel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.PictureBox iconPictureBox;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Control Panel
            this.controlPanel = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 150,
                BackColor = System.Drawing.Color.FromArgb(230, 230, 230)
            };

            // XML File Selection
            this.selectXmlButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(10, 20),
                Size = new System.Drawing.Size(120, 30),
                Text = "Select XML",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };
            this.selectXmlButton.Click += new System.EventHandler(this.SelectXmlButton_Click);

            this.xmlFileLabel = new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(140, 20),
                Size = new System.Drawing.Size(300, 30),
                Text = "No XML file selected",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            // XSL File Selection
            this.selectXslButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(10, 60),
                Size = new System.Drawing.Size(120, 30),
                Text = "Select XSL",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };
            this.selectXslButton.Click += new System.EventHandler(this.SelectXslButton_Click);

            this.xslFileLabel = new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(140, 60),
                Size = new System.Drawing.Size(300, 30),
                Text = "No XSL file selected",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            // Transform Button
            this.transformButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(120, 30),
                Text = "Transform",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(100, 149, 237),
                ForeColor = System.Drawing.Color.White
            };
            this.transformButton.Click += new System.EventHandler(this.TransformButton_Click);

            // Save Button
            this.saveButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(140, 100),
                Size = new System.Drawing.Size(120, 30),
                Text = "Save Output",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            // View Source Button
            this.viewSourceButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(270, 100),
                Size = new System.Drawing.Size(120, 30),
                Text = "View Source",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };
            this.viewSourceButton.Click += new System.EventHandler(this.ViewSourceButton_Click);

            // Drop Area Label
            this.dropLabel = new System.Windows.Forms.Label
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Text = "Drag and drop XML and XSL files here",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.FromArgb(220, 220, 220),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

            // Icon PictureBox
            this.iconPictureBox = new System.Windows.Forms.PictureBox
            {
                Size = new System.Drawing.Size(64, 64),
                Location = new System.Drawing.Point(535, 5), // Initial position near top-right corner (adjusted for form's client size of 600x400)
                Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right, // Anchor to top-right corner
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            };

            // Status Label
            this.statusLabel = new System.Windows.Forms.Label
            {
                Dock = System.Windows.Forms.DockStyle.Bottom,
                Height = 30,
                Text = "Drag and drop or select XML and XSL files.",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.FromArgb(220, 220, 220)
            };

            // Add Controls to Panel
            this.controlPanel.Controls.Add(this.selectXmlButton);
            this.controlPanel.Controls.Add(this.xmlFileLabel);
            this.controlPanel.Controls.Add(this.selectXslButton);
            this.controlPanel.Controls.Add(this.xslFileLabel);
            this.controlPanel.Controls.Add(this.transformButton);
            this.controlPanel.Controls.Add(this.saveButton);
            this.controlPanel.Controls.Add(this.viewSourceButton);

            // Add Controls to Form
            this.Controls.Add(this.dropLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.statusLabel);

            // Form Settings
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }
    }
}