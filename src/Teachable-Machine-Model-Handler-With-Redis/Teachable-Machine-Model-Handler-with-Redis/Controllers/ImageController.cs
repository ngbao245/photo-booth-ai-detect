using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teachable_Machine_Model_Handler_with_Redis.Services;

namespace Teachable_Machine_Model_Handler_with_Redis.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ResizeImageByte _resizeImageByte;
        private readonly RedisListener _redisListener;

        public ImageController(RedisListener redisListener, ResizeImageByte resizeImageByte)
        {
            _resizeImageByte = resizeImageByte;
            _redisListener = redisListener;
        }

        [Route("/api/GetByteImage")]
        [HttpGet]
        public IActionResult GetByteImage()
        {
            var byteImage = _redisListener.ListenForData("video");
            //var resizedByteImage = _resizeImageByte.ResizeImage(byteImage, 1000000);
            //var length = byteImage.Length;
            var data = new Response()
            {
                data = byteImage
            };
            return Ok(data);
        }


        [Route("/api/ReturnResultString")]
        [HttpGet]
        public IActionResult GetByteImage(string? result)
        {
            var resultService = new ResultService();
            if (result != null)
            {
                resultService.SendResultToRedisChannel(result);
            }
            return Ok();
        }

    }
}
