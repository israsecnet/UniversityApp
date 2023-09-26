using SQLite;

namespace UniversityApp;

public partial class CourseView : ContentPage
{
	public Course currentCourse;
	public CourseView(int courseId)
	{
		InitializeComponent();
		Course course = MainPage.courseList[courseId];
		currentCourse = course;
		Instructor instructor = MainPage.instructors[course.instructorId];
		Assessment PA = MainPage.assessments[course.pa];
		Assessment OA = MainPage.assessments[course.oa];
		courseTitle.Text = course.courseName;
		courseStart.Date = course.start;
		courseEnd.Date = course.end;
		courseStatus.ItemsSource = MainPage.statusValues;
		courseStatus.SelectedItem = course.status;
		instructorName.Text = instructor.instructorName;
		instructorPhone.Text = instructor.instructorPhone;
		instructorEmail.Text = instructor.instructorEmail;
		paName.Text = PA.assessmentName;
		oaName.Text = OA.assessmentName;

    }

    private void courseStart_DateSelected(object sender, DateChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        bool valid = MainPage.checkDates(courseStart.Date, courseEnd.Date);
		if (valid)
		{
            currentCourse.start = e.NewDate;
			DataFunctions.updateCourse(db, currentCourse);
        }	
		
    }

    private void courseEnd_DateSelected(object sender, DateChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        bool valid = MainPage.checkDates(courseStart.Date, courseEnd.Date);
        if (valid)
        {
            currentCourse.end = e.NewDate;
            DataFunctions.updateCourse(db, currentCourse);
        }
    }

    private void courseStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        currentCourse.status = courseStatus.SelectedItem as string;
        DataFunctions.updateCourse(db, currentCourse);
    }

    private void courseTitle_TextChanged(object sender, TextChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        currentCourse.courseName = courseTitle.Text;
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();
    }
}