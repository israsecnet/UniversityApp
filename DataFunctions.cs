using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApp
{

    public static class DataFunctions
    {

        public static void deleteNote(Note note)
        {
            var db = new SQLiteConnection(MainPage.databasePath);
            db.Delete(note);
        }

        public static void deleteCourse(Course course)
        {
            var db = new SQLiteConnection(MainPage.databasePath);
            db.Delete(course);
        }

        public static void addNewTerm()
        {
            var db = new SQLiteConnection(MainPage.databasePath);
            var resp = db.Query<Term>($"SELECT * FROM Terms ORDER BY termId DESC LIMIT 1");
            Term nt = resp.First();
            string termName = "Term " + (nt.termId+1).ToString();
            Term rt = new Term(termName, DateTime.Now, DateTime.Now.AddDays(60));
            db.Insert(rt);
            MainPage.sync_db();
        }

        public static void addNewCourse(int termId)
        {
            var db = new SQLiteConnection(MainPage.databasePath);
            Assessment PA = new Assessment(1, "PerformanceAssessment23", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 1);
            Assessment OA = new Assessment(0, "ObjectiveAssessment23", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 1);
            Course course1 = new Course(termId, 1, "NewCourse23", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            addCourse(db, course1);
            List<Course> resp = db.Query<Course>($"SELECT courseId FROM Courses WHERE courseName='NewCourse23'");
            PA.courseId = resp[0].courseId;
            OA.courseId = resp[0].courseId;
            addAssessment(db, PA);
            addAssessment(db, OA);
            List<Assessment> resp2 = db.Query<Assessment>($"SELECT assessmentId FROM Assessments WHERE courseId='{resp[0].courseId.ToString()}'");
            foreach (Assessment a in resp2)
            {
                if (a.type == 1)
                {
                    course1.pa = a.assessmentId;
                }
                else
                {
                    course1.oa = a.assessmentId;
                }
            }
            db.Update(course1);
            MainPage.sync_db();


        }
        public static void createTable()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            var db = new SQLiteConnection(databasePath);
            db.CreateTable<Term>();
            db.CreateTable<Course>();
            db.CreateTable<Assessment>();
            db.CreateTable<Instructor>();
            db.CreateTable<Note>();
        }

        public static void addTerm(SQLiteConnection db, Term term)
        {
            db.Insert(term);
        }
        public static void updateTerm(SQLiteConnection db, Term term)
        {
            db.Update(term);
        }
        public static void addCourse(SQLiteConnection db, Course course)
        {
            db.Insert(course);
        }
        public static void updateCourse(SQLiteConnection db, Course course)
        {
            db.Update(course);
        }
        public static void addAssessment(SQLiteConnection db, Assessment assessment)
        {
            db.Insert(assessment);
        }
        public static void updateAssessment(SQLiteConnection db, Assessment assessment)
        {
            db.Update(assessment);
        }
        public static void addInstructor( SQLiteConnection db, Instructor instructor)
        {
            db.Insert(instructor);
        }
        public static void updateInstructor(SQLiteConnection db, Instructor instructor)
        {
            db.Update(instructor);
        }
        public static void addNote(SQLiteConnection db, Note note)
        {
            db.Insert(note);
        }
        public static void updateNote(SQLiteConnection db, Note note)
        {
            db.Update(note);
        }
        public static List<Course> getCourses(SQLiteConnection db, Term term)
        {
            var courses = db.Query<Course>($"SELECT * FROM Courses WHERE termId={term.termId}");
            List<Course> result = new List<Course>();
            foreach (Course course in courses)
            {
                result.Add(course);
            }
            return result;
        }
    }
}
