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

namespace SimpleMusicStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var discogsRecordDto = DiscogsUtilities.Get<DiscogsRecordDto>(3771290);
            var viewModel = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRecordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var discogsId = DiscogsUtilities.GetDiscogsId(model.DiscogsUrl);
            return Redirect($"/admin/record/preview/{discogsId}");
        }

        
        public IActionResult Preview(long discogsId)
        {
            if (!DiscogsUtilities.IsValidDiscogsId(discogsId))
            {
                return RedirectToAction("Add");
            }

            //TODO if already in store redirect to record page

            var discogsRecordDto = DiscogsUtilities.Get<DiscogsRecordDto>(discogsId);

            var previewModel = _mapper.Map<RecordAdminViewModel>(discogsRecordDto);

            return View(previewModel);
        }

        [HttpPost]
        public IActionResult Preview(long discogsId, PriceBindingModel model)
        {
            if (!DiscogsUtilities.IsValidDiscogsId(discogsId) || !ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            var recordDto = DiscogsUtilities.Get<DiscogsRecordDto>(discogsId);
            _recordService.AddRecord(recordDto, model.Price);

            //TODO redirect to record page
            return Redirect("/");
        }

        public IActionResult Edit(int id)
        {
            if (!_recordService.IsValidRecordId(id))
            {
                return Redirect("/");
            }

            var record = _recordService.GetRecord(id);
            var recordViewModel = _mapper.Map<RecordAdminViewModel>(record);

            return View(recordViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, PriceBindingModel model)
        {
            if (!_recordService.IsValidRecordId(id) || ModelState.IsValid)
            {
                return Redirect("/");
            }

            _recordService.EditRecordPrice(id, model.Price);

            //TODO redirect to record page
            return Redirect("/");
        }

        public IActionResult Remove(int id)
        {
            if (!_recordService.IsValidRecordId(id))
            {
                return Redirect("/");
            }

            var record = _recordService.GetRecord(id);
            var recordViewModel = _mapper.Map<RecordAdminViewModel>(record);

            return View(recordViewModel);
        }

        [HttpPost]
        public IActionResult Remove(int id, bool isPostMethod = true)
        {
            if (!_recordService.IsValidRecordId(id))
            {
                return Redirect("/");
            }

            _recordService.RemoveRecord(id);
            
            return Redirect("/");
        }



    }
}