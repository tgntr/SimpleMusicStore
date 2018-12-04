using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.ViewModels;
using SimpleMusicStore.Web.Services;

namespace SimpleMusicStore.Web.Controllers
{
    public class ViewController : Controller
    {
        private RecordService _recordService;
        private LabelService _labelService;
        private ArtistService _artistService;
        private IMapper _mapper;

        public ViewController(
           SimpleDbContext context,
           IMapper mapper)
        {
            _recordService = new RecordService(context, mapper);
            _labelService = new LabelService(context);
            _artistService = new ArtistService(context);
            _mapper = mapper;
        }

        public IActionResult Records(string orderBy = "")
        {
            List<RecordViewModel> records = _recordService.All(orderBy).Select(_mapper.Map<RecordViewModel>).ToList();

            return View(records);
        }

        public IActionResult Records(int id)
        {
            var record = _recordService.GetRecord(id);

            if (record is null)
            {
                return RedirectToAction("Records");
            }

            var viewModel = _mapper.Map<RecordViewModel>(record);

            return View(viewModel);
        }
        
        public IActionResult Labels(string orderBy = "")
        {
            List<LabelViewModel> labels = _labelService.All(orderBy).Select(_mapper.Map<LabelViewModel>).ToList();

            return View(labels);
        }

        public IActionResult Labels(int id)
        {
            var label = _labelService.GetLabel(id);

            if (label is null)
            {
                return RedirectToAction("Labels");
            }

            var viewModel = _mapper.Map<LabelViewModel>(label);

            return View(viewModel);
        }
        
        public IActionResult Artists(string orderBy = "")
        {
            List<ArtistViewModel> artists = _artistService.All(orderBy).Select(_mapper.Map<ArtistViewModel>).ToList();

            return View(artists);
        }

        public IActionResult Artists(int id)
        {
            var artist = _artistService.GetArtist(id);

            if (artist is null)
            {
                return RedirectToAction("Artists");
            }

            var viewModel = _mapper.Map<ArtistViewModel>(artist);

            return View(viewModel);
        }

        public IActionResult Genres(string genre, string orderBy = "")
        {
            if (_recordService.IsValidGenre(genre))
            {
                return RedirectToAction("Records");
            }

            List<RecordViewModel> records = _recordService.All(genre).Select(_mapper.Map<RecordViewModel>).ToList();
            
            return View(records);
        }
    }
}