using SQLite;
using System.Windows.Input;

namespace UniversityApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            loadData();
        }

        public void loadData()
        {
            DataFunctions.createTable();
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            var db = new SQLiteConnection(databasePath);

            Term term1 = new Term("Term1", DateTime.Now, DateTime.Now.AddMonths(6));
            DataFunctions.addTerm(db, term1);

            Course course1 = new Course(1,1,"Course Example 1:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            Course course2 = new Course(1, 1, "Course Example 2:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            Course course3 = new Course(1, 1, "Course Example 3:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            Course course4 = new Course(1, 1, "Course Example 4:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            Course course5 = new Course(1, 1, "Course Example 5:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            Course course6 = new Course(1, 1, "Course Example 6:", DateTime.Now, DateTime.Now.AddMonths(4), "Active", "Enter Course Details Here:");
            DataFunctions.addCourse(db, course1);
            DataFunctions.addCourse(db, course2);
            DataFunctions.addCourse(db, course3);
            DataFunctions.addCourse(db, course4);
            DataFunctions.addCourse(db, course5);
            DataFunctions.addCourse(db, course6);

            Assessment PA = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 1);
            Assessment OA = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 1);
            Assessment PA2 = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 2);
            Assessment OA2 = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 2);
            Assessment PA3 = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 3);
            Assessment OA3 = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 3);
            Assessment PA4 = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 4);
            Assessment PA5 = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 5);
            Assessment OA4 = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 4);
            Assessment OA5 = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 5);
            Assessment PA6 = new Assessment(1, "Performance Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 6);
            Assessment OA6 = new Assessment(0, "Objective Assessment #1", DateTime.Now, DateTime.Now.AddMonths(3), "Enter details about assessment here:", 6);
            DataFunctions.addAssessment(db, PA);
            DataFunctions.addAssessment(db, OA);
            DataFunctions.addAssessment(db, PA2);
            DataFunctions.addAssessment(db, OA2);
            DataFunctions.addAssessment(db, PA3);
            DataFunctions.addAssessment(db, OA3);
            DataFunctions.addAssessment(db, PA4);
            DataFunctions.addAssessment(db, OA4);
            DataFunctions.addAssessment(db, PA5);
            DataFunctions.addAssessment(db, OA5);
            DataFunctions.addAssessment(db, PA6);
            DataFunctions.addAssessment(db, OA6);

            Instructor instructor = new Instructor("Anika Patel","555-123-4567","anika.patel@strimeuniversity.edu");
            DataFunctions.updateInstructor(db, instructor);


            List<Term> terms = new List<Term>();
            Dictionary<Term, Course> courses = new Dictionary<Term, Course>();
            
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseView());
        }
    }
}