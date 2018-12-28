using SimpleMusicStore.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.ViewModels
{
    public class LabelViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<RecordDto> Records { get; set; }

        public List<CommentDto> Comments { get; set; }

        [Required]
        public string Comment { get; set; }

        public bool IsFollowed { get; set; } = false;
    }
}
