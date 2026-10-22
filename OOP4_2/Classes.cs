using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP4_2
{
    public enum DocumentStatus
    {
        REGISTERED,
        APPROVED,
        REJECTED,
        ARCHIVED
    }

    public enum UserRole
    {
        SECRETARY,
        DIRECTOR
    }

    public interface IObserver
    {
        void update(Document doc);
    }
    public interface IObservable
    {
        void attach(IObserver observer);
        void detach(IObserver observer);
        void notifyObservers(Document doc);
    }

    public interface IDocumentFormatter
    {
        string format(Document doc);
    }

    public class HtmlFormatter : IDocumentFormatter
    {
        public string format(Document doc)
        {
            return $"<p>{doc.name} | {doc.regNumber} | {doc.status}</p>\n";
        }
    }

    public class TextFormatter : IDocumentFormatter
    {
        public string format(Document doc)
        {
            return $"{doc.name} | {doc.regNumber} | {doc.status}{Environment.NewLine}";
        }
    }

    public interface IApprover
    {
        void approve(Document doc);
        void reject(Document doc, string reason);
    }

    public abstract class Document
    {
        public int id { get; set; }
        public string regNumber { get; set; }
        public DateTime regDate { get; set; }
        public string sender { get; set; }
        public string content { get; set; }
        public DocumentStatus status { get; set; }

        public string name { get; set; }
        private static int _counter = 0;

        protected Document()
        {
            _counter++;
            name = $"{GetType().Name} №{_counter}";
        }

        public virtual void addDoc()
        {
            status = DocumentStatus.REGISTERED;
        }

        public virtual void editDoc(string newContent) { content = newContent; }
        public virtual void delDoc() { status = DocumentStatus.ARCHIVED; }

        public virtual string viewDoc()
        {
            return $"=== {name} ===\n" +
                   $"ID: {id}\n" +
                   $"Рег. номер: {regNumber}\n" +
                   $"Дата регистрации: {regDate:dd.MM.yyyy HH:mm}\n" +
                   $"Отправитель: {sender}\n" +
                   $"Содержание: {content}\n" +
                   $"Статус: {status}";
        }

        public void approveBy(IApprover approver)
        {
            approver.approve(this);
        }

        public override string ToString() => $"{name} ({regNumber})";
    }

    public class Letter : Document
    {
        public string city { get; set; }
        public string postIndex { get; set; }
        public void setCity(string city) { this.city = city; }
        public void setPostIndex(string index) { this.postIndex = index; }
        public override string viewDoc()
        {
            return base.viewDoc() +
                   $"\nГород: {city}\n" +
                   $"Почтовый индекс: {postIndex}";
        }
    }

    public class Order : Document
    {
        public string department { get; set; }
        public string number { get; set; }
        public void setDepartment(string dep) { department = dep; }
        public void setNumber(string num) { number = num; }
        public override string viewDoc()
        {
            return base.viewDoc() +
                   $"\nПодразделение: {department}\n" +
                   $"Номер приказа: {number}";
        }
    }

    public class Resolution : Document
    {
        public string rName { get; set; }
        public DateTime deadline { get; set; }
        public void setDeadline(DateTime date) { deadline = date; }
        public void extendDeadline(DateTime newDate) { deadline = newDate; }
        public override string viewDoc()
        {
            return base.viewDoc() +
                   $"\nНазвание: {rName}\n" +
                   $"Срок исполнения: {deadline:dd.MM.yyyy}";
        }
    }

    public class Complaint : Document
    {
        public string applicantName { get; set; }
        public int cmpId { get; set; }
        public void setApplicant(string name) { applicantName = name; }
        public void closeComplaint(string result) { content += $"\n[Закрыто]: {result}"; }
        public override string viewDoc()
        {
            return base.viewDoc() +
                   $"\nЗаявитель: {applicantName}\n" +
                   $"ID жалобы: {cmpId}";
        }
    }

    public class Journal: IObservable
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<Document> docs { get; set; } = new List<Document>();
        private List<IObserver> _observers = new List<IObserver>();
        private IDocumentFormatter _formatter;
        private static int _counter = 0;

        public Journal()
        {
            _counter++;
            name = $"Journal №{_counter}";
            startDate = DateTime.Now;
            endDate = DateTime.Now.AddYears(1);
            _formatter = new TextFormatter();
        }

        public void setFormatter(IDocumentFormatter formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter), "Стратегия не может быть null");
            _formatter = formatter;
        }
        public string outputAll()
        {
            if (docs.Count == 0)
                return "Журнал пуст.";

            var sb = new StringBuilder();
            foreach (var doc in docs)
            {
                sb.Append(_formatter.format(doc));
            }
            return sb.ToString();
        }

        public void attach(IObserver observer)
        {
            if (observer != null && !_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void detach(IObserver observer)
        {
            if (observer != null)
                _observers.Remove(observer);
        }

        public void notifyObservers(Document doc)
        {
            foreach (var observer in _observers)
            {
                observer.update(doc);
            }
        }

        public void enterJournalData(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public List<Document> viewJournal()
        {
            return docs;
        }

        public void addDoc(Document doc)
        {
            if (doc == null) return;
            doc.regNumber = $"REG-{docs.Count + 1:D4}";
            doc.regDate = DateTime.Now;
            doc.addDoc();
            docs.Add(doc);
            notifyObservers(doc);
        }

        public void removeDoc(Document doc)
        {
            if (doc == null) return;
            docs.Remove(doc);
        }

        public Document findDoc(Document doc)
        {
            if (doc == null) return null;
            return docs.FirstOrDefault(d => d.id == doc.id);
        }
    }

    public class User : IApprover, IObserver
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string login { get; set; }
        public string passw { get; set; }
        public UserRole role { get; set; }
        public string name { get; set; }
        public Action<string> OnReceiveCorrespondence;
        private static int _counter = 0;

        public User()
        {
            _counter++;
            name = $"User №{_counter}";
        }

        public void approve(Document doc)
        {
            if (role != UserRole.DIRECTOR)
                throw new UnauthorizedAccessException("Только руководитель может утверждать!");
            doc.status = DocumentStatus.APPROVED;
        }

        public void reject(Document doc, string reason)
        {
            if (role != UserRole.DIRECTOR)
                throw new UnauthorizedAccessException("Только руководитель может отклонять!");
            doc.status = DocumentStatus.REJECTED;
            doc.content += $"\n[Отклонено]: {reason}";
        }

        public bool hasRole(UserRole role) => this.role == role;

        public void update(Document doc)
        {
            getCorrespondence(doc);
        }
        public void getCorrespondence(Document doc)
        {
            string msg = $"Пользователь {firstName} {surName} получил корреспонденцию: {doc.name}";
            if (OnReceiveCorrespondence != null)
                OnReceiveCorrespondence.Invoke(msg);
            else
                Console.WriteLine(msg);
        }

        public override string ToString() => $"{name} ({login})";
    }
    
    public class AuthService
    {
        public List<User> users { get; set; } = new List<User>();

        public bool login(string login, string password)
        {
            var user = findUserByLogin(login);
            if (user == null) return false;
            return checkPassword(user, password);
        }

        public User findUserByLogin(string login)
        {
            return users.FirstOrDefault(u => u.login == login);
        }

        public bool checkPassword(User user, string password)
        {
            return user != null && user.passw == password;
        }

        public void addUser(User user)
        {
            users.Add(user);
        }
    }
}