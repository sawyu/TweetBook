using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Domain;
using TweetBook.Extensions;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public TagController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns all the tags in the system
        /// </summary>
        ///<response code="200">Returns all the tags in the system</response>
        [HttpGet(ApiRoutes.Tags.GetAll)]
       // [Authorize(Policy ="TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _postService.GetAllTagsAsync();
            var tagsResponse = _mapper.Map<List<TagResponse>>(tags);
            return Ok(tagsResponse);
        }
      
        [HttpGet(ApiRoutes.Tags.Get,Name ="Get")]
        public async Task<IActionResult> Get([FromRoute]string tagName)
        {
            var tag =await _postService.GetTagByNameAsync(tagName);
            if(tag == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TagResponse>(tag));
        }

        /// <summary>
        /// Create a tag in the system
        /// </summary>
        /// <remarks>
        ///     Sample **request**:
        ///         
        ///     POST /api/v1/tags
        ///     {
        ///         "name":"Some name"
        ///     }
        /// </remarks>
        ///<response code="200">Create a tag in the system</response>
        //////<response code="200">Unable to create a tag due to validation error</response>
        [HttpPost(ApiRoutes.Tags.Create)]
        [ProducesResponseType(typeof(TagResponse),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
        {
            var newTag = new Tag
            {
                TagName = request.TagName,
                CreatorId = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow

            };
            var created = await _postService.CreateTagAsync(newTag);
            if (created == null)
            {
                return BadRequest(new { error = "Unable to create tag" });
            }
            var response = _mapper.Map<TagResponse>(newTag);
            /* var baseUrl = $"{ HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
             var locationUrl = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
             return Created(locationUrl, newTag); */
            return CreatedAtRoute("Get", new { tagName = response.Name }, response);
        }
     
        [HttpDelete(ApiRoutes.Tags.Delete)]
        [Authorize(Policy = "MustWorkForChapsas")]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted =  await _postService.DeleteTagAsync(tagName);                    
            if (deleted) return NoContent();
            return NotFound();

        }
    }
}
