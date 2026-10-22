using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP4_2
{
    public partial class Form1 : Form
    {
        private Journal _journal = new Journal();
        private AuthService _auth = new AuthService();
        private User _currentUser = null;
        private int _docCounter = 0;

        public Form1()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            _auth.addUser(new User
            {
                id = 1,
                firstName = "Иван",
                surName = "Иванов",
                login = "secretary",
                passw = "123",
                role = UserRole.SECRETARY
            });

            _auth.addUser(new User
            {
                id = 2,
                firstName = "Пётр",
                surName = "Петров",
                login = "director",
                passw = "123",
                role = UserRole.DIRECTOR
            });

            cmbDocType.Items.AddRange(new[] { "Письмо", "Приказ", "Постановление", "Жалоба" });
            cmbDocType.SelectedIndex = 0;

            lblUser.Text = "Пользователь: не авторизован";
        }
        private void Log(string msg)
        {
            lstLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {msg}");
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private bool IsAuthorized()
        {
            if (_currentUser == null)
            {
                Log("Ошибка: сначала войдите!");
                return false;
            }
            return true;
        }

        private void RefreshList()
        {
            lstDocuments.Items.Clear();
            foreach (var d in _journal.viewJournal())
                lstDocuments.Items.Add(d);
        }
        private void btnHtml_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            _journal.setFormatter(new HtmlFormatter());
            string result = _journal.outputAll();
            ShowOutput("Журнал (HTML)", result);
            Log("Вывод в формате HTML");
        }
        private void btnText_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            _journal.setFormatter(new TextFormatter());
            string result = _journal.outputAll();
            ShowOutput("Журнал (Текст)", result);
            Log("Вывод в формате текста");
        }
        private void ShowOutput(string title, string content)
        {
            using (var form = new Form())
            {
                form.Text = title;
                form.Size = new Size(600, 400);
                form.StartPosition = FormStartPosition.CenterParent;
                var txt = new TextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Both,
                    Dock = DockStyle.Fill,
                    Font = new Font("Consolas", 10),
                    Text = content,
                    ReadOnly = true
                };
                form.Controls.Add(txt);
                form.ShowDialog(this);
            }
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            _currentUser.OnReceiveCorrespondence = (msg) => Log(msg);
            _journal.attach(_currentUser);
            Log($"Пользователь {_currentUser.firstName} подписан на уведомления");
        }
        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            _journal.detach(_currentUser);
            Log($"Пользователь {_currentUser.firstName} отписан от уведомлений");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_currentUser != null)
            {
                Log("Вы уже авторизованы");
                return;
            }
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                Log("Ошибка: введите логин и пароль");
                return;
            }
            var user = _auth.findUserByLogin(login);
            if (user == null)
            {
                Log("Ошибка: пользователь не найден");
                return;
            }
            if (!_auth.checkPassword(user, password))
            {
                Log("Ошибка: неверный пароль");
                return;
            }
            _currentUser = user;
            lblUser.Text = $"Пользователь: {_currentUser.firstName} {_currentUser.surName} ({_currentUser.role})";
            Log($"Вход выполнен: {_currentUser.name} ({_currentUser.role})");
            txtLogin.Clear();
            txtPassword.Clear();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                Log("Ошибка: вы не авторизованы");
                return;
            }
            Log($"Выход: {_currentUser.name}");
            _currentUser = null;
            lblUser.Text = "Пользователь: не авторизован";
            txtLogin.Clear();
            txtPassword.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            Document doc = null;
            _docCounter++;
            switch (cmbDocType.SelectedItem.ToString())
            {
                case "Письмо": doc = new Letter(); break;
                case "Приказ": doc = new Order(); break;
                case "Постановление": doc = new Resolution(); break;
                case "Жалоба": doc = new Complaint(); break;
            }
            doc.id = _docCounter;
            doc.sender = txtSender.Text;
            doc.content = txtContent.Text;
            _journal.addDoc(doc);
            RefreshList();
            Log($"Добавлен: {doc.name}");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            if (lstDocuments.SelectedItem == null) { Log("Выберите документ"); return; }
            var doc = (Document)lstDocuments.SelectedItem;
            using (var editForm = new Form())
            {
                editForm.Text = $"Редактирование: {doc.name}";
                editForm.Size = new Size(420, 400);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.MaximizeBox = false;
                editForm.MinimizeBox = false;
                var lblSenderEdit = new Label { Text = "Отправитель:", Location = new Point(15, 20), Size = new Size(110, 20) };
                var txtSenderEdit = new TextBox { Text = doc.sender, Location = new Point(130, 17), Size = new Size(250, 23) };
                var lblContentEdit = new Label { Text = "Содержание:", Location = new Point(15, 55), Size = new Size(110, 20) };
                var txtContentEdit = new TextBox
                {
                    Text = doc.content,
                    Location = new Point(130, 52),
                    Size = new Size(250, 60),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical
                };
                var lblStatusEdit = new Label { Text = "Статус:", Location = new Point(15, 125), Size = new Size(110, 20) };
                var txtStatusEdit = new TextBox
                {
                    Text = doc.status.ToString(),
                    Location = new Point(130, 122),
                    Size = new Size(250, 23),
                    ReadOnly = true,
                    BackColor = SystemColors.Control
                };
                editForm.Controls.Add(lblSenderEdit);
                editForm.Controls.Add(txtSenderEdit);
                editForm.Controls.Add(lblContentEdit);
                editForm.Controls.Add(txtContentEdit);
                editForm.Controls.Add(lblStatusEdit);
                editForm.Controls.Add(txtStatusEdit);
                TextBox txtExtra1 = null;
                TextBox txtExtra2 = null;
                int y = 160;
                if (doc is Letter letter)
                {
                    var lbl1 = new Label { Text = "Город:", Location = new Point(15, y), Size = new Size(110, 20) };
                    txtExtra1 = new TextBox { Text = letter.city, Location = new Point(130, y - 3), Size = new Size(250, 23) };

                    var lbl2 = new Label { Text = "Индекс:", Location = new Point(15, y + 30), Size = new Size(110, 20) };
                    txtExtra2 = new TextBox { Text = letter.postIndex, Location = new Point(130, y + 27), Size = new Size(250, 23) };

                    editForm.Controls.Add(lbl1); editForm.Controls.Add(txtExtra1);
                    editForm.Controls.Add(lbl2); editForm.Controls.Add(txtExtra2);
                }
                else if (doc is Order order)
                {
                    var lbl1 = new Label { Text = "Подразделение:", Location = new Point(15, y), Size = new Size(110, 20) };
                    txtExtra1 = new TextBox { Text = order.department, Location = new Point(130, y - 3), Size = new Size(250, 23) };

                    var lbl2 = new Label { Text = "Номер приказа:", Location = new Point(15, y + 30), Size = new Size(110, 20) };
                    txtExtra2 = new TextBox { Text = order.number, Location = new Point(130, y + 27), Size = new Size(250, 23) };

                    editForm.Controls.Add(lbl1); editForm.Controls.Add(txtExtra1);
                    editForm.Controls.Add(lbl2); editForm.Controls.Add(txtExtra2);
                }
                else if (doc is Resolution res)
                {
                    var lbl1 = new Label { Text = "Название:", Location = new Point(15, y), Size = new Size(110, 20) };
                    txtExtra1 = new TextBox { Text = res.rName, Location = new Point(130, y - 3), Size = new Size(250, 23) };

                    var lbl2 = new Label { Text = "Срок (дд.мм.гггг):", Location = new Point(15, y + 30), Size = new Size(110, 20) };
                    txtExtra2 = new TextBox { Text = res.deadline.ToString("dd.MM.yyyy"), Location = new Point(130, y + 27), Size = new Size(250, 23) };

                    editForm.Controls.Add(lbl1); editForm.Controls.Add(txtExtra1);
                    editForm.Controls.Add(lbl2); editForm.Controls.Add(txtExtra2);
                }
                else if (doc is Complaint cmp)
                {
                    var lbl1 = new Label { Text = "Заявитель:", Location = new Point(15, y), Size = new Size(110, 20) };
                    txtExtra1 = new TextBox { Text = cmp.applicantName, Location = new Point(130, y - 3), Size = new Size(250, 23) };

                    var lbl2 = new Label { Text = "ID жалобы:", Location = new Point(15, y + 30), Size = new Size(110, 20) };
                    txtExtra2 = new TextBox { Text = cmp.cmpId.ToString(), Location = new Point(130, y + 27), Size = new Size(250, 23) };

                    editForm.Controls.Add(lbl1); editForm.Controls.Add(txtExtra1);
                    editForm.Controls.Add(lbl2); editForm.Controls.Add(txtExtra2);
                }

                var btnOk = new Button
                {
                    Text = "Сохранить",
                    Location = new Point(130, y + 80),
                    Size = new Size(120, 30),
                    DialogResult = DialogResult.OK
                };
                var btnCancel = new Button
                {
                    Text = "Отмена",
                    Location = new Point(260, y + 80),
                    Size = new Size(120, 30),
                    DialogResult = DialogResult.Cancel
                };
                editForm.Controls.Add(btnOk);
                editForm.Controls.Add(btnCancel);
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    doc.sender = txtSenderEdit.Text;
                    doc.editDoc(txtContentEdit.Text);

                    if (doc is Letter l2)
                    {
                        l2.setCity(txtExtra1.Text);
                        l2.setPostIndex(txtExtra2.Text);
                    }
                    else if (doc is Order o2)
                    {
                        o2.setDepartment(txtExtra1.Text);
                        o2.setNumber(txtExtra2.Text);
                    }
                    else if (doc is Resolution r2)
                    {
                        r2.rName = txtExtra1.Text;
                        if (DateTime.TryParse(txtExtra2.Text, out var dt))
                            r2.setDeadline(dt);
                        else
                            Log("Предупреждение: неверный формат даты");
                    }
                    else if (doc is Complaint c2)
                    {
                        c2.setApplicant(txtExtra1.Text);
                        if (int.TryParse(txtExtra2.Text, out var cid))
                            c2.cmpId = cid;
                        else
                            Log("Предупреждение: неверный ID жалобы");
                    }

                    RefreshList();
                    Log($"Отредактирован: {doc.name}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            if (lstDocuments.SelectedItem == null) { Log("Выберите документ"); return; }
            var doc = (Document)lstDocuments.SelectedItem;
            var result = MessageBox.Show(
                $"Удалить документ?\n\n{doc.name}",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            _journal.removeDoc(doc);
            RefreshList();
            Log($"Удалён: {doc.name}");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            if (lstDocuments.SelectedItem == null) { Log("Выберите документ"); return; }

            var doc = (Document)lstDocuments.SelectedItem;
            MessageBox.Show(doc.viewDoc(), $"Просмотр: {doc.name}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            if (lstDocuments.SelectedItem == null) { Log("Выберите документ"); return; }
            try
            {
                var doc = (Document)lstDocuments.SelectedItem;
                doc.approveBy(_currentUser);
                RefreshList();
                Log($"Утверждён: {doc.name}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Log($"Ошибка: {ex.Message}");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;
            if (lstDocuments.SelectedItem == null) { Log("Выберите документ"); return; }
            try
            {
                var doc = (Document)lstDocuments.SelectedItem;
                _currentUser.reject(doc, "Не соответствует требованиям");
                RefreshList();
                Log($"Отклонён: {doc.name}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Log($"Ошибка: {ex.Message}");
            }
        }

        private void btnJournalData_Click(object sender, EventArgs e)
        {
            if (!IsAuthorized()) return;

            if (_currentUser.role != UserRole.SECRETARY)
            {
                Log("Ошибка: только секретарь может вводить данные о журнале!");
                return;
            }
            using (var form = new Form())
            {
                form.Text = "Данные о журнале";
                form.Size = new Size(420, 280);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                var lblId = new Label { Text = "ID журнала:", Location = new Point(15, 20), Size = new Size(120, 20) };
                var txtId = new TextBox { Text = _journal.id.ToString(), Location = new Point(140, 17), Size = new Size(240, 23) };
                var lblName = new Label { Text = "Название:", Location = new Point(15, 55), Size = new Size(120, 20) };
                var txtName = new TextBox { Text = _journal.name, Location = new Point(140, 52), Size = new Size(240, 23) };
                var lblStart = new Label { Text = "Дата начала:", Location = new Point(15, 90), Size = new Size(120, 20) };
                var txtStart = new TextBox { Text = _journal.startDate.ToString("dd.MM.yyyy"), Location = new Point(140, 87), Size = new Size(240, 23) };
                var lblEnd = new Label { Text = "Дата окончания:", Location = new Point(15, 125), Size = new Size(120, 20) };
                var txtEnd = new TextBox { Text = _journal.endDate.ToString("dd.MM.yyyy"), Location = new Point(140, 122), Size = new Size(240, 23) };
                var btnOk = new Button { Text = "Сохранить", Location = new Point(140, 170), Size = new Size(110, 30), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Отмена", Location = new Point(260, 170), Size = new Size(110, 30), DialogResult = DialogResult.Cancel };
                form.Controls.Add(lblId); form.Controls.Add(txtId);
                form.Controls.Add(lblName); form.Controls.Add(txtName);
                form.Controls.Add(lblStart); form.Controls.Add(txtStart);
                form.Controls.Add(lblEnd); form.Controls.Add(txtEnd);
                form.Controls.Add(btnOk); form.Controls.Add(btnCancel);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (!int.TryParse(txtId.Text, out var newId))
                    {
                        Log("Ошибка: ID должен быть числом");
                        return;
                    }
                    if (!DateTime.TryParse(txtStart.Text, out var start))
                    {
                        Log("Ошибка: неверный формат даты начала");
                        return;
                    }
                    if (!DateTime.TryParse(txtEnd.Text, out var end))
                    {
                        Log("Ошибка: неверный формат даты окончания");
                        return;
                    }
                    if (end < start)
                    {
                        Log("Ошибка: дата окончания раньше даты начала");
                        return;
                    }
                    _journal.id = newId;
                    _journal.name = txtName.Text;
                    _journal.startDate = start;
                    _journal.endDate = end;
                    Log($"Данные журнала обновлены: ID={_journal.id}, \"{_journal.name}\", {_journal.startDate:dd.MM.yyyy}–{_journal.endDate:dd.MM.yyyy}");
                }
            }
        }
    }
}