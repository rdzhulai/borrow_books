namespace Assignment1
{
    public class Record
    {
        private Book book;
        private Reader reader;
        private DateTime borrowed;
        private bool returned;

        static int DAYS_IN_MONTH = 30;
        static int EXTRA_MONTH_FEE = 5;
        static float EXTRA_DAY_FEE = .1f;
        static int FREE_MONTHS = 1;

        public Record(Book book, Reader reader, DateTime borrowed)
        {
            this.book = book;
            this.reader = reader;
            this.borrowed = borrowed;
            returned = false;
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
            set { returned = value; }
        }
    }

}
