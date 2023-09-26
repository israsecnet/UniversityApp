using SQLite;
using System.Windows.Input;

namespace UniversityApp
{
    public partial class MainPage : ContentPage
    {
        public static List<Term> terms = new List<Term>();
        public static Dictionary<Term, List<Course>> courses = new Dictionary<Term, List<Course>>();
        public static Dictionary<int, Course> courseList = new Dictionary<int, Course>();
        public static Dictionary<int, Assessment> assessments = new Dictionary<int, Assessment>();
        public static Dictionary<int, Instructor> instructors = new Dictionary<int, Instructor>();
        public static Dictionary<int, Note> notes = new Dictionary<int, Note>();
        public static Term termSelected;
        public static List<String> statusValues = new List<String>();
        public static string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
        public MainPage()
        {
            InitializeComponent();
            //addExampleData();
            load_saved_data();
            load_ui(1);
            statusValues.Add("In Progress");
            statusValues.Add("Completed");
            statusValues.Add("Dropped");
            statusValues.Add("Plan to Take");
        }

        public void addExampleData()
        {
            File.Delete(databasePath);
            DataFunctions.createTable();

            var db = new SQLiteConnection(databasePath);

            Term term1 = new Term("Term 1", DateTime.Now, DateTime.Now.AddMonths(6));
            Term term2 = new Term("Term 2", DateTime.Now, DateTime.Now.AddMonths(6));
            DataFunctions.addTerm(db, term1);
            DataFunctions.addTerm(db, term2);

            Course course1 = new Course(1, 1, "Math 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 1, 2);
            Course course2 = new Course(1, 1, "English 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 3, 4);
            Course course3 = new Course(1, 1, "Science 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 1, 1);
            Course course4 = new Course(1, 1, "History 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 1, 1);
            Course course5 = new Course(1, 1, "Writing 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 1, 1);
            Course course6 = new Course(1, 1, "Spanish 101", DateTime.Now, DateTime.Now.AddMonths(4), "In Progress", "Enter Course Details Here:", 1, 1);
            DataFunctions.addCourse(db, course1);
            DataFunctions.addCourse(db, course2);
            DataFunctions.addCourse(db, course3);
            DataFunctions.addCourse(db, course4);
            DataFunctions.addCourse(db, course5);
            DataFunctions.addCourse(db, course6);
            course1 = new Course(2, 1, "Math 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            course2 = new Course(2, 1, "English 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            course3 = new Course(2, 1, "Science 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            course4 = new Course(2, 1, "History 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            course5 = new Course(2, 1, "Writing 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
            course6 = new Course(2, 1, "Spanish 202", DateTime.Now, DateTime.Now.AddMonths(4), "Plan to Take", "Enter Course Details Here:", 1, 2);
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

            Instructor instructor = new Instructor("Anika Patel", "555-123-4567", "anika.patel@strimeuniversity.edu");
            DataFunctions.addInstructor(db, instructor);

        }

        private void load_saved_data()
        {
            var db = new SQLiteConnection(databasePath);

            var tmpterm = db.Query<Term>("SELECT * FROM Terms");
            foreach (Term term in tmpterm)
            {
                terms.Add(term);
            }

            foreach (Term term in terms)
            {
                var tmpcourses = db.Query<Course>($"SELECT * FROM Courses WHERE termId={term.termId}");
                List<Course> CourseList = new List<Course>();
                foreach (Course course in tmpcourses)
                {
                    CourseList.Add(course);
                    courseList.Add(course.courseId, course);
                }
                courses.Add(term, CourseList);
            }
            var tmpAssessment = db.Query<Assessment>("SELECT * FROM Assessments");
            foreach (Assessment assessment in tmpAssessment)
            {
                assessments.Add(assessment.assessmentId, assessment);
            }
            var tmpInstructor = db.Query<Instructor>("SELECT * FROM Instructors");
            foreach (Instructor instructor in tmpInstructor)
            {
                instructors.Add(instructor.instructorId, instructor);
            }
            var tmpNote = db.Query<Note>("SELECT * FROM Notes");
            foreach (Note note in tmpNote)
            {
                notes.Add(note.noteId, note);
            }
        }

        public static void sync_db()
        {
            terms = new List<Term>();
            courses = new Dictionary<Term, List<Course>>();
            courseList = new Dictionary<int, Course>();
            assessments = new Dictionary<int, Assessment>();
            instructors = new Dictionary<int, Instructor>();
            notes = new Dictionary<int, Note>();
            var db = new SQLiteConnection(databasePath);
            var tmpterm = db.Query<Term>("SELECT * FROM Terms");
            foreach (Term term in tmpterm)
            {
                terms.Add(term);
            }

            foreach (Term term in terms)
            {
                var tmpcourses = db.Query<Course>($"SELECT * FROM Courses WHERE termId={term.termId}");
                List<Course> CourseList = new List<Course>();
                foreach (Course course in tmpcourses)
                {
                    CourseList.Add(course);
                    courseList.Add(course.courseId, course);
                }
                courses.Add(term, CourseList);
            }
            var tmpAssessment = db.Query<Assessment>("SELECT * FROM Assessments");
            foreach (Assessment assessment in tmpAssessment)
            {
                assessments.Add(assessment.assessmentId, assessment);
            }
            var tmpInstructor = db.Query<Instructor>("SELECT * FROM Instructors");
            foreach (Instructor instructor in tmpInstructor)
            {
                instructors.Add(instructor.instructorId, instructor);
            }
            var tmpNote = db.Query<Note>("SELECT * FROM Notes");
            foreach (Note note in tmpNote)
            {
                notes.Add(note.noteId, note);
            }
        }



        public void load_ui(int term)
        {
            termStack.Children.Clear();
            courseStack.Children.Clear();
            termSelected = terms[term - 1];


            //Foreach Term
            foreach (Term tmpterm in terms)
            {
                Button button = new Button
                {
                    Text = tmpterm.termName,
                    Padding = 5,
                    BackgroundColor = Microsoft.Maui.Graphics.Colors.LightCoral,
                    TextColor = Microsoft.Maui.Graphics.Colors.Black,
                    CornerRadius = 5,
                };
                button.Clicked += void (sender, args) => load_ui(tmpterm.termId);
                termStack.Children.Add(button);
            }

            //Foreach Course in Term
            foreach (Course course in courses[termSelected])
            {
                //Add Course Button
                Button button = new Button
                {
                    Text = course.courseName
                };
                button.Clicked += async (sender, args) => await Navigation.PushAsync(new CourseView(course.courseId));
                courseStack.Children.Add(button);
            }
            termStart.Date = termSelected.start;
            termEnd.Date = termSelected.end;
            termTitle.Text = termSelected.termName;

        }
         public static bool checkDates(DateTime start, DateTime end)
        {
            if (end < start)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void termTitleChange(object sender, TextChangedEventArgs e)
        {
            var db = new SQLiteConnection(databasePath);

            if (e.NewTextValue != null)
            {
                termSelected.termName = e.NewTextValue;
                DataFunctions.updateTerm(db, termSelected);
                load_ui(termSelected.termId);
            }
        }

        private void termEnd_DateSelected(object sender, DateChangedEventArgs e)
        {
            var db = new SQLiteConnection(databasePath);
            bool valid = checkDates(termStart.Date, termEnd.Date);
            if (valid)
            {
                termEnd.Date = e.NewDate;
                termSelected.end = e.NewDate;
                DataFunctions.updateTerm(db, termSelected);
            }
        }
        private void termStart_DateSelected(object sender, DateChangedEventArgs e)
        {
            var db = new SQLiteConnection(databasePath);
            bool valid = checkDates(termStart.Date, termEnd.Date);
            if (valid)
            {
                termStart.Date = e.NewDate;
                termSelected.start = e.NewDate;
                DataFunctions.updateTerm(db, termSelected);
            }

        }
    }
}