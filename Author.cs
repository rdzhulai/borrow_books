namespace Assignment1
{
    public class Author
    {
        private string firstName;
        private string lastName;

        private bool IsNameValid(string firstName, string lastName)
        {
            if ('A' > firstName[0] || firstName[0] > 'Z'
                || 'A' > lastName[0] || lastName[0] > 'Z')
            {
                return false;
            }
            return firstName.Skip(1).All((char c) => c >= 'a' && c <= 'z')
                && lastName.Skip(1).All((char c) => c >= 'a' && c <= 'z');
        }

        public Author(string firstName, string lastName)
        {
            if (!IsNameValid(firstName, lastName))
                throw new ArgumentException();
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Author(string name)
        {
            string[] splittedName = name.Split(' ');
            if (!IsNameValid(splittedName[0], splittedName[1]))
                throw new ArgumentException();
            firstName = splittedName[0];
            lastName = splittedName[1];
        }

        public void SetAuthorName(string firstName, string lastName)
        {
            if (IsNameValid(firstName, lastName))
            {
                this.firstName = firstName;
                this.lastName = lastName;
            }
        }

        public string GetAuthorName()
        {
            return $"{firstName} {lastName}";
        }
    }
}

