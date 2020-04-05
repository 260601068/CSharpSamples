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
    public partial class FormWriter : Form
    {
        private LetterModel letter;

        public delegate void ChangeList();
        public event ChangeList changeListEvent;
        public FormWriter()
        {
            InitializeComponent();
        }

        public FormWriter(LetterModel letter)
        {
            InitializeComponent();
            this.letter = letter;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            LetterModel letterModel = new LetterModel();
            letterModel.Title = this.textBoxTitle.Text;
            letterModel.Content = this.textBoxContent.Text;
            letterModel.Receiver = this.textBoxReceiver.Text;
            letterModel.AddTime = DateTime.Now;
            if (LetterBll.DealWriter(letterModel))
            {
                changeListEvent();
                MessageBox.Show("发送成功！");
            }
            else
            {
                MessageBox.Show("发送失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormWriter_Load(object sender, EventArgs e)
        {
            if(this.letter != null)
            {
                this.textBoxTitle.Text = letter.Title;
                this.textBoxContent.Text = letter.Content;
                this.textBoxReceiver.Text = letter.Receiver;
            }
        }
    }
}
