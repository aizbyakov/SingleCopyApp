using System.Windows.Forms;

namespace SingleCopyAppTest
{
    public partial class MainForm : Form
    {
        private AnotherForm anotherForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpenAnotherForm_Click(object sender, System.EventArgs e)
        {
            anotherForm = new AnotherForm();
            anotherForm.ShowDialog();
        }
    }
}
