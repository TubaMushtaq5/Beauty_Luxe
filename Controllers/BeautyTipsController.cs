using BeautyLuxe.Models;
using BeautyLuxe.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautyLuxe.Controllers
{
    public class BeautyTipsController : Controller
    {
        private readonly IBeautyTipsRepository _beautyTipsRepository;
        public BeautyTipsController(IBeautyTipsRepository beautytipsRepository)
        {
            _beautyTipsRepository = beautytipsRepository;
        }
        public IActionResult Index()
        {
            var allTips = _beautyTipsRepository.GetAllBeautyTips();
            return View(allTips);
        }

        [Authorize(Roles = "admin")]
        public IActionResult CreateBeautyTip()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CreateBeautyTip(BeautyTip beautyTip)
        {


            _beautyTipsRepository.CreateBeautyTip(beautyTip);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditBeautyTip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beautyTip = _beautyTipsRepository.GetBeautyTipById(id.Value);


            if (beautyTip == null)
            {
                return NotFound();
            }

            return View(beautyTip);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditBeautyTip(BeautyTip modifiedBeautyTip)
        {
            _beautyTipsRepository.UpdateBeautyTip(modifiedBeautyTip);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteBeautyTip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beautyTip = _beautyTipsRepository.GetBeautyTipById(id.Value);
            _beautyTipsRepository.DeleteBeautyTip(beautyTip);
            return RedirectToAction("Index");
        }

        public IActionResult BeautyTipDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beautyTip = _beautyTipsRepository.GetBeautyTipById(id.Value);


            if (beautyTip == null)
            {
                return NotFound();
            }

            return View(beautyTip);
        }
    }
}
