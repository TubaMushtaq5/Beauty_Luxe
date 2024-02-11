using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautyLuxe.Models;
using Microsoft.AspNetCore.Identity;

namespace BeautyLuxe.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string FullName { get; set; }
    public List<Booking> Bookings { get; set; }
}

