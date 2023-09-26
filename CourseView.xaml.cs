namespace UniversityApp;

public partial class CourseView : ContentPage
{
	public CourseView(int courseId)
	{
		InitializeComponent();
		Course course = MainPage.courseList[courseId];
		Instructor instructor = MainPage.instructors[course.instructorId];
		Assessment PA = MainPage.assessments[course.pa];
		Assessment OA = MainPage.assessments[course.oa];
		courseTitle.Text = course.courseName;
		courseStart.Date = course.start;
		courseEnd.Date = course.end;
        MainPage.statusValues.Add("In Progress");
        MainPage.statusValues.Add("Completed");
        MainPage.statusValues.Add("Dropped");
        MainPage.statusValues.Add("Plan to Take");
		courseStatus.ItemsSource = MainPage.statusValues;
		courseStatus.SelectedItem = course.status;
		instructorName.Text = instructor.instructorName;
		instructorPhone.Text = instructor.instructorPhone;
		instructorEmail.Text = instructor.instructorEmail;
		paName.Text = PA.assessmentName;
		oaName.Text = OA.assessmentName;

    }
}