
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos;
using SimpleMusicStore.Web.Services;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace SimpleMusicStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecordController : Controller
    {
        private RecordService _recordService;
        private IMapper _mapper;

        public RecordController(
           SimpleDbContext context,
           IMapper mapper)
        {
            _recordService = new RecordService(context, mapper);
            _mapper = mapper;
        }



        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(AddRecordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentDiscogsId = await DiscogsUtilities.GetDiscogsIdAsync(model.DiscogsUrl);

            var recordId = await _recordService.FindByDiscogsIdAsync(currentDiscogsId);
            if (recordId >= 0)
            {
                var record = await _recordService.GetAsync(recordId);

                if (!record.IsActive)
                {
                    await _recordService.AddExistingAsync(recordId);
                }

                return Redirect($"/records/details?recordId={recordId}");
            }

            return RedirectToAction("Preview", "Record", new { discogsId = currentDiscogsId, Area = "Admin" });
        }




        public async Task<IActionResult> Preview(long discogsId)
        {
            if (!await DiscogsUtilities.IsValidDiscogsIdAsync(discogsId))
            {
                return RedirectToAction("Add");
            }

            var discogsRecordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(discogsId);

            var model = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Preview(long discogsId, RecordAdminViewModel model)
        {
            if (!await DiscogsUtilities.IsValidDiscogsIdAsync(discogsId))
            {
                return RedirectToAction("Add");
            }

            if (!ModelState.IsValid)
            {
                var discogsRecordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(discogsId);

                model = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);

                return View(model);
            }
            
            var recordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(discogsId);
            await _recordService.AddAsync(recordDto, model.Price);

            var recordId = await _recordService.FindByDiscogsIdAsync(discogsId);
            return Redirect($"/records/details?recordId={recordId}");
        }



        public async Task<IActionResult> Edit(int recordId)
        {
            if (!await _recordService.IsValidIdAsync(recordId))
            {
                return Redirect("/records/all");
            }

            var record = await _recordService.GetAsync(recordId);
            var model = _mapper.Map<RecordAdminViewModel>(record);

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int recordId, RecordAdminViewModel model)
        {
            if (!await _recordService.IsValidIdAsync(recordId))
            {
                return Redirect("/records/all");
            }

            if (!ModelState.IsValid)
            {
                var record = await _recordService.GetAsync(recordId);
                model = _mapper.Map<RecordAdminViewModel>(record);

                return View(model);
            }

            await _recordService.EditAsync(recordId, model.Price);
            
            return Redirect($"/records/details?recordId={recordId}");
        }



        public async Task<IActionResult> Remove(int recordId)
        {
            if (!await _recordService.IsValidIdAsync(recordId))
            {
                return Redirect("/records/all");
            }

            await _recordService.RemoveAsync(recordId);

            return Redirect("/");
            
        }
    }
}