using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SimpleMusicStore.Models
{
    public class SimpleUser : IdentityUser
    {
        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<ArtistUser> FollowedArtists { get; set; } = new List<ArtistUser>();

        public List<LabelUser> FollowedLabels { get; set; } = new List<LabelUser>();

        public List<RecordUser> Wantlist { get; set; } = new List<RecordUser>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
