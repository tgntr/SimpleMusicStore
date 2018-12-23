using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.DiscogsDtos.RecordDtos;
using SimpleMusicStore.Web.Services;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using SimpleMusicStore.Web.Models.ViewModels;
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

        public async Task<IActionResult> Add()
        {
            var discogsRecordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(3771290);
            var viewModel = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var discogsId = await DiscogsUtilities.GetDiscogsIdAsync(model.DiscogsUrl);
            return Redirect($"/admin/record/preview/{discogsId}");
        }

        
        public async Task<IActionResult> Preview(long discogsId)
        {
            if (!await DiscogsUtilities.IsValidDiscogsIdAsync(discogsId))
            {
                return RedirectToAction("Add");
            }

            var recordId = await _recordService.FindByDiscogsId(discogsId);
            if (recordId >= 0)
            {
                return Redirect($"/view/records/{recordId}");
            }

            var discogsRecordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(discogsId);

            var previewModel = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);

            return View(previewModel);
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
                return View(model);
            }

            var recordDto = await DiscogsUtilities.GetAsync<DiscogsRecordDto>(discogsId);
            await _recordService.AddRecord(recordDto, model.Price);

            var recordId = await _recordService.FindByDiscogsId(discogsId);
            return Redirect($"/view/records/{recordId}");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await _recordService.IsValidRecordId(id))
            {
                return Redirect("/view/records");
            }

            var record = await _recordService.GetRecordAsync(id);
            var recordViewModel = _mapper.Map<RecordAdminViewModel>(record);

            return View(recordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecordAdminViewModel model)
        {
            if (!await _recordService.IsValidRecordId(id))
            {
                return Redirect("/view/records");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _recordService.EditRecordPrice(id, model.Price);
            
            return Redirect($"/view/records/{id}");
        }

        public async Task<IActionResult> Remove(int id)
        {
            if (!await _recordService.IsValidRecordId(id))
            {
                return Redirect("/view/records");
            }

            var record = await _recordService.GetRecordAsync(id);
            var recordViewModel = _mapper.Map<RecordAdminViewModel>(record);

            return View(recordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id, bool isPostMethod = true)
        {
            if (!await _recordService.IsValidRecordId(id))
            {
                return Redirect("/view/records");
            }

            await _recordService.RemoveRecord(id);
            
            return Redirect("/view/records");
        }



    }
}