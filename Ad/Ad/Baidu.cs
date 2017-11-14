using System;
using System.Windows.Forms;

namespace Ad
{
    public partial class BaiduWindow : Form
    {
        public BaiduWindow()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baidu.com");
            this.Close();
        }
    }
}
