using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using RouletteApi.Common;
using RouletteApi.Models;
using RouletteApi.Services;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;
        private readonly BetsService _betsService;
        private readonly IConfiguration Configuration;
        public RouletteController(RouletteService rouletteService, BetsService betsService, IConfiguration configuration)
        {
            _betsService = betsService;
            _rouletteService = rouletteService;
            Configuration = configuration;
        }

        /// <summary>
        /// 1. New roulette creation endpoint that returns the id of the new roulette created
        /// </summary>
        /// <returns>IdRoule</returns>
        [HttpPost]
        public ActionResult<string> Create()
        {
            try
            {
                RouletteDto roulette = new RouletteDto()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Active = false,
                    OpenedDate = DateTime.Now.Date,
                    ClosedDate = null,
                    Name = "Roulette"
                };
                roulette = _rouletteService.Create(roulette);

                return Ok(roulette.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 2. Roulette opening endpoint (the input is a roulette id) that allows subsequent betting requests, 
        /// it must simply return a status confirming that the operation was successful or denied
        /// </summary>
        /// <returns>IdRoule</returns>
        [HttpPut("{id}/open")]
        public IActionResult Open([FromRoute(Name = "id")] string id)
        {
            try
            {
                RouletteDto RouletteDto = _rouletteService.Get(id);
                if (RouletteDto == null)
                {
                    return NotFound();
                }
                if (RouletteDto.Active)
                {
                    return BadRequest(new
                    {
                        error = false,
                        msg = "RC004"
                    });
                }
                RouletteDto.Active = true;
                _rouletteService.Update(id, RouletteDto);

                return Ok(RouletteDto.Active);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 3. Endpoint betting on a number (valid numbers to bet are from 0 to 36) or color (black or red) 
        /// of the roulette a certain amount of money (maximum 10,000 dollars) to an open roulette 
        /// (note: this enpoint receives in addition to the parameters of the bet, a user id in the HEADERS 
        /// assuming that the service that makes the request already made an authentication and validation that 
        /// the client has the necessary credit to make the bet)
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/bet")]
        public IActionResult Bet([FromHeader(Name = "userId")] string userId, [FromRoute(Name = "id")] string id, BetsDto bet)
        {
            try
            {
                if (bet.BetValue > double.Parse(Configuration["BetMax"]) || bet.BetValue < 1)
                {
                    return BadRequest(new
                    {
                        error = true,
                        msg = "RC002"
                    });
                }
                if (bet.BetType == BetTypeEnumerable.Position && (bet.Position == null || bet.Color != null))
                {
                    return BadRequest(new
                    {
                        error = true,
                        msg = "RC003"
                    });
                }
                if (bet.BetType == BetTypeEnumerable.Color && (bet.Position != null || bet.Color == null))
                {
                    return BadRequest(new
                    {
                        error = true,
                        msg = "RC003"
                    });
                }
                RouletteDto roulette = _rouletteService.Get(id);
                if (roulette == null || !roulette.Active)
                {
                    return BadRequest(new
                    {
                        error = true,
                        msg = "RC001"
                    });
                }
                bet.BetDate = DateTime.Now.Date;
                bet.IdUser = userId;
                bet.IdRoulette = roulette.Id;
                _betsService.Create(bet);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 4. Endpoint of closing bets given a roulette id, this endpoint must return 
        /// the result of the bets made from opening to closing.
        /// </summary>
        /// <param name="id"> rulette id</param>
        /// <returns></returns>
        [HttpPut("{id}/close")]
        public ActionResult<List<BetsDto>> Close([FromRoute(Name = "id")] string id)
        {
            try
            {
                RouletteDto RouletteDto = _rouletteService.Get(id);
                if (RouletteDto == null)
                {
                    return NotFound();
                }
                if (!RouletteDto.Active)
                {
                    return BadRequest(new
                    {
                        error = true,
                        msg = "RC001"
                    });
                }
                RouletteDto.Active = false;
                RouletteDto.ClosedDate = DateTime.Now.Date;
                _rouletteService.Update(id, RouletteDto);
                List<BetsDto> best = _betsService.GetBestInRoulette(RouletteDto.Id);

                return Ok(best);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }

        /// <summary>
        /// 5. Endpoint of list of roulettes created with their states (open or closed)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<RouletteDto>> Get()
        {
            try
            {
                List<RouletteDto> roulette = _rouletteService.Get();

                return Ok(roulette);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Bets")]
        public ActionResult<List<RouletteDto>> GetBets([FromRoute(Name = "id")] string id)
        {
            try
            {
                List<BetsDto> best = _betsService.GetBestInRoulette(id);

                return Ok(best);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
