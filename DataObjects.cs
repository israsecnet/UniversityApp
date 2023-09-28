using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniversityApp
{
    [SQLite.Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int courseId { get; set; }
        public int termId { get; set; }
        public Course() { }
        public Course(int termId, int instructorId, string courseName, DateTime start, DateTime end, string status, string courseDetails, int pa, int oa)
        {
            this.termId = termId;
            this.instructorId = instructorId;
            this.courseName = courseName;
            this.start = start;
            this.end = end;
            this.status = status;
            this.courseDetails = courseDetails;
            this.pa = pa;
            this.oa = oa;
        }
        
        public int instructorId { get; set; }
        public string courseName { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string status { get; set; }
        public string courseDetails { get; set; }
        public int pa { get; set; }
        public int oa {  get; set; }
        public int startNotification {  get; set; }
        public int endNotification { get; set; }

    }

    [SQLite.Table("Terms")]
    public class Term
    {
        public Term() { }
        public Term(string termName, DateTime start, DateTime end)
        {
            this.termName = termName;
            this.start = start;
            this.end = end;
        }
        [PrimaryKey, AutoIncrement]
        public int termId { get; set; }
        public string termName { get; set; }
        public DateTime start{ get; set;}
        public DateTime end { get; set; }
    }

    [SQLite.Table("Instructors")]
    public class Instructor
    {
        public Instructor() { }
        public Instructor(string instructorName, string instructorPhone, string instructorEmail) 
        {
            this.instructorName = instructorName;
            this.instructorPhone = instructorPhone;
            this.instructorEmail = instructorEmail;
        }
        [PrimaryKey, AutoIncrement]
        public int instructorId { get; set; }
        public string instructorName { get; set; }
        public string instructorPhone { get; set; }
        public string instructorEmail { get; set; }
    }

    [SQLite.Table("Notes")]
    public class Note
    {
        public Note() { }
        public Note(int courseId, string content)
        {
            this.courseId = courseId;
            this.content = content;
        }
        [PrimaryKey, AutoIncrement]
        public int noteId { get; set; }
        public int courseId { get; set; }
        public string content { get; set; }
    }

    [SQLite.Table("Assessments")]
    public class Assessment
    {
        public Assessment() { }
        public Assessment(int type, string assessmentName, DateTime start, DateTime end, string assessmentDetails, int courseId)
        {
            this.type = type;
            this.assessmentName = assessmentName;
            this.start = start;
            this.end = end;
            this.assessmentDetails = assessmentDetails;
            this.courseId = courseId;
            dueDate = end;
        }
        [PrimaryKey, AutoIncrement]
        public int assessmentId { get; set; }
        public int type { get; set; }
        public string assessmentName { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int startNotif { get; set; }
        public int endNotif { get; set; }
        public string assessmentDetails { get; set; }
        public int courseId { get; set; }
        public DateTime dueDate { get; set; }
    }
}
