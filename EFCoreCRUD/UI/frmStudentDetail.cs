using EFCoreCRUD.MANAGER;
using EFCoreCRUD.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFCoreCRUD.UI
{
    public partial class frmStudentDetail : Form
    {

        Form1 frm;

        public frmStudentDetail(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        StudentManager _studentManager = new();

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxName.Text))
                {
                    MessageBox.Show("Please enter a name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(textBoxSurname.Text))
                {
                    MessageBox.Show("Please enter surname.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxSurname.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(textBoxAddress.Text))
                {
                    MessageBox.Show("Please enter address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAddress.Focus();
                    return;
                }
                Student student = new Student();
                student.Id = Convert.ToInt32(idLabel.Text);
                student.Name = textBoxName.Text;
                student.Surename = textBoxSurname.Text;
                student.Address = textBoxAddress.Text;
                if (_studentManager.Update(student)) 
                {
                    MessageBox.Show("Student has been updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    frm.LoadData();
                }
                else
                {
                    MessageBox.Show("Student update failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
