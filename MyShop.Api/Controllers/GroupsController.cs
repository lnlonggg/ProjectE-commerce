using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.DTOs;
using MyShop.Core.Entities;
using MyShop.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace MyShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDto groupDto)
        {
            var newGroup = await _groupService.CreateGroupAsync(groupDto);
            return CreatedAtAction(nameof(GetGroup), new { id = newGroup.Id }, newGroup);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupCreateDto groupDto)
        {
            var success = await _groupService.UpdateGroupAsync(id, groupDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var success = await _groupService.DeleteGroupAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}