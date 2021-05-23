/*Design & develop a web application to maintain records of students.
The single the record shall contain the following information: 
| Name | Roll number | Age | Gender |
 
The application should have the ability to INSERT, DELETE, UPDATE & SEARCH records.
Please Note: this application should not use any DATABASE.

For the above problem statment, I have created MVC asp.net application
Because of some authorization issue, this solution can not be  pushed to GitHub.
So, committing the Action methods for insert, update and delete actions
 */



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaintenanceOfStudentRecords.Models;


namespace MaintenanceOfStudentRecords.Controllers
{
    public class HomeController : Controller
    {
        string file = @"~\Files\TextFile1.txt";

        [NonAction]
        public List<StudentInfo> listOfStudents()
        {
            List<StudentInfo> recordList = new List<StudentInfo>();
            string[] lines = System.IO.File.ReadAllLines(file);
            for (int i = 0; i < lines.Length; i++)
            {
                StudentInfo studentrecord = new StudentInfo();
                string line = lines[i];
                string[] studentFields = line.Split(',');
                studentrecord.Name = studentFields[0];
                studentrecord.RollNumber = studentFields[1];
                studentrecord.Age = Convert.ToInt32(studentFields[2]);
                studentrecord.Gender = studentFields[3];
                recordList.Add(studentrecord);

            }
            return recordList;

        }

        //To display all the students list in the Home page UI
        public ActionResult Index()
        {
            List<StudentInfo> recordList = new List<StudentInfo>();
            recordList = listOfStudents();
             return View(recordList);
        }

        //Inserting new student record
        public ActionResult insertStudent(StudentInfo record)
        {
            StudentInfo info = new StudentInfo();
            var data = record.Name + ',' + record.RollNumber + ',' + record.Age + ',' + record.Gender;
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(data,true);
            }
                      
            
            return View(info);

        }

        //Update of existing student record
        //Roll number is considered as unique key value
        public ActionResult updateStudent(StudentInfo record)
        {
                      
            string[] lines = System.IO.File.ReadAllLines(file);
            for (int i = 0; i< lines.Length; i++)
            {
                string line = lines[i];
                string[] temp = line.Split(',');
               // info.Name = temp[0];
                
                    var data = record.Name + ',' + record.RollNumber + ',' + record.Age + ',' + record.Gender;
                if (data != line && data.Contains(temp[1]))
                {
                        lines[i] = data;
                        System.IO.File.WriteAllLines(file,lines);
                        break;
                    }
                    
                   
                }
            
          
          
            return View(listOfStudents());
        }

        //Deletion of student record
        public ActionResult deleteStudent(string rollNum)
        {
                     
            List<string> lines = System.IO.File.ReadAllLines(file).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] temp = line.Split(',');

                
                if (temp[1].Equals(rollNum))
                {
                    lines.Remove(line);
                    System.IO.File.WriteAllLines(file, lines);
                    break;
                }

              
            }

            return View(listOfStudents());
        }


    }
}
