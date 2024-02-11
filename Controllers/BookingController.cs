// BookingController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautyLuxe.Models;
using BeautyLuxe.Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeautyLuxe.Areas.Identity.Data;


public class BookingController : Controller
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly UserManager<User> _userManager;

    public BookingController(IServiceRepository serviceRepository, IBookingRepository bookingRepository, IUserRepository userRepository, UserManager<User> userManager)
    {
        _serviceRepository = serviceRepository;
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    [Authorize(Roles = "client")]
    [Route("Booking/BookAppointment/{serviceId}")]
    public IActionResult BookAppointment(int serviceId)
    {
        string userId = _userManager.GetUserId(User);
        var service = _serviceRepository.GetServiceById(serviceId);
        var user = _userRepository.getUserById(userId);

        if (service == null || user == null)
        {
            return NotFound();
        }


        var bookingModel = new Booking
        {
            ServiceId = serviceId,
            UserId = userId,
            Service = service, 
            User = user 
        };


        return View(bookingModel);
    }


    [Authorize(Roles = "client")]
    [HttpPost]
    public IActionResult BookedAppointment(Booking booking)
    {
        booking.User = _userRepository.getUserById(booking.UserId);
        booking.Slot = _serviceRepository.GetSlotById(booking.SlotId);
        booking.Slot.IsAvailable = false;
        booking.Slot.BookingId = booking.Id;

        _bookingRepository.BookAppointment(booking);
        return RedirectToAction("ConfirmBooking", new { serviceId = booking.ServiceId, slotId = booking.SlotId, userId = booking.UserId });
    }

    [Authorize(Roles = "client")]
    public IActionResult ConfirmBooking(int serviceId, int slotId, string userId)
    {
        var booking = new Booking
        {
            UserId = userId,
            ServiceId = serviceId,
            SlotId = slotId,
            User = _userRepository.getUserById(userId), 
            Service = _serviceRepository.GetServiceById(serviceId),
            Slot = _serviceRepository.GetSlotById(slotId)
        };
        return View(booking);
    }

    [Authorize(Roles = "client")]
    public IActionResult UserBookings()
    {
        _serviceRepository.RemoveExpiredSlots();
        var userId = _userManager.GetUserId(User);
        var bookings = _bookingRepository.GetBookingsByUserId(userId);
        return View(bookings);
    }

    [Authorize(Roles = "client")]
    public IActionResult UpdateAppointment(int bookingId)
    {
        var appointment = _bookingRepository.GetBookingById(bookingId);
        var slots = _serviceRepository.GetSlotsForService(appointment.ServiceId);
        ViewBag.Slots = slots.Where(s => s.IsAvailable && s.Id != appointment.SlotId).ToList();


        if (appointment == null || appointment.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }
        return View(appointment);
    }

    [Authorize(Roles = "client")]
    [HttpPost]
    public IActionResult SaveUpdatedAppointment(Booking updatedBooking)
    {
        var originalBooking = _bookingRepository.GetBookingById(updatedBooking.Id);
        if (originalBooking == null || originalBooking.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }

        var existingSlot = _serviceRepository.GetSlotById(originalBooking.SlotId);
        existingSlot.IsAvailable = true;

        originalBooking.SlotId = updatedBooking.SlotId;
        var newSlot = _serviceRepository.GetSlotById(originalBooking.SlotId);
        newSlot.IsAvailable = false;
        _bookingRepository.UpdateAppointment(originalBooking);
        return RedirectToAction("UserBookings");
    }

    [Authorize(Roles = "client")]
    public IActionResult CancelAppointment(int bookingId)
    {
        var appointment = _bookingRepository.GetBookingById(bookingId);

        if (appointment == null || appointment.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }
        var slot = _serviceRepository.GetSlotById(appointment.SlotId);
        slot.IsAvailable = true;
        _bookingRepository.CancelAppointment(bookingId);

        return RedirectToAction("UserBookings");
    }

    [Authorize(Roles = "admin")]
    public IActionResult AllBookings()
    {
        _serviceRepository.RemoveExpiredSlots();
        var bookings = _bookingRepository.GetAllBookings();
        return View(bookings);
    }
}
