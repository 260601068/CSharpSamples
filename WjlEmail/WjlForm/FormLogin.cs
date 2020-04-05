using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WjlForm
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string account=ConfigurationManager.AppSettings["account"];
            string password = ConfigurationManager.AppSettings["password"];
            string accountInput=textBoxAccount.Text;
            string passwordInput = textBoxPassword.Text;
            if(string.IsNullOrEmpty(accountInput) || string.IsNullOrEmpty(passwordInput)){
                labelLoginRes.Text = "用户名密码不能为空！";
                    return;
            }
            if(!account.Equals(accountInput) || !password.Equals(passwordInput))
            {
                labelLoginRes.Text = "账号和密码不正确！";
                    return;
            }
            Form formMain=new FormMain();
            formMain.Show();
            this.Hide();
        }
    }
}
