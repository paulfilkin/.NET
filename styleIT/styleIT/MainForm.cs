using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Drawing;

namespace XmlTransformerApp
{
    public partial class MainForm : Form
    {
        private string xmlPath;
        private string xslPath;
        private string lastOutputPath;

        public MainForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "StyleIT - XML to HTML Transformer";
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;

            // Set the form icon at runtime
            try
            {
                if (File.Exists("styleIT.ico"))
                {
                    this.Icon = new Icon("styleIT.ico");
                }
                else
                {
                    statusLabel.Text = "Icon file 'styleIT.ico' not found. Using default icon.";
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error loading icon: {ex.Message}. Using default icon.";
            }

            // Load the icon into the PictureBox
            try
            {
                if (File.Exists("styleIT.ico"))
                {
                    iconPictureBox.Image = Image.FromFile("styleIT.ico");
                }
                else
                {
                    statusLabel.Text = "Icon file 'styleIT.ico' not found. PictureBox will be empty.";
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Error loading icon into UI: {ex.Message}. PictureBox will be empty.";
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    xmlPath = file;
                    xmlFileLabel.Text = Path.GetFileName(file);
                }
                else if (file.EndsWith(".xsl", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".xslt", StringComparison.OrdinalIgnoreCase))
                {
                    xslPath = file;
                    xslFileLabel.Text = Path.GetFileName(file);
                }
            }
            UpdateStatus();
            if (!string.IsNullOrEmpty(xmlPath) && !string.IsNullOrEmpty(xslPath))
            {
                PerformTransform();
            }
        }

        private void SelectXmlButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xmlPath = openFileDialog.FileName;
                    xmlFileLabel.Text = Path.GetFileName(xmlPath);
                    UpdateStatus();
                }
            }
        }

        private void SelectXslButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XSL Files (*.xsl;*.xslt)|*.xsl;*.xslt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xslPath = openFileDialog.FileName;
                    xslFileLabel.Text = Path.GetFileName(xslPath);
                    UpdateStatus();
                }
            }
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xmlPath) || string.IsNullOrEmpty(xslPath))
            {
                MessageBox.Show("Please select both an XML and an XSL file.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PerformTransform();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastOutputPath) || !File.Exists(lastOutputPath))
            {
                MessageBox.Show("No transformation output available to save.", "No Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML Files (*.html)|*.html";
                saveFileDialog.DefaultExt = "html";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(lastOutputPath, saveFileDialog.FileName, true);
                    statusLabel.Text = $"Output saved to {saveFileDialog.FileName}";
                }
            }
        }

        private void ViewSourceButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastOutputPath) || !File.Exists(lastOutputPath))
            {
                MessageBox.Show("No transformation output available to view.", "No Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (Form sourceForm = new Form())
            {
                sourceForm.Text = "HTML Source";
                sourceForm.Size = new Size(800, 600);
                TextBox sourceBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Consolas", 10),
                    Text = File.ReadAllText(lastOutputPath)
                };
                sourceForm.Controls.Add(sourceBox);
                sourceForm.ShowDialog();
            }
        }

        private void PerformTransform()
        {
            try
            {
                // Validate XML and XSL
                ValidateFile(xmlPath, "XML");
                ValidateFile(xslPath, "XSL");

                lastOutputPath = Path.Combine(Path.GetTempPath(), $"output_{Guid.NewGuid()}.html");
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslPath);
                xslt.Transform(xmlPath, lastOutputPath);

                // Open in default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = lastOutputPath,
                    UseShellExecute = true
                });

                statusLabel.Text = "Transformation successful!";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Transformation failed.";
                MessageBox.Show($"Error during transformation: {ex.Message}", "Transformation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateFile(string filePath, string fileType)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid {fileType} file: {ex.Message}");
            }
        }

        private void UpdateStatus()
        {
            if (!string.IsNullOrEmpty(xmlPath) && !string.IsNullOrEmpty(xslPath))
                statusLabel.Text = "Ready to transform.";
            else
                statusLabel.Text = "Drag and drop or select XML and XSL files.";
        }
    }
}