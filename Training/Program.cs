using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.IRepository;
using Training.Model.Entity;
using Traning.Repository;
using Traning.Repository.Helpers;

namespace Training
{
    class Program
    {

        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["TrainingDB"].ToString();
            try
            {
                SQLHelper.Initialize(connString);
                SQLHelper.AssureDatabase("Subjects");
                SQLHelper.AssureDatabase("Students");
                SQLHelper.AssureDatabase("StudentsXSubjects");
                StudentRepository studentRepository = new StudentRepository();

                //StudentEntity student = new StudentEntity();
                //student.Name = "Draghici";
                //student.Surname = "Alice";
                //student.PhoneNo = "0724059799";
                //studentRepository.Add(student);

                //StudentEntity student2 = new StudentEntity();
                //student2.Name = "Pavel";
                //student2.Surname = "Marcela";
                //student2.PhoneNo = "0729874154";
                //studentRepository.Add(student2);

                SubjectRepository subjectRepository = new SubjectRepository();

                //SubjectEntity subject1 = new SubjectEntity();
                //subject1.Description = "Matematica";
                //SubjectEntity subject2 = new SubjectEntity();
                //subject2.Description = "Algebra";
                //subjectRepository.Add(subject1);
                //subjectRepository.Add(subject2);
                //subject2.Description = "Franceza";
                //subjectRepository.Update(1, subject2);
                //subjectRepository.Delete(4);
                //subjectRepository.Delete(5);
                //subjectRepository.Delete(6);

                var subjects = subjectRepository.Get();
                var subjectById = subjectRepository.GetById(2);
                studentRepository.AssignToSubject(7, 2);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
