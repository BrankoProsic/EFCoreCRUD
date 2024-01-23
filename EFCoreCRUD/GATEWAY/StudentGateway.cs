using EFCoreCRUD.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCRUD.GATEWAY
{
    public class StudentGateway
    {
        AppDbContext _dbContext = new AppDbContext();

        public bool Add(Student student)
        {
            _dbContext.Students.Add(student);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Student>GetAll()
        {
            return _dbContext.Students.ToList();
        }

        public bool Update(Student student)
        {
            var data = _dbContext.Students.FirstOrDefault(c => c.Id == student.Id);
            if(data == null)
            {
                return false;
            }
            data.Name = student.Name;
            data.Surename = student.Surename;
            data.Address = student.Address;
            return _dbContext.SaveChanges()>0;
        }
        public bool Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return false;
            }
            _dbContext.Students.Remove(student);
            return _dbContext.SaveChanges() > 0;
        }

    }
}
