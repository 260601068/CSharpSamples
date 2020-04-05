using EmailClient.BLL;
using EmailClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WjlForm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormWriter formWriter = new FormWriter();
            formWriter.changeListEvent += FormWriter_changeListEvent;
            formWriter.ShowDialog();
        }
        private void FormWriter_changeListEvent()
        {
            this.dataGridView1.DataSource = LetterBll.GetList("");
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baidu.cn");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = LetterBll.GetListByPage("", "ID asc", 1, 3);
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            this.labelPageNum.Text = (Convert.ToInt32(this.labelPageNum.Text) + 1).ToString();
            this.dataGridView1.DataSource = LetterBll.GetListByPage("", "ID asc", (Convert.ToInt32(this.labelPageNum.Text)-1)*3+1, Convert.ToInt32(this.labelPageNum.Text) * 3);
        }

        private void buttonPrePage_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(this.labelPageNum.Text) - 1<1) return;
            this.labelPageNum.Text = (Convert.ToInt32(this.labelPageNum.Text) -1).ToString();
            this.dataGridView1.DataSource = LetterBll.GetListByPage("", "ID asc", (Convert.ToInt32(this.labelPageNum.Text) - 1) * 3 + 1, Convert.ToInt32(this.labelPageNum.Text) * 3);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //选中（蓝色显示）鼠标点击的行
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    selectId= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }
        int selectId = -1;
        private void 编辑ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (selectId != -1)
            {
                LetterModel letter = LetterBll.GetModelById(selectId);
                FormWriter formWriter = new FormWriter(letter);
                formWriter.ShowDialog();
            }
        }
    }
}
