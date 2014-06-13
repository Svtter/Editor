using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Editor
{
    public partial class Notepad : Form
    {
        /* saved 保存
         * firstSaved 用于第一次保存
         */
        bool saved = true;
        bool firstSaved = false;
        public Notepad()
        {
            InitializeComponent();
        }
        public Notepad(String args)
        {
            InitializeComponent();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void 自动换行ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (自动换行ToolStripMenuItem.Checked)
            {
                自动换行ToolStripMenuItem.Checked = false;
                richTextBox1.WordWrap = false;
            }
            else
            {
                自动换行ToolStripMenuItem.Checked = true;
                richTextBox1.WordWrap = true;
            }
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (状态栏ToolStripMenuItem.Checked)
            {
                状态栏ToolStripMenuItem.Checked = false;
                statusStrip1.Hide();
            }
            else
            {
                状态栏ToolStripMenuItem.Checked = true;
                statusStrip1.Show();
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowApply = true;
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
            richTextBox1.ForeColor = fontDialog1.Color;
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            撤销ToolStripMenuItem.Enabled = false;
        }

        private void 剪贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //影响的按钮
            撤销ToolStripMenuItem.Enabled = true;
            saved = false;
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘帖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 日期时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(DateTime.Now.ToString());
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 转到ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 新建RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                richTextBox1.Text = "";
            }
            else
            {
                //弹出保存选项
                if (DialogResult.Yes == MessageBox.Show("您还没有保存正在编辑的文件，是否放弃？[Y/N]", "尚未保存",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2))
                {
                    richTextBox1.Text = "";
                }
                else
                {
                    保存SToolStripMenuItem_Click(sender,e);
                    richTextBox1.Text = "";
                }
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!firstSaved)
            {
                saveFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = "保存文件";
                saveFileDialog1.ShowHelp = true;
                saveFileDialog1.CreatePrompt = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                //
                firstSaved = true;
                saved = true;
            }
            else
            {
                StreamWriter saveTemp = new StreamWriter(saveFileDialog1.FileName, true,
                Encoding.GetEncoding("gb2312"));
                saveTemp.Write(richTextBox1.Text);
                saveTemp.Close();
                saved = true;
            }
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.Title = "保存文件";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            StreamWriter saveTemp = new StreamWriter(saveFileDialog1.FileName, true,
                Encoding.GetEncoding("gb2312"));
            saveTemp.Write(richTextBox1.Text);
            saveTemp.Close();
            saved = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
                Application.Exit();
            else
            {
                if (DialogResult.Yes == MessageBox.Show("您还没有保存正在编辑的文件，是否放弃？[Y/N]", "尚未保存",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2))
                {
                    Application.Exit();
                }
                else
                {
                    保存SToolStripMenuItem_Click(sender, e);
                    Application.Exit();
                }
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            openFileDialog1.AddExtension = false;
            openFileDialog1.Filter = "所有文件|*.*|文本文件(*.txt)|*.txt";
            openFileDialog1.Title = "打开文件";
            //not choose a file
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            StreamReader open = new StreamReader(openFileDialog1.FileName,
                Encoding.GetEncoding("gb2312"));

            richTextBox1.Text = open.ReadToEnd();
            open.Close();
        }

        private void Notepad_Load(object sender, EventArgs e)
        {
            剪贴ToolStripMenuItem.Enabled = false;
            复制ToolStripMenuItem.Enabled = false;
            删除ToolStripMenuItem.Enabled = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitializeLifetimeService();
            folderBrowserDialog1.ShowDialog();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Equals(""))
            {
                剪贴ToolStripMenuItem.Enabled = false;
                复制ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
            }
            else
            {
                剪贴ToolStripMenuItem.Enabled = true;
                复制ToolStripMenuItem.Enabled = true;
                删除ToolStripMenuItem.Enabled = true;
            }
        }

        private void 关于记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Svitter\n", "about", MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }
    }
}
