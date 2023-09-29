using SQLite;

namespace UniversityApp;

public partial class CourseView : ContentPage
{
	public Course currentCourse;
    public Instructor currentInstructor;
    public Assessment PA, OA;
    public SQLiteConnection db = new SQLiteConnection(MainPage.databasePath);
    public CourseView(int courseId)
	{
		InitializeComponent();
		Course course = MainPage.courseList[courseId];
		currentCourse = course;
		currentInstructor= MainPage.instructors[course.instructorId];
		PA = MainPage.assessments[course.pa];
		OA = MainPage.assessments[course.oa];
		courseTitle.Text = course.courseName;
		courseStart.Date = course.start;
		courseEnd.Date = course.end;
		courseStatus.ItemsSource = MainPage.statusValues;
		courseStatus.SelectedItem = course.status;
        courseStartNotif.ItemsSource = MainPage.notificationValues;
        courseStartNotif.SelectedItem = course.startNotification;
        courseEndNotif.ItemsSource = MainPage.notificationValues;
        courseEndNotif.SelectedItem = course.endNotification;
        paEndNotif.ItemsSource = MainPage.notificationValues;
        oaEndNotif.ItemsSource = MainPage.notificationValues;
        paStartNotif.ItemsSource = MainPage.notificationValues;
        oaStartNotif.ItemsSource = MainPage.notificationValues;
        oaStart.Date = OA.start;
        oaEnd.Date = OA.end;
        paStart.Date = PA.start;
        oaEnd.Date = PA.end;
        oaDueDate.Date = OA.dueDate;
        paDueDate.Date = PA.dueDate;
        paEndNotif.SelectedItem = PA.endNotif;
        paStartNotif.SelectedItem = PA.startNotif;
        oaEndNotif.SelectedItem = OA.endNotif;
        oaStartNotif.SelectedItem = OA.startNotif;
        instructorName.Text = currentInstructor.instructorName;
		instructorPhone.Text = currentInstructor.instructorPhone;
		instructorEmail.Text = currentInstructor.instructorEmail;
		paName.Text = PA.assessmentName;
		oaName.Text = OA.assessmentName;
        courseDetails.Text = course.courseDetails;

        populateNotes();

    }
    public async Task ShareText(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest { Text = text });
    }

    private void populateNotes()
    {
        noteStack.Children.Clear();

        foreach (Note note in MainPage.notes.Values)
        {
            if (note.courseId == currentCourse.courseId)
            {
                SwipeItem shareItem = new SwipeItem
                {
                    Text = "Share",
                    BindingContext = note,
                    BackgroundColor = Colors.LightBlue
                };
                SwipeItem deleteItem = new SwipeItem
                {
                    Text = "Delete",
                    BindingContext = note,
                    BackgroundColor = Colors.LightCoral
                };
                shareItem.Invoked += onShareInvoked;
                deleteItem.Invoked += onDeleteInvoked;
                List<SwipeItem> items = new List<SwipeItem>() { shareItem, deleteItem };
                Grid grid = new Grid
                {
                    BackgroundColor = Colors.LightCoral
                };
                grid.Add(new Label
                {
                    Text = note.content
                });
                SwipeView swp = new SwipeView
                {
                    RightItems = new SwipeItems(items),
                    Content = grid
                };
                noteStack.Add(swp);
                
            }
        }
    }

    private void onDeleteInvoked(object sender, EventArgs e)
    {
       var item = sender as SwipeItem;
       var note = item.BindingContext as Note;
        DataFunctions.deleteNote(note);
        MainPage.sync_db();
        populateNotes();
    }

    private async void onShareInvoked(object sender, EventArgs e)
    {
        var item = sender as SwipeItem;
        var note = item.BindingContext as Note;
        await ShareText(note.content);
        MainPage.sync_db();
    }

    private void courseStart_DateSelected(object sender, DateChangedEventArgs e)
    {
        bool valid = MainPage.checkDates(courseStart.Date, courseEnd.Date);
		if (valid)
		{
            currentCourse.start = e.NewDate;
			DataFunctions.updateCourse(db, currentCourse);
            MainPage.sync_db();
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
            MainPage.sync_db();
        }
    }

    private void courseStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        currentCourse.status = courseStatus.SelectedItem as string;
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();
    }

    private void courseTitle_TextChanged(object sender, TextChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        currentCourse.courseName = courseTitle.Text;
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();
    }

    private void instructorEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        if (e.NewTextValue != null)
        {
            currentInstructor.instructorEmail = e.NewTextValue;
            DataFunctions.updateInstructor(db, currentInstructor);
            MainPage.sync_db();
        }
    }

    private void instructorPhone_TextChanged(object sender, TextChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        if (e.NewTextValue != null)
        {
            currentInstructor.instructorPhone = e.NewTextValue;
            DataFunctions.updateInstructor(db, currentInstructor);
            MainPage.sync_db();
        }
    }

    private void instructorName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        if (e.NewTextValue != null)
        {
            currentInstructor.instructorName = e.NewTextValue;
            DataFunctions.updateInstructor(db, currentInstructor);
            MainPage.sync_db();
        }
    }

    private void addNote(object sender, EventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        if (noteInput.Text != null)
        {
            Note note = new Note(currentCourse.courseId, noteInput.Text);
            DataFunctions.addNote(db, note);
            MainPage.sync_db();
        }
        noteInput.Text = "";
        populateNotes();
    }

    private void courseStartNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentCourse.startNotification = Convert.ToInt32(courseStartNotif.SelectedItem);
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();
        
    }
    private void courseEndNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentCourse.endNotification = Convert.ToInt32(courseEndNotif.SelectedItem);
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();

    }

    private void paStartNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        PA.startNotif = Convert.ToInt32(paStartNotif.SelectedItem);
        DataFunctions.updateAssessment(db, PA);
        MainPage.sync_db();

    }

    private void paEndNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        PA.endNotif = Convert.ToInt32(paEndNotif.SelectedItem);
        DataFunctions.updateAssessment(db, PA);
        MainPage.sync_db();


    }

    private void oaStartNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        OA.startNotif = Convert.ToInt32(oaStartNotif.SelectedItem);
        DataFunctions.updateAssessment(db, OA);
        MainPage.sync_db();

    }

    private void courseDetails_TextChanged(object sender, TextChangedEventArgs e)
    {
        currentCourse.courseDetails = courseDetails.Text;
        DataFunctions.updateCourse(db, currentCourse);
        MainPage.sync_db();
        
    }

    private void oaName_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null)
        {
            OA.assessmentName = e.NewTextValue;
            DataFunctions.updateAssessment(db, OA);
            MainPage.sync_db();
        }
        
    }

    private void paName_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null)
        {
            PA.assessmentName = e.NewTextValue;
            DataFunctions.updateAssessment(db, PA);
            MainPage.sync_db();
        }
    }

    private void oaDueDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        OA.dueDate = e.NewDate;
        db.Update(OA);
        MainPage.sync_db();
    }

    private void paDueDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        PA.dueDate = e.NewDate;
        db.Update(PA);
        MainPage.sync_db();
    }

    private void oaStart_DateSelected(object sender, DateChangedEventArgs e)
    {
        OA.start = e.NewDate;
        db.Update(OA);
        MainPage.sync_db();
    }

    private void oaEnd_DateSelected(object sender, DateChangedEventArgs e)
    {
        OA.end = e.NewDate;
        db.Update(OA);
        MainPage.sync_db();
    }

    private void paStart_DateSelected(object sender, DateChangedEventArgs e)
    {
        PA.start = e.NewDate;
        db.Update(PA);
        MainPage.sync_db();
    }

    private void paEnd_DateSelected(object sender, DateChangedEventArgs e)
    {
        PA.end = e.NewDate;
        db.Update(PA);
        MainPage.sync_db();
    }

    private void oaEndNotif_SelectedIndexChanged(object sender, EventArgs e)
    {
        var db = new SQLiteConnection(MainPage.databasePath);
        OA.endNotif = Convert.ToInt32(oaEndNotif.SelectedItem);
        DataFunctions.updateAssessment(db, OA);
        MainPage.sync_db();

    }
}