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
