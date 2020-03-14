using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using RediSearchCore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FastFoodsController : ControllerBase
    {
        private readonly IFastFoodService _fastFoodService;

        public FastFoodsController(IFastFoodService fastFoodService)
        {
            _fastFoodService = fastFoodService;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<FastFoods>>> Search([FromQuery]FastFoodsInputCommand command)
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = await _fastFoodService.SearchAsync(command)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("CreateIndex")]
        public ActionResult<bool> CreateIndex()
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = _fastFoodService.CreateIndex()
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("DropIndex")]
        public ActionResult<bool> DropIndex()
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = _fastFoodService.DropIndex()
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("PushSampleData")]
        public ActionResult<bool> PushSampleData()
        {
            try
            {
                _fastFoodService.PushSampleData();

                return Ok(new Notification
                {
                    Success = true
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<IEnumerable<Airports>>> Get(string key)
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = await _fastFoodService.GetAsync(key)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(string docId, FastFoods fastFoods)
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = await _fastFoodService.UpdateAsync(docId, fastFoods)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add(string docId, FastFoods fastFoods)
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = await _fastFoodService.AddAsync(docId, fastFoods)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> Delete(string key)
        {
            try
            {
                return Ok(new Notification
                {
                    Success = true,
                    Data = await _fastFoodService.DeleteAsync(key)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Notification
                {
                    Success = false,
                    Errors = ex.Message
                });
            }
        }
    }
}