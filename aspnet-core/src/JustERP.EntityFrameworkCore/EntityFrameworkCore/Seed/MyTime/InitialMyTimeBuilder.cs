namespace JustERP.EntityFrameworkCore.Seed.MyTime
{
    public class InitialMyTimeBuilder
    {
        private readonly JustERPDbContext _context;

        public InitialMyTimeBuilder(JustERPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultActivityCreator(_context).Create();
            new DefaultLabelCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
