namespace OOP4_2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lstDocuments = new ListBox();
            lstLog = new ListBox();
            txtSender = new TextBox();
            txtContent = new TextBox();
            cmbDocType = new ComboBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnView = new Button();
            btnApprove = new Button();
            btnReject = new Button();
            btnLogin = new Button();
            btnLogout = new Button();
            btnJournalData = new Button();
            btnHtml = new Button();
            btnText = new Button();
            btnSubscribe = new Button();
            btnUnsubscribe = new Button();
            lblDocType = new Label();
            lblSender = new Label();
            lblContent = new Label();
            lblDocuments = new Label();
            lblLog = new Label();
            lblUser = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            lblLogin = new Label();
            lblPassword = new Label();
            SuspendLayout();
            // 
            // lstDocuments
            // 
            lstDocuments.FormattingEnabled = true;
            lstDocuments.Location = new Point(340, 35);
            lstDocuments.Name = "lstDocuments";
            lstDocuments.Size = new Size(400, 409);
            lstDocuments.TabIndex = 21;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.Location = new Point(12, 545);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(728, 124);
            lstLog.TabIndex = 23;
            // 
            // txtSender
            // 
            txtSender.Location = new Point(120, 172);
            txtSender.Name = "txtSender";
            txtSender.Size = new Size(200, 23);
            txtSender.TabIndex = 10;
            // 
            // txtContent
            // 
            txtContent.Location = new Point(120, 202);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.ScrollBars = ScrollBars.Vertical;
            txtContent.Size = new Size(200, 80);
            txtContent.TabIndex = 12;
            // 
            // cmbDocType
            // 
            cmbDocType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDocType.Location = new Point(120, 142);
            cmbDocType.Name = "cmbDocType";
            cmbDocType.Size = new Size(200, 23);
            cmbDocType.TabIndex = 8;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 295);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 30);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(170, 295);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(150, 30);
            btnEdit.TabIndex = 14;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(12, 335);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 30);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnView
            // 
            btnView.Location = new Point(170, 335);
            btnView.Name = "btnView";
            btnView.Size = new Size(150, 30);
            btnView.TabIndex = 16;
            btnView.Text = "Просмотреть";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += btnView_Click;
            // 
            // btnApprove
            // 
            btnApprove.Location = new Point(12, 375);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(150, 30);
            btnApprove.TabIndex = 17;
            btnApprove.Text = "Утвердить";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.Location = new Point(170, 375);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(150, 30);
            btnReject.TabIndex = 18;
            btnReject.Text = "Отклонить";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(12, 100);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 30);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(170, 100);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(150, 30);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "Выйти из аккаунта";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnJournalData
            // 
            btnJournalData.Location = new Point(12, 415);
            btnJournalData.Name = "btnJournalData";
            btnJournalData.Size = new Size(308, 30);
            btnJournalData.TabIndex = 19;
            btnJournalData.Text = "Ввести данные о журнале";
            btnJournalData.UseVisualStyleBackColor = true;
            btnJournalData.Click += btnJournalData_Click;
            // 
            // btnHtml
            // 
            btnHtml.Location = new System.Drawing.Point(12, 455);
            btnHtml.Name = "btnHtml";
            btnHtml.Size = new System.Drawing.Size(150, 30);
            btnHtml.TabIndex = 24;
            btnHtml.Text = "Вывод HTML";
            btnHtml.UseVisualStyleBackColor = true;
            btnHtml.Click += new System.EventHandler(this.btnHtml_Click);
            // 
            // btnText
            // 
            btnText.Location = new System.Drawing.Point(170, 455);
            btnText.Name = "btnText";
            btnText.Size = new System.Drawing.Size(150, 30);
            btnText.TabIndex = 25;
            btnText.Text = "Вывод текстом";
            btnText.UseVisualStyleBackColor = true;
            btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // btnSubscribe
            // 
            btnSubscribe.Location = new System.Drawing.Point(12, 495);
            btnSubscribe.Name = "btnSubscribe";
            btnSubscribe.Size = new System.Drawing.Size(150, 30);
            btnSubscribe.TabIndex = 26;
            btnSubscribe.Text = "Подписаться";
            btnSubscribe.UseVisualStyleBackColor = true;
            btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // btnUnsubscribe
            // 
            btnUnsubscribe.Location = new System.Drawing.Point(170, 495);
            btnUnsubscribe.Name = "btnUnsubscribe";
            btnUnsubscribe.Size = new System.Drawing.Size(150, 30);
            btnUnsubscribe.TabIndex = 27;
            btnUnsubscribe.Text = "Отписаться";
            btnUnsubscribe.UseVisualStyleBackColor = true;
            btnUnsubscribe.Click += new System.EventHandler(this.btnUnsubscribe_Click);
            // 
            // lblDocType
            // 
            lblDocType.AutoSize = true;
            lblDocType.Location = new Point(12, 145);
            lblDocType.Name = "lblDocType";
            lblDocType.Size = new Size(92, 15);
            lblDocType.TabIndex = 7;
            lblDocType.Text = "Тип документа:";
            // 
            // lblSender
            // 
            lblSender.AutoSize = true;
            lblSender.Location = new Point(12, 175);
            lblSender.Name = "lblSender";
            lblSender.Size = new Size(81, 15);
            lblSender.TabIndex = 9;
            lblSender.Text = "Отправитель:";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(12, 205);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(79, 15);
            lblContent.TabIndex = 11;
            lblContent.Text = "Содержание:";
            // 
            // lblDocuments
            // 
            lblDocuments.AutoSize = true;
            lblDocuments.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDocuments.Location = new Point(340, 15);
            lblDocuments.Name = "lblDocuments";
            lblDocuments.Size = new Size(127, 15);
            lblDocuments.TabIndex = 20;
            lblDocuments.Text = "Журнал документов:";
            // 
            // lblLog
            // 
            lblLog.AutoSize = true;
            lblLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLog.Location = new Point(12, 530);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(109, 15);
            lblLog.TabIndex = 22;
            lblLog.Text = "Журнал событий:";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUser.Location = new Point(12, 9);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(185, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "Пользователь: не авторизован";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(100, 37);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(220, 23);
            txtLogin.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(100, 67);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(220, 23);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(12, 40);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(44, 15);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(12, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(52, 15);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Пароль:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 620);
            Controls.Add(lstLog);
            Controls.Add(lblLog);
            Controls.Add(lstDocuments);
            Controls.Add(lblDocuments);
            Controls.Add(btnJournalData);
            Controls.Add(btnReject);
            Controls.Add(btnApprove);
            Controls.Add(btnView);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(btnHtml);
            Controls.Add(btnText);
            Controls.Add(btnSubscribe);
            Controls.Add(btnUnsubscribe);
            Controls.Add(txtContent);
            Controls.Add(lblContent);
            Controls.Add(txtSender);
            Controls.Add(lblSender);
            Controls.Add(cmbDocType);
            Controls.Add(lblDocType);
            Controls.Add(btnLogout);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtLogin);
            Controls.Add(lblLogin);
            Controls.Add(lblUser);
            Font = new Font("Segoe UI", 9F);
            MaximumSize = new Size(776, 725);
            MinimumSize = new Size(776, 725);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Журнал регистрации корреспонденции";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox lstDocuments;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ComboBox cmbDocType;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnJournalData;
        private System.Windows.Forms.Button btnHtml;
        private System.Windows.Forms.Button btnText;
        private System.Windows.Forms.Button btnUnsubscribe;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Label lblDocType;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label lblDocuments;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblPassword;
    }
}