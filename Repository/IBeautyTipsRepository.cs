using BeautyLuxe.Models;

namespace BeautyLuxe.Repository
{
    public interface IBeautyTipsRepository
    {
        BeautyTip CreateBeautyTip(BeautyTip beautyTip);
        BeautyTip GetBeautyTipById(int beautyTipId);
        IEnumerable<BeautyTip> GetAllBeautyTips();
        BeautyTip UpdateBeautyTip(BeautyTip beautyTip);

        void DeleteBeautyTip(BeautyTip beautyTip);
    }
}
