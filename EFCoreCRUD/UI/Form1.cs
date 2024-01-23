using EFCoreCRUD.MANAGER;
using EFCoreCRUD.MODEL;
using EFCoreCRUD.UI;

namespace EFCoreCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StudentManager _studentManager = new StudentManager();
        //AppDbContext _dbContext = new AppDbContext();

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData()
        {
            var students = _studentManager.GetAll();
            studentDataGridView.Rows.Clear();
            foreach (var student in students)
            {
                studentDataGridView.Rows.Add(student.Id, student.Name, student.Surename, student.Address);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(textBoxName.Text))
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
                student.Name = textBoxName.Text;
                student.Surename = textBoxSurname.Text;
                student.Address = textBoxAddress.Text;
                //_dbContext.Students.Add(student); sada je nepotrebno jer je uvrsten * StudentManager
                if(_studentManager.Add(student)) // obrisao * (_dbContext.SaveChanges()>0)
                {
                    MessageBox.Show("Student has been saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Student save failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void Reset()
        {
            textBoxName.Text = string.Empty;
            textBoxSurname.Clear();
            textBoxAddress.Clear();
            LoadData();
        }

        private void studentDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = studentDataGridView.SelectedRows[0];
                frmStudentDetail frm = new frmStudentDetail(this);
                //frm.Show();
                frm.idLabel.Text = dr.Cells[0].Value.ToString();
                frm.textBoxName.Text = dr.Cells[1].Value.ToString();
                frm.textBoxSurname.Text = dr.Cells[2].Value.ToString();
                frm.textBoxAddress.Text = dr.Cells[3].Value.ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = studentDataGridView.SelectedRows[0];
                if (MessageBox.Show("Do you want to delete?", "Qeuestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = (int)dr.Cells[0].Value;
                    bool isDelete = _studentManager.Delete(id);
                    if (isDelete)
                    {
                        MessageBox.Show("Student has been removed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ssLoadData();
                        studentDataGridView.Rows.Remove(dr);
                    }
                    else
                    {
                        MessageBox.Show("Student removed failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}