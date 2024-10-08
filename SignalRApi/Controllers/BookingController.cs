﻿using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignalRBusinessLayer.Abstract;
using SignalRDtoLayer.BookingDto;
using SignalREntityLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<CreateBookingDto> _validator; 

        public BookingController(IBookingService BookingService,IValidator<CreateBookingDto> validator)
        {
            _bookingService = BookingService;
            _validator = validator;
        }

       

        [HttpGet]
        public IActionResult BookingLİst()
        {
            var values = _bookingService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            var validation=_validator.Validate(createBookingDto);
            if(!validation.IsValid)
            {
                return BadRequest(validation.Errors);

            }
            Booking Booking = new Booking()
            {
                Name = createBookingDto.Name,
                Phone = createBookingDto.Phone,
                Mail = createBookingDto.Mail,
                PersonCount = createBookingDto.PersonCount,
                Date = createBookingDto.Date,
                Description=createBookingDto.Description


            };
            _bookingService.TAdd(Booking);
            return Ok("Rezervasyon yapıldı.");

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var values = _bookingService.TGetByID(id);
            _bookingService.TDelete(values);
            return Ok("Rezervasyonsilindi.");

        }
        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            Booking Booking = new Booking()
            {
                BookingID = updateBookingDto.BookingID,
                Name = updateBookingDto.Name,
                Phone = updateBookingDto.Phone,
                Mail = updateBookingDto.Mail,
                PersonCount = updateBookingDto.PersonCount,
                Date = updateBookingDto.Date,
                Description=updateBookingDto.Description

            };
            _bookingService.TUpdate(Booking);
            return Ok("Rezervasyon güncellendi.");

        }
        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            return Ok(value);

        }
        [HttpGet("BookingStatusApproved/{id}")]
         public IActionResult BookingStatusApproved(int id)

        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon Açıklaması Değiştirildi");
        }
		[HttpGet("BookingStatusCancelled/{id}")]
		public IActionResult BookingStatusCancelled(int id)

		{
			_bookingService.TBookingStatusCancelled(id);
			return Ok("Rezervasyon Açıklaması Değiştirildi");
		}
	}
}
