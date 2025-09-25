using System;
using System.Drawing;
using System.Windows.Forms;

public class Program : Form
{
    private MonthCalendar monthCalendar1;
    private TextBox textBox1;
    private Button resetButton;
    private Button todayButton;
    private CheckBox toggleWeekNum;
    private Label daysCountLabel;
    private Button nextHolidayButton;

    [STAThread]
    static void Main()
    {
        Application.Run(new Program());
    }

    public Program()
    {
        // ��������� �����
        this.Text = "������ MonthCalendar � ������ ���������";
        this.ClientSize = new Size(920, 450);

        // ���� ��� ������ ��������� ���
        textBox1 = new TextBox
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(48, 300),
            Multiline = true,
            ReadOnly = true,
            Size = new Size(824, 32)
        };

        // ���������
        monthCalendar1 = new MonthCalendar
        {
            Location = new Point(50, 20),
            CalendarDimensions = new Size(2, 1),
            FirstDayOfWeek = Day.Monday,
            MaxSelectionCount = 21,
            ScrollChange = 1,
            ShowToday = false,
            ShowTodayCircle = false,
            ShowWeekNumbers = true
        };
        monthCalendar1.DateSelected += MonthCalendar1_DateSelected;
        monthCalendar1.DateChanged += MonthCalendar1_DateChanged;

        // ������ ������ ������ ���
        resetButton = new Button
        {
            Text = "�������� �����",
            Location = new Point(50, 350),
            Size = new Size(120, 30)
        };
        resetButton.Click += (s, e) =>
        {
            monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            textBox1.Text = "����� ��� �������.";
            UpdateDaysCount();
        };

        // ������ �������� �� ����������� ����
        todayButton = new Button
        {
            Text = "�������",
            Location = new Point(190, 350),
            Size = new Size(120, 30)
        };
        todayButton.Click += (s, e) =>
        {
            monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            textBox1.Text = "������� �� ����������� ����.";
            UpdateDaysCount();
        };

        // ������� ��� ����������� ������� ������
        toggleWeekNum = new CheckBox
        {
            Text = "���������� ������ ������",
            Checked = true,
            Location = new Point(330, 355),
            Size = new Size(180, 20)
        };
        toggleWeekNum.CheckedChanged += (s, e) =>
        {
            monthCalendar1.ShowWeekNumbers = toggleWeekNum.Checked;
        };

        // ����� � ����������� ��������� ����
        daysCountLabel = new Label
        {
            Location = new Point(520, 355),
            Size = new Size(200, 20)
        };
        UpdateDaysCount();

        // ������ ��� �������� � ���������� ���������
        nextHolidayButton = new Button
        {
            Text = "��������� ��������",
            Location = new Point(740, 350),
            Size = new Size(130, 30)
        };
        nextHolidayButton.Click += (s, e) =>
        {
            DateTime[] holidays = new DateTime[]
            {
                new DateTime(DateTime.Today.Year, 1, 1),
                new DateTime(DateTime.Today.Year, 2, 14),
                new DateTime(DateTime.Today.Year, 7, 4),
                new DateTime(DateTime.Today.Year, 12, 25)
            };

            foreach (var d in holidays)
            {
                if (d >= DateTime.Today)
                {
                    monthCalendar1.SetSelectionRange(d, d);
                    textBox1.Text = "��������� ��������: " + d.ToShortDateString();
                    UpdateDaysCount();
                    break;
                }
            }
        };

        // ��������� ��� �������� �� �����
        this.Controls.AddRange(new Control[]
        {
            textBox1,
            monthCalendar1,
            resetButton,
            todayButton,
            toggleWeekNum,
            daysCountLabel,
            nextHolidayButton
        });
    }

    // ���������� ���������� ��������� ����
    private void UpdateDaysCount()
    {
        int days = (monthCalendar1.SelectionEnd - monthCalendar1.SelectionStart).Days + 1;
        daysCountLabel.Text = "������� ����: " + days;
    }

    // ���������� ������ ��� ������ ��� �������������
    private void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
    {
        textBox1.Text = "�������: � " + e.Start.ToShortDateString() + " �� " + e.End.ToShortDateString();
        UpdateDaysCount();
    }

    // ���������� ������ ��� ��������� ��������� ���
    private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
        textBox1.Text = "���� ��������: � " + e.Start.ToShortDateString() + " �� " + e.End.ToShortDateString();
        UpdateDaysCount();
    }
}
