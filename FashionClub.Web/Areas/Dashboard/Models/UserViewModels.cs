using FashionClub.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionClub.Web.Areas.Dashboard.Models
{
    public class UserActionModel
    {
    }
    public class UsersListingViewModel
    {
        //public IQueryable<FashionClubUser> Users { get; set; }
        public List<FashionClubUser> Users { get; set; }
        public string SearchTerm { get; set; }
        public List<FashionClubRole> Roles { get; set; }
    }
    public class UserRolesViewModel
    {
        public string UserID { get; set; }
        public IEnumerable<FashionClubRole> Roles { get; set; }
        public IEnumerable<FashionClubRole> UserRoles { get; set; }
    }
    public class UserProfileViewModel
    {
        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
    public class UserDetailsViewModel
    {
        public FashionClubUser FashionClubUser { get; set; }

    }
}