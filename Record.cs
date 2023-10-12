namespace Assignment1
{
    public class Record
    {
        private Book book;
        private Reader reader;
        private DateTime borrowed;
        private bool returned;
        private DateTime returnedTime;

        private const int DAYS_IN_MONTH = 30;
        private const int EXTRA_MONTH_FEE = 5;
        private const float EXTRA_DAY_FEE = .1f;
        private const int FREE_MONTHS = 1;

        public Record(
            Book book, Reader reader, DateTime borrowed, bool returned = false)
        {
            this.book = book;
            this.reader = reader;
            this.borrowed = borrowed;
            this.returned = returned;
        }

        public Record(
            Book book, Reader reader, DateTime borrowed, DateTime returnedTime)
        {
            this.book = book;
            this.reader = reader;
            this.borrowed = borrowed;
            returned = true;
            this.returnedTime = returnedTime;
        }

        public float GetFee(DateTime date)
        {
            int borrowedDays = (date - borrowed).Days;
            (int months, int days) = Math.DivRem(
                borrowedDays, DAYS_IN_MONTH);
            if (months < FREE_MONTHS || (months == FREE_MONTHS && days == 0))
            {
                return 0;
            }
            return (months - FREE_MONTHS) * EXTRA_MONTH_FEE
                 + days * EXTRA_DAY_FEE;
        }
        public Book Book
        {
            get { return book; }
        }
        public Reader Reader
        {
            get { return reader; }
        }
        public bool Returned
        {
            get { return returned; }
        }

        public DateTime ReturnedTime
        {
            get { return returnedTime; }
        }

        public float ReturnBook(DateTime date)
        {
            if (returned)
                throw new Exception("Returning an already returned book");
            returned = true;
            returnedTime = date;
            return GetFee(date);
        }
    }

}
