using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyLuxe.Repository
{
    public class BeautyTipsRepository : IBeautyTipsRepository
    {
        private readonly AppDbContext _dbContext;

        public BeautyTipsRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public BeautyTip CreateBeautyTip(BeautyTip beautyTip)
        {
            _dbContext.BeautyTips.Add(beautyTip);
            _dbContext.SaveChanges();
            return beautyTip;
        }

        public void DeleteBeautyTip(BeautyTip beautyTip)
        {
           
            if (beautyTip != null)
            {
                _dbContext.BeautyTips.Remove(beautyTip);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<BeautyTip> GetAllBeautyTips()
        {
            return _dbContext.BeautyTips.ToList();
        }

        public BeautyTip GetBeautyTipById(int beautyTipId)
        {
            return _dbContext.BeautyTips.FirstOrDefault(b => b.Id == beautyTipId);
        }

        public BeautyTip UpdateBeautyTip(BeautyTip beautyTip)
        {
            _dbContext.BeautyTips.Update(beautyTip);
            _dbContext.SaveChanges();
            return beautyTip;
        }
    }
}
