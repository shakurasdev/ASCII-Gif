using ASCIITools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GifConversionApp
{
    public partial class FormGifConverter : Form
    {
        public FormGifConverter()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private BackgroundWorker _backgroundWorker;

        private void Form1_Load(object sender, EventArgs e)
        {
            cbLowerCase.Checked = true;
            cbUpperCase.Checked = true;
            cbNumbers.Checked = true;
            cbSpecialChars1.Checked = true;
            cbSpecialChars2.Checked = true;
            cbSpecialChars3.Checked = true;
            
            this.Text = "GifConverter";
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button1.Text = "select .gif and convert";
            button1.Font = new Font(FontFamily.GenericMonospace, 12);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = false;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            progressBar.Minimum = 0;
            progressBar.Step = 1;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Visible = true;
            button1.Enabled = true;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            GifConverterBackgroundArgument arg = e.Argument as GifConverterBackgroundArgument;

            arg.GifConverter.CreateGifAtCurrentFrame(bw, arg.Filename);
        }

        private ASCIICategories CheckboxesIntoCategoriesStruct()
        {
            return new ASCIICategories
            {
                LowerCase = cbLowerCase.Checked,
                UpperCase = cbUpperCase.Checked,
                Numbers = cbNumbers.Checked,
                SpecialChars1 = cbSpecialChars1.Checked,
                SpecialChars2 = cbSpecialChars2.Checked,
                SpecialChars3 = cbSpecialChars3.Checked,
                SpecialChars4 = cbSpecialChars4.Checked
            };
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            ASCIICategories categories = CheckboxesIntoCategoriesStruct();

            if (categories.ContainsAtLeastOne())
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Nur gifs (gif) | *.gif";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Image gif = Image.FromStream(ofd.OpenFile());
                        int teilungsfaktor = (int)numericUpDownDivisor.Value;
                        int fontsize = (int)numericUpDownFont.Value;

                        GifConverter gifConverter = new GifConverter(gif, teilungsfaktor, fontsize, categories);

                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.FileName = "result";
                            sfd.DefaultExt = "gif";
                            sfd.Filter = "Gifs (*.gif)|*.gif";

                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                if (sfd.FileName != ofd.FileName)
                                {
                                    b.Visible = false;
                                    b.Enabled = false;
                                    progressBar.Maximum = gifConverter.TotalFrames;
                                    progressBar.Value = 0;
                                    _backgroundWorker.RunWorkerAsync(new GifConverterBackgroundArgument() { GifConverter = gifConverter, Filename = sfd.FileName });
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You need to select at least one type of chars.");
            }
            
        }
    }
}