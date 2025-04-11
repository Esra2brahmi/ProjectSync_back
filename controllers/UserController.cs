using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectSync_back.data;
using projectSync_back.Dtos.User;
using projectSync_back.Models;
using projectSync_back.Mappers;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Interfaces;

namespace projectSync_back.Controllers
{
    [Route("/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpOptions]
        [Route("user")]
        public IActionResult Preflight()
        {
            return NoContent(); 
        }

        private readonly IUserRepository _userRepo;
        public UserController(ApplicationDBContext context,IUserRepository userRepo)
        {
           _userRepo=userRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var users = await _userRepo.GetAllAsync();
            var userDto=users.Select(s=>s.ToUserDto());
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user =await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            // Validate the DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userModel = userDto.ToUserFromCreateDto();
            await _userRepo.CreateAsync(userModel);
            // Return the created user with its ID
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateUserRequestDto updateUserRequest)
        {
            var userModel=await _userRepo.UpdateAsync(id,updateUserRequest);
            if(userModel==null){
                return NotFound();
            }
             return Ok(userModel.ToUserDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var userModel=await _userRepo.DeleteAsync(id);
            if(userModel==null){
                return NotFound();
            }
            return NoContent();
        }


        
        
        
        }


}