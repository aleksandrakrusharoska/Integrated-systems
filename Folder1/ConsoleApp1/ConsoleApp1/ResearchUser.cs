namespace ConsoleApp1
{
    class ResearchUser : LibraryUser
    {
        public override int BorrowLimit() => int.MaxValue;
    }
}
