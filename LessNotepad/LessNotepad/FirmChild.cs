using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Collections;
using System.IO;

namespace LessNotepad
{
    public partial class FirmChild : Form
    {
        //bool ifBold = false, ifitalic = false;
        public FirmChild()
        {
            InitializeComponent();
        }

        private void FirmChild_Load(object sender, EventArgs e)
        {
            InstalledFontCollection myFont = new InstalledFontCollection();
            FontFamily[] ff = myFont.Families;
            ArrayList list = new ArrayList();
            int count = ff.Length;
            for(int i = 0; i < count; i++)
            {
                string FontName = ff[i].Name;
                toolStripComboBoxFonts.Items.Add(FontName);
            }
        }

        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            if (!textBoxNote.Font.Bold)
            {
                if (textBoxNote.Font.Italic)
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Bold | FontStyle.Italic);
                    //ifBold = true;
                    //ifitalic = true;
                }
                else
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Bold);
                    //ifitalic = false;
                }
            }
            else
            {
                if (textBoxNote.Font.Italic)
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Italic);
                }
                else
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Regular);
                }
            }
        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            if (!textBoxNote.Font.Italic)
            {
                if (textBoxNote.Font.Bold)
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Bold | FontStyle.Italic);
                    //ifBold = true;
                    //ifitalic = true;
                }
                else
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Italic);
                    //ifBold = false;
                }
            }
            else
            {
                if (textBoxNote.Font.Bold) 
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Bold);
                }
                else
                {
                    textBoxNote.Font = new Font(textBoxNote.Font, FontStyle.Regular);
                }
            }
        }

        private void toolStripComboBoxFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ifitalic = textBoxNote.Font.Italic;
            //ifBold = textBoxNote.Font.Bold;
            string fontName = toolStripComboBoxFonts.Text;
            float fontSize = float.Parse(toolStripComboBoxSize.Text);
            //textBoxNote.Font = new Font(fontName, fontSize);
            if (textBoxNote.Font.Italic & textBoxNote.Font.Bold)
            {
                textBoxNote.Font = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Italic);
            }
            else if(!textBoxNote.Font.Italic & textBoxNote.Font.Bold)
            {
                textBoxNote.Font = new Font(fontName, fontSize, FontStyle.Bold);
            }
            else
            {
                textBoxNote.Font = new Font(fontName, fontSize,FontStyle.Italic);
            }
        }

        private void toolStripComboBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fontName = toolStripComboBoxFonts.Text;
            float fontSize = float.Parse(toolStripComboBoxSize.Text);
            //textBoxNote.Font = new Font(fontName, fontSize);
            if (textBoxNote.Font.Italic & textBoxNote.Font.Bold)
            {
                textBoxNote.Font = new Font(fontName, fontSize, FontStyle.Bold | FontStyle.Italic);
            }
            else if (!textBoxNote.Font.Italic & textBoxNote.Font.Bold)
            {
                textBoxNote.Font = new Font(fontName, fontSize, FontStyle.Bold);
            }
            else
            {
                textBoxNote.Font = new Font(fontName, fontSize, FontStyle.Italic);
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if(this.Text == "") 
            {
                if (textBoxNote.Text.Trim() != "")
                {
                    toolStripLabelMark.Text = "";
                    saveFileDialog1.Filter = ("文本文档（*.txt）|*.txt");
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        StreamWriter sw = new StreamWriter(path, false);
                        sw.WriteLine(textBoxNote.Text.Trim());
                        this.Text = path;
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    MessageBox.Show("空文件不可保存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string path = this.Text;
                StreamWriter sw = new StreamWriter(path, false);
                sw.WriteLine(textBoxNote.Text.Trim());
                sw.Flush();
                sw.Close();
            }
            
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ("文本文档（*.txt）|*.txt");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                string text = sr.ReadToEnd();
                textBoxNote.Text = text;
                this.Text = path;
                toolStripLabelMark.Text = "";
                sr.Close();
            }
        }

        private void textBoxNote_TextChanged(object sender, EventArgs e)
        {
            toolStripLabelMark.Text = "*";
        }

        private void FirmChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(toolStripLabelMark.Text == "*")
            {
               DialogResult dr = MessageBox.Show("文档尚未保存，是否保存", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(dr == DialogResult.Yes)
                {
                    if (this.Text == "")
                    {
                        toolStripLabelMark.Text = "";
                        if (textBoxNote.Text.Trim() != "")
                        {
                            saveFileDialog1.Filter = ("文本文档（*.txt）|*.txt");
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                //toolStripLabelMark.Text = "";
                                string path = saveFileDialog1.FileName;
                                StreamWriter sw = new StreamWriter(path, false);
                                sw.WriteLine(textBoxNote.Text.Trim());
                                this.Text = path;
                                sw.Flush();
                                sw.Close();
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("空文件不可保存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        
                        string path = this.Text;
                        StreamWriter sw = new StreamWriter(path, false);
                        sw.WriteLine(textBoxNote.Text.Trim());
                        sw.Flush();
                        sw.Close();
                        
                    }
                }
                else if(dr == DialogResult.No)
                {
                    this.Dispose();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            if (toolStripLabelMark.Text == "*")
            {
                DialogResult dr = MessageBox.Show("文档尚未保存，是否保存", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (this.Text == "")
                    {
                        if (textBoxNote.Text.Trim() != "")
                        {
                            toolStripLabelMark.Text = "";
                            saveFileDialog1.Filter = ("文本文档（*.txt）|*.txt");
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                string path = saveFileDialog1.FileName;
                                StreamWriter sw = new StreamWriter(path, false);
                                sw.WriteLine(textBoxNote.Text.Trim());
                                this.Text = path;
                                sw.Flush();
                                sw.Close();
                                //toolStripLabelMark.Text = "";
                                textBoxNote.Text = "";
                                toolStripLabelMark.Text = "";
                                this.Text = "";
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("空文件不可保存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string path = this.Text;
                        StreamWriter sw = new StreamWriter(path, false);
                        sw.WriteLine(textBoxNote.Text.Trim());
                        sw.Flush();
                        sw.Close();
                        //toolStripLabelMark.Text = "";
                    }
                }
                else if (dr == DialogResult.No)
                {
                    textBoxNote.Text = "";
                    toolStripLabelMark.Text = "";
                    this.Text = "";
                }
            }
            
        }
    }
}
