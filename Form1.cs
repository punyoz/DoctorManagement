namespace DoctorManagement
{
    public partial class Form1 : Form
    {
        private List<Appointment> appointments = new List<Appointment>
        {
            new Appointment { AppointmentID = 1, PatientName = "John Doe", AppointmentDate = DateTime.Now.AddDays(2), Status = "Scheduled" },
            new Appointment { AppointmentID = 2, PatientName = "Jane Smith", AppointmentDate = DateTime.Now.AddDays(-1), Status = "Completed" },
            new Appointment { AppointmentID = 3, PatientName = "Mike Johnson", AppointmentDate = DateTime.Now.AddDays(5), Status = "Scheduled" },
            new Appointment { AppointmentID = 4, PatientName = "Emily Davis", AppointmentDate = DateTime.Now.AddDays(-3), Status = "No Show" }
        };

        private List<Availability> availabilityList = new List<Availability>();

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadAppointments();
            PopulateStatusComboBox();
        }

        private void SetupDataGridView()
        {
            guna2DataGridView1.Columns.Clear();

            guna2DataGridView1.Columns.Add("AppointmentID", "Appointment ID");
            guna2DataGridView1.Columns.Add("PatientName", "Patient Name");
            guna2DataGridView1.Columns.Add("AppointmentDate", "Appointment Date");
            guna2DataGridView1.Columns.Add("Category", "Category");
            guna2DataGridView1.Columns.Add("Status", "Status");
        }
        private void LoadAppointments()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (var appointment in appointments)
            {
                string category = appointment.AppointmentDate > DateTime.Now ? "Upcoming" : "Past";
                guna2DataGridView1.Rows.Add(
                    appointment.AppointmentID,
                    appointment.PatientName,
                    appointment.AppointmentDate.ToString("yyyy-MM-dd HH:mm"),
                    category,
                    appointment.Status
                );
            }
        }
        private void PopulateStatusComboBox()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Completed");
            guna2ComboBox1.Items.Add("Cancelled");
            guna2ComboBox1.Items.Add("No Show");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a status to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedRowIndex = guna2DataGridView1.SelectedRows[0].Index;
            if (int.TryParse(guna2DataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString(), out int appointmentID))
            {
                var appointment = appointments.FirstOrDefault(a => a.AppointmentID == appointmentID);

                if (appointment != null)
                {
                    appointment.Status = guna2ComboBox1.SelectedItem.ToString();
                    LoadAppointments();
                    MessageBox.Show($"Status updated to '{appointment.Status}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The selected Appointment ID is not a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadAvailability()
        {
            guna2DataGridView1.Rows.Clear();

            foreach (var availability in availabilityList)
            {
                guna2DataGridView1.Rows.Add(
                    availability.Date.ToString("yyyy-MM-dd"),
                    availability.Status
                );
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = guna2DateTimePicker1.Value.Date;
            string status = Save.Checked ? "Available" : "Not Available";

            var existingAvailability = availabilityList.FirstOrDefault(a => a.Date == selectedDate);

            if (existingAvailability != null)
            {
                existingAvailability.Status = status;
                MessageBox.Show($"Availability updated for {selectedDate.ToShortDateString()}.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                availabilityList.Add(new Availability
                {
                    Date = selectedDate,
                    Status = status
                });
                MessageBox.Show($"Availability set for {selectedDate.ToShortDateString()}.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadAvailability();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
