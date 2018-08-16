﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SnowLeopard.Infrastructure
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class HealthController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly SnowLeopardUtils _snowLeopardUtils;

        /// <summary>
        /// HealthController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="snowLeopardUtils"></param>
        public HealthController(
            ILogger<HealthController> logger,
            SnowLeopardUtils snowLeopardUtils
            )
        {
            _logger = logger;
            _snowLeopardUtils = snowLeopardUtils;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return _snowLeopardUtils.EntryAssemblyVersion;
        }
    }
}
