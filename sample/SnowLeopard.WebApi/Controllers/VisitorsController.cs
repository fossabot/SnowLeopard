﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SnowLeopard.Caching.Abstractions;
using SnowLeopard.Infrastructure;
using SnowLeopard.Model.BaseModels;
using SnowLeopard.Mongo;
using SnowLeopard.WebApi.MongoEntities;

namespace SnowLeopard.WebApi.Controllers
{
    /// <summary>
    /// Values
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class VisitorsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly VistorMongoContext _vistorMongoCtx;

        public VisitorsController(
            ILogger<VisitorsController> logger,
            VistorMongoContext vistorMongoCtx
        )
        {
            _logger = logger;
            _vistorMongoCtx = vistorMongoCtx;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<Visitor>>), (int)HttpStatusCode.OK)]
        [CachingInterceptor]
        [Caching]
        public virtual async Task<List<Visitor>> Get()
        {
            return _vistorMongoCtx.Visitors.AsQueryable().Take(10).ToList();
        }

        /// <summary>
        /// Get
        /// </summary>+
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDTO<Visitor>), (int)HttpStatusCode.OK)]
        public async Task<Visitor> Get(string id)
        {
            return await _vistorMongoCtx.Visitors.FirstOrDefaultAsync(x => x.Id == new MongoDB.Bson.ObjectId(id));
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ProducesResponseType(typeof(BaseDTO<Visitor>), (int)HttpStatusCode.OK)]
        public async Task<Visitor> Post([FromBody]Visitor value)
        {
            var result = await _vistorMongoCtx.Visitors
                            .FindOneAndUpdateAsync(x => x.UserId == value.UserId && x.VisitorId == value.VisitorId,
                                Builders<Visitor>.Update.Set(x => x.VisitTime, DateTime.Now),
                                true
                            );
            return result;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseDTO<Visitor>), (int)HttpStatusCode.OK)]
        public async Task<Visitor> Put(string id, [FromBody]Visitor value)
        {
            var result = await _vistorMongoCtx.Visitors
                            .FindOneAndUpdateAsync(x => x.UserId == value.UserId && x.VisitorId == value.VisitorId,
                                Builders<Visitor>.Update.Set(x => x.VisitTime, DateTime.Now),
                                true
                            );
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<bool> Delete(string id)
        {
            await _vistorMongoCtx.Visitors.DeleteOneAsync(x => x.Id == new MongoDB.Bson.ObjectId(id));
            return true;
        }
    }
}
